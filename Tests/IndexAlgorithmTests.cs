using LottonRandomNumberGeneratorV2.Algorithms;

namespace Tests
{
    [TestClass]
    public class IndexAlgorithmTests
    {
        [TestMethod]
        public void MyTestMethod()
        {
            //Arrange
            var al = this.GetAlgorithm();

            //Act
            var returnValue = al.Generate(50, 5);

            //Assert
            Assert.IsNotNull(returnValue);
            Assert.AreEqual(1, returnValue.Count());
            Assert.AreEqual(5, returnValue[0].Count());
        }

        private IAlgorithm GetAlgorithm()
        {
            return new IndexAlgorithm(new TestCombinationAlgorithm(), new TestRandomAlgorithm());
        }
    }
}