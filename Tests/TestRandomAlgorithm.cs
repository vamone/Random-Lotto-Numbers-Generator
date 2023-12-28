namespace Tests
{
    public class TestRandomAlgorithm : RandomAlgorithm
    {
        public AlgorithmType AlgorithmType { get; } = AlgorithmType.Random;

        public override List<List<int>> Generate(int maxNumber, int combinationLength)
        {
            return base.Generate(maxNumber, combinationLength);        
        }
    }
}