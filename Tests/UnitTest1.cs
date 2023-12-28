using LottonRandomNumberGeneratorV2.Extensions;
using System.Data;

namespace Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        [DataRow(45057474)]
        public void LottoNumberOfCombinations(int numberOfCombinations)
        {
            //Arrange & Act
            var numbers = this.GetLottoNumbers();

            //Assert
            Assert.AreEqual(numberOfCombinations, numbers.Count());
        }

        [TestMethod]
        [DataRow(2, 23, 24, 29, 33, 36, 8335461, "2023-12-27")]
        public void LottoWinningNumbers(int n1, int n2, int n3, int n4, int n5, int n6, int expectedIndex, string dateTime)
        {
            //Arrange
            var numbers = this.GetLottoNumbers();

            //Act
            int index = numbers.FindIndex(x => x.Contains(n1) && x.Contains(n2) && x.Contains(n3) && x.Contains(n4) && x.Contains(n5) && x.Contains(n6));

            //Assert
            Assert.AreEqual(expectedIndex, index);
        }

        [TestMethod]
        public void LottoNumberOfCombinations3()
        {
            //Arrange & Act
            var numbers = this.GetLottoNumbers();

            int totalCount = numbers.Count();

            int pageSize = 3;
            int size = totalCount / pageSize;

            var chunkedNumbers = numbers.Chunk(size);
            int controlCount = chunkedNumbers.Sum(x => x.Count());

            var bbb = chunkedNumbers.SelectMany(x => x.OrderBy(_ => Guid.NewGuid()).Take(1).ToList()).ToList();

            //Assert
            Assert.AreEqual(numbers.Count(), controlCount);
        }

        [TestMethod]
        public void TestMethod2()
        {
            var al1 = new RandomAlgorithm();

            var aaa = al1.Generate(9, 7);

            var bb = aaa.Select(x => x.Sum());

            var al = new CombinationAlgorithm();

            //var numbers = al.Generate(59, 6, 0);

            //int index = numbers.FindIndex(x => x.Contains(n1) && x.Contains(n2) && x.Contains(n3) && x.Contains(n4) && x.Contains(n5) && x.Contains(n6));

            //Assert.AreEqual(expectedIndex, index);
        }

        private List<List<int>> GetLottoNumbers()
        {
            var al = new CombinationAlgorithm();
            return al.Generate(59, 6);
        }
    }
}