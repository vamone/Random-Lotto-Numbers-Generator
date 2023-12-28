namespace Tests
{
    public class TestCombinationAlgorithm : CombinationAlgorithm
    {
        public AlgorithmType AlgorithmType { get; } = AlgorithmType.Combination;

        public override List<List<int>> Generate(int maxNumber, int combinationLength)
        {
            return new List<List<int>> 
            { 
                Enumerable.Range(1, combinationLength).ToList(), 
                Enumerable.Range(1, combinationLength).ToList(), 
                Enumerable.Range(1, combinationLength).ToList(), 
                Enumerable.Range(1, combinationLength).ToList(), 
                Enumerable.Range(1, combinationLength).ToList() 
            };
        }
    }
}