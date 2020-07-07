namespace Spark.Toolkit
{
    public interface IDirectoryParser
    {
        string Name { get; }
        void Parse(string input, string output);
    }
}