using System.Collections.Generic;
using Spark.Gameforge.Nostale;

namespace Spark.Gameforge.Nostale
{
    public class NostalePatch
    {
        public List<NostaleFile> Entries { get; set; }
        public long TotalSize { get; set; }
        public int Build { get; set; }
    }
}