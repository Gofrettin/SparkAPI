using System.Collections.Generic;

namespace Spark.Toolkit.Reader
{
    public class TextContent : TextRegion
    {
        public TextContent(IEnumerable<TextLine> lines) : base(lines)
        {
        }
    }
}