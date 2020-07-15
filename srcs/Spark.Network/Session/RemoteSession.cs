using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using NLog;
using Spark.Network.Decoder;
using Spark.Network.Encoder;

namespace Spark.Network.Session
{
    public class RemoteSession : ISession
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public RemoteSession(IEncoder encoder, IDecoder decoder)
        {
            Encoder = encoder;
            Decoder = decoder;

            Socket = new Socket(SocketType.Stream, ProtocolType.Tcp);
            BackgroundTask = new Task<Task>(async () =>
            {
                while (Socket.Connected && !CancellationTokenSource.IsCancellationRequested)
                {
                    var buffer = new byte[Socket.ReceiveBufferSize];
                    int size = await Socket.ReceiveAsync(buffer, SocketFlags.None, CancellationTokenSource.Token);

                    if (CancellationTokenSource.IsCancellationRequested)
                    {
                        break;
                    }

                    IEnumerable<string> decoded = Decoder.Decode(buffer, size);
                    foreach (string packet in decoded)
                    {
                        Logger.Trace($"In: {packet}");
                        PacketReceived?.Invoke(packet);
                    }
                }
            }, TaskCreationOptions.LongRunning);

            CancellationTokenSource = new CancellationTokenSource();
        }

        public Socket Socket { get; }
        public Task BackgroundTask { get; }

        public IEncoder Encoder { get; }
        public IDecoder Decoder { get; }
        public Func<string, string>[] Modifiers { get; set; }

        public CancellationTokenSource CancellationTokenSource { get; }

        public event Action<string> PacketReceived;

        public void SendPacket(string packet)
        {
            string modified = packet;
            if (Modifiers != null)
            {
                foreach (Func<string, string> modifier in Modifiers)
                {
                    modified = modifier.Invoke(modified);
                }
            }

            Logger.Trace($"Out: {packet}");
            byte[] encoded = Encoder.Encode(modified);
            Socket.Send(encoded);
        }

        public void Stop()
        {
            CancellationTokenSource.Cancel();
            Socket.Close();

            Task.WaitAll(BackgroundTask);
        }

        public void Connect(IPEndPoint ep)
        {
            Socket.Connect(ep);

            BackgroundTask.Start();

            Logger.Info($"Connected to {ep}");
        }
    }
}