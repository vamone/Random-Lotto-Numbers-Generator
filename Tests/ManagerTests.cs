using LottonRandomNumberGeneratorV2.Algorithms;
using LottonRandomNumberGeneratorV2.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
    [TestClass]
    public class ManagerTests
    {
        [TestMethod]
        public void FormatNumbersIntoString_ListOfNumbers_ReturnsFormattedNumbers()
        {
            //Arrange
            var manager = this.GetManager();

            var listMain = new List<List<int>>()
            {
                new List<int>
                {
                    1, 2, 3, 4, 5
                }
            };

            var listBonus = new List<List<int>>()
            {
                new List<int>
                {
                    1, 2
                }
            };

            //Act
            string returnValue = manager.FormatNumbersIntoString(listMain, listBonus);

            //Assert
            Assert.IsNotNull(returnValue);
            Assert.AreEqual("1-2-3-4-5 | 1-2", returnValue);
        }

        [TestMethod]
        public void ManageNumbers_RandomAlgorithm_ReturnsValidNumbers()
        {
            //Arrange
            var manager = this.GetManager();
            var game = new Game(AlgorithmType.Random) { MainCombinatioLength = 5, BonusCombinatioLength = 2 };

            //Act 
            var numbers = manager.ManageNumbers(game);

            //Assert
            Assert.IsNotNull(numbers);
            Assert.AreEqual(1, numbers.Count());
            Assert.AreEqual("1-2-3-4-5 | 1-2", numbers[0]);
        }

        [TestMethod]
        public void ManageNumbers_RandomAlgorithmTake3_Returns3FormatedNumbers()
        {
            //Arrange
            var manager = this.GetManager();
            var game = new Game(AlgorithmType.Random) { MainCombinatioLength = 5, BonusCombinatioLength = 2, Take = 3 };

            //Act 
            var numbers = manager.ManageNumbers(game);

            //Assert
            Assert.IsNotNull(numbers);
            Assert.AreEqual(3, numbers.Count());
            Assert.AreEqual("1-2-3-4-5 | 1-2", numbers[0]);
            Assert.AreEqual("1-2-3-4-5 | 1-2", numbers[1]);
            Assert.AreEqual("1-2-3-4-5 | 1-2", numbers[2]);
        }

        [TestMethod]
        public void ManageNumbers_CombinationAlgorithm_ReturnsValidNumbers()
        {
            //Arrange
            var manager = this.GetManager();
            var game = new Game(AlgorithmType.Combination) { MainCombinatioLength = 5, BonusCombinatioLength = 2 };

            //Act 
            var numbers = manager.ManageNumbers(game);

            //Assert
            Assert.IsNotNull(numbers);
            Assert.AreEqual(1, numbers.Count());
            Assert.AreEqual("1-2-3-4-5 | 1-2", numbers[0]);
        }

        [TestMethod]
        public void ManageNumbers_CombinationAlgorithmTake3_Returns3FormatedNumbers()
        {
            //Arrange
            var manager = this.GetManager();
            var game = new Game(AlgorithmType.Combination) { MainCombinatioLength = 5, BonusCombinatioLength = 2, Take = 3 };

            //Act 
            var numbers = manager.ManageNumbers(game);

            //Assert
            Assert.IsNotNull(numbers);
            Assert.AreEqual(3, numbers.Count());
            Assert.AreEqual("1-2-3-4-5 | 1-2", numbers[0]);
            Assert.AreEqual("1-2-3-4-5 | 1-2", numbers[1]);
            Assert.AreEqual("1-2-3-4-5 | 1-2", numbers[2]);
        }

        [TestMethod]
        public void ManageNumbers_CombinationAlgorithmChunkInto3_Returns3FormatedNumbers()
        {
            //Arrange
            var manager = this.GetManager();
            var game = new Game(AlgorithmType.Combination) { MainCombinatioLength = 5, BonusCombinatioLength = 2, ChunkInto = 3 };

            //Act 
            var numbers = manager.ManageNumbers(game);

            //Assert
            Assert.IsNotNull(numbers);
            Assert.AreEqual(3, numbers.Count());
            Assert.AreEqual("1-2-3-4-5 | 1-2", numbers[0]);
            Assert.AreEqual("1-2-3-4-5 | 1-2", numbers[1]);
            Assert.AreEqual("1-2-3-4-5 | 1-2", numbers[2]);
        }

        [TestMethod]
        public void ManageNumbers_CombinationAlgorithmChunkInto3_Returns3FormatedNumbers2()
        {
            //Arrange
            var manager = this.GetManager();
            var game = new Game(AlgorithmType.Index) { MainCombinatioLength = 5, Take = 3 };

            //Act 
            var numbers = manager.ManageNumbers(game);

            //Assert
            Assert.IsNotNull(numbers);
            Assert.AreEqual(3, numbers.Count());
            Assert.AreEqual("1-2-3-4-5", numbers[0]);
            Assert.AreEqual("1-2-3-4-5", numbers[1]);
            Assert.AreEqual("1-2-3-4-5", numbers[2]);
        }

        private Manager GetManager()
        {
            return new Manager(new List<IAlgorithm>
            {
                new TestRandomAlgorithm(),
                new TestCombinationAlgorithm(),
                new IndexAlgorithm(new TestCombinationAlgorithm())
            });
        }
    }
}