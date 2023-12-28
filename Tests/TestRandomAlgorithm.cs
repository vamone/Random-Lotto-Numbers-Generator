namespace Tests
{
    public class TestRandomAlgorithm : IAlgorithm
    {
        public AlgorithmType AlgorithmType { get; } = AlgorithmType.Random;

        public List<List<int>> Generate(int maxNumber, int combinationLength)
        {
            return new List<List<int>> 
            { 
                Enumerable.Range(1, combinationLength).ToList() 
            };
        }
    }
}