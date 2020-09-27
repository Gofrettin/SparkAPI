namespace Spark.Gameforge.Nostale
{
    public class NostaleClientInfo
    {
        public string DxHash { get; set; }
        public string GlHash { get; set; }
        public string Version { get; set; }

        public override string ToString() => $"{nameof(DxHash)}: {DxHash}, {nameof(GlHash)}: {GlHash}, {nameof(Version)}: {Version}";
    }
}