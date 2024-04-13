using LottonRandomNumberGeneratorV2;
using LottonRandomNumberGeneratorV2.Algorithms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
    [TestClass]
    public class GameBoxTests
    {
        readonly GameBox _gameBox;

        public GameBoxTests()
        {
            this._gameBox = new GameBox(Constraints.GetGameConfigs().Values, Constraints.GetAlgorithms().Values);
        }

        [TestMethod]
        public void Generate_SingleNumber_ReturnsNumberBetween1And450574742()
        {
            //Arrange & Act
            var returnValue = this._gameBox.GenerateNumbers(this._gameBox.GetGameByType(GameType.Setforlive), this._gameBox.GetAlgorithmByType(AlgorithmType.Combination), 3);

            //Assert
            Assert.IsNotNull(returnValue);
        }
    }
}
