namespace Spark.Core.Extension
{
    public static class BoolExtensions
    {
        public static bool Reverse(this bool value)
        {
            return !value;
        }
    }
}