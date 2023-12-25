namespace LottonRandomNumberGeneratorV2
{
    public class GameInput
    {
        public string GameName { get; set; }    

        public int MainMaxNumber { get; set; }

        public int MainCombinatioLength { get; set; }

        public int BonusMaxNumber { get; set; }

        public int BonusCombinatioLength { get; set; }

        public int Take { get; set; } = 1;

        public AlgorithmType Algorithm { get; set; } = AlgorithmType.Combination;
    }
}