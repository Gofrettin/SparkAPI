using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using NLog;
using Spark.Network.Decoder;
using Spark.Network.Encoder;

namespace Spark.Network
{
    public class RemoteNetwork : INetwork
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public byte EndByte { get; }
        private List<byte> Buffer { get; } = new List<byte>();
        
        public RemoteNetwork(IEncoder encoder, IDecoder decoder, byte endByte)
        {
            Encoder = encoder;
            Decoder = decoder;
            EndByte = endByte;
            
            CancellationTokenSource = new CancellationTokenSource();
            
            Client = new TcpClient(AddressFamily.InterNetwork);
            BackgroundTask = new Task<Task>(NetworkLoop, TaskCreationOptions.LongRunning);
        }

        public TcpClient Client { get; }
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
            
            Client.GetStream().Write(encoded);
        }

        public void Close()
        {
            CancellationTokenSource.Cancel();
            Client.Close();

            Task.WaitAll(BackgroundTask);
        }

        private async Task NetworkLoop()
        {
            while (Client.Connected && !CancellationTokenSource.IsCancellationRequested)
            {
                var buffer = new byte[Client.ReceiveBufferSize];
                int size = await Client.GetStream().ReadAsync(buffer, CancellationTokenSource.Token);

                if (CancellationTokenSource.IsCancellationRequested)
                {
                    break;
                }
                    
                Array.Resize(ref buffer, size);

                if (buffer.Length == 0)
                {
                    Logger.Warn("Packet buffer size is equal to 0");
                    continue;
                }
                    
                if (buffer[^1] != EndByte)
                {
                    Buffer.AddRange(buffer);
                    continue;
                }

                byte[] bytes = Buffer.Count > 0 ? Buffer.Concat(buffer).ToArray() : buffer.ToArray();
                if (Buffer.Count > 0)
                {
                    Buffer.Clear();
                }

                IEnumerable<string> decoded = Decoder.Decode(bytes, bytes.Length);
                foreach (string packet in decoded)
                {
                    Logger.Trace($"In: {packet}");
                    PacketReceived?.Invoke(packet);
                }
            }
        }

        public void Connect(IPEndPoint ep)
        {
            Client.Connect(ep);
            BackgroundTask.Start();
            Logger.Info($"Connected to {ep}");
        }
    }
}