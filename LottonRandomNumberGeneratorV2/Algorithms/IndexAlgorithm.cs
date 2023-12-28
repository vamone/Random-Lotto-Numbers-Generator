namespace LottonRandomNumberGeneratorV2.Algorithms
{
    public class IndexAlgorithm : IAlgorithm
    {
        readonly IAlgorithm _combinationAlgorithm;

        public IndexAlgorithm(CombinationAlgorithm combinationAlgorithm)
        {
            this._combinationAlgorithm = combinationAlgorithm;
        }

        public AlgorithmType AlgorithmType => AlgorithmType.Index;

        public List<List<int>> Generate(int maxNumber, int combinationLength)
        {
            var combinationNumbers = this._combinationAlgorithm.Generate(maxNumber, combinationLength);

            var rand = new Random();
            int minValue = 1;
            int maxValue = combinationNumbers.Count();

            int randomNumber = rand.Next(minValue, maxValue);

            return new List<List<int>> { combinationNumbers[randomNumber].ToList() };
        }
    }
}