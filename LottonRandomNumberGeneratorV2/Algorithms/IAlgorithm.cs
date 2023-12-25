public interface IAlgorithm
{
    ICollection<ICollection<int>> Generate(int maxNumber, int combinationLength);
}
