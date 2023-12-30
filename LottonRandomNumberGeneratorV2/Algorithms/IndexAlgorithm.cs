namespace LottonRandomNumberGeneratorV2.Algorithms
{
    public class IndexAlgorithm : IAlgorithm
    {
        readonly IAlgorithm _combinationAlgorithm;

        readonly IAlgorithm _randomAlgorithm;

        public IndexAlgorithm(CombinationAlgorithm combinationAlgorithm, RandomAlgorithm randomAlgorithm)
        {
            this._combinationAlgorithm = combinationAlgorithm;
            this._randomAlgorithm = randomAlgorithm;
        }

        public AlgorithmType Type => AlgorithmType.Index;

        public List<List<int>> Generate(int maxNumber, int combinationLength)
        {
            if (maxNumber <= 0 || combinationLength <= 0)
            {
                return new List<List<int>>();
            }

            var combinationNumbers = this._combinationAlgorithm.Generate(maxNumber, combinationLength);

            int randomNumber = this._randomAlgorithm.Generate(combinationNumbers.Count(), 1)[0][0];

            return new List<List<int>> { combinationNumbers[randomNumber].ToList() };
        }
    }
}