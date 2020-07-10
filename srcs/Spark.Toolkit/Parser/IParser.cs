using System.IO;

namespace Spark.Toolkit.Parser
{
    public interface IParser
    {
        void Parse(DirectoryInfo input, DirectoryInfo output);
    }
}