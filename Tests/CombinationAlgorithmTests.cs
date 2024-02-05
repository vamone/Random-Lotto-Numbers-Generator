namespace Tests
{
    [TestClass]
    public class CombinationAlgorithmTests
    {
        [TestMethod]
        public void Generate_SingleNumber_ReturnsNumberBetween1And45057474()
        {
            //Arrange
            int maxValue = 45057474; //2118760 // 2118760
            var al = new CombinationAlgorithm();

            //Act
            var returnValue = al.Generate(maxValue, 1);

            //Assert
            Assert.IsNotNull(returnValue);
            Assert.AreEqual(maxValue, returnValue.Count());
            Assert.IsTrue(returnValue[0][0] > 0);
            Assert.IsTrue(returnValue[0][0] <= maxValue);
        }
    }
}