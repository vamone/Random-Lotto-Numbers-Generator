namespace LottonRandomNumberGeneratorV2
{
    public class WriteLineConfig
    {
        public WriteLineConfig(string printValue, bool isWriteLine = true, WriteLineSpacers lineSpacers = WriteLineSpacers.None)
        {
            this.Value = printValue;
            this.IsWriteLine = isWriteLine;
            this.LineSpacers = lineSpacers;
        }

        public string Value { get; set; }

        public bool IsWriteLine { get; set; }

        public WriteLineSpacers LineSpacers { get; set; }
    }
}

public enum WriteLineSpacers
{
    None = 0,
    Before = 1,
    After = 2,
    Both = 3
}