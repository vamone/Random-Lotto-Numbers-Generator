namespace Tests
{
    public class TestRandomAlgorithm : RandomAlgorithm
    {
        public AlgorithmType AlgorithmType { get; } = AlgorithmType.Random;

        public override Dictionary<int, List<int>> Generate(int maxNumber, int combinationLength)
        {
            return base.Generate(maxNumber, combinationLength);        
        }
    }
}