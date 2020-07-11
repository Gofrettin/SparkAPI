using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using Spark.Network.Decoder;
using Spark.Network.Encoder;

namespace Spark.Network.Session
{
    public class RemoteSession : ISession
    {
        public event Action<string> PacketReceived;

        public Socket Socket { get; }
        
        public IEncoder Encoder { get; }
        public IDecoder Decoder { get; }
        public Func<string, string>[] Modifiers { get; set; }

        public RemoteSession(IEncoder encoder, IDecoder decoder)
        {
            Encoder = encoder;
            Decoder = decoder;

            Socket = new Socket(SocketType.Stream, ProtocolType.Tcp);
        }

        public void Connect(IPEndPoint ep)
        {
            Socket.Connect(ep);
            Task.Run(() =>
            {
                while (Socket.Connected)
                {
                    var buffer = new byte[Socket.ReceiveBufferSize];
                    int size = Socket.Receive(buffer, SocketFlags.None);

                    IEnumerable<string> decoded = Decoder.Decode(buffer, size);
                    foreach (string packet in decoded)
                    {
                        PacketReceived?.Invoke(packet);
                    }
                }
            });
        }
        
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

            byte[] encoded = Encoder.Encode(modified);
            Socket.Send(encoded);
        }

        public void Stop()
        {
            Socket.Disconnect(false);
        }
    }
}