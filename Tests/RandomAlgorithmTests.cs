﻿namespace Tests
{
    [TestClass]
    public class RandomAlgorithmTests
    {
        [TestMethod]
        public void Generate_SingleNumber_ReturnsNumberBetween1And45057474()
        {
            //Arrange
            int maxValue = 45057474;
            var al = new RandomAlgorithm();

            //Act
            var returnValue = al.Generate(maxValue, 1);

            //Assert
            Assert.IsNotNull(returnValue);
            Assert.AreEqual(1, returnValue.Count());
            Assert.IsTrue(returnValue[0][0] > 0);
            Assert.IsTrue(returnValue[0][0] <= maxValue);
        }
    }
}