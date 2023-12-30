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

        public Dictionary<int, List<int>> Generate(int maxNumber, int combinationLength)
        {
            var returnValue = new Dictionary<int, List<int>>();

            if (maxNumber <= 0 || combinationLength <= 0)
            {
                return returnValue;
            }

            var combinationNumbers = this._combinationAlgorithm.Generate(maxNumber, combinationLength);

            int randomNumber = this._randomAlgorithm.Generate(combinationNumbers.Count(), 1)[0][0];

            returnValue.Add(0, combinationNumbers[randomNumber]);

            return returnValue;
        }
    }
}