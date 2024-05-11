namespace LottonRandomNumberGeneratorV2.Models
{
    public interface INumetricConfig
    {
        int Number { get; set; }
    }

    public class NumetricConfig : INumetricConfig
    {
        public int Number { get; set; }
    }
}