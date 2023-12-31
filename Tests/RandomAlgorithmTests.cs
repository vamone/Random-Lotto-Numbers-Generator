using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
    [TestClass]
    public class RandomAlgorithmTests
    {
        [TestMethod]
        public void Generate_SingleNumber_ReturnsNumberBetween1And45057474()
        {
            //Arrange
            int maxValue = 45057474; //2118760 // 2118760
            //var al = new TestRandomAlgorithm();

            ////Act
            //var returnValue = al.Generate(maxValue, 1);

            ////Assert
            //Assert.IsTrue(returnValue[0][0] > 0);
            //Assert.IsTrue(returnValue[0][0] <= maxValue);
        }
    }
}