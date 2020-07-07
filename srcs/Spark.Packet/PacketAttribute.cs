using System;

namespace Spark.Packet
{
    [AttributeUsage(AttributeTargets.Class)]
    public class PacketAttribute : Attribute
    {
        public PacketAttribute(string header) => Header = header;
        public string Header { get; }
    }
}