using LottonRandomNumberGeneratorV2;
using LottonRandomNumberGeneratorV2.Enums;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
    [TestClass]
    public class LottoEngineTests
    {
        readonly LottoEngine _engine;

        public LottoEngineTests()
        {
            this._engine = new LottoEngine(null, null);
        }

        [TestMethod]
        public void MyTestMethod()
        {
            //Arrange
            var mockGame = new Mock<IGameConfig>();
            mockGame.Setup(x => x.Sets).Returns(new List<ISetConfig> {  new SetConfig(GameSetType.Main, 39, 5), new SetConfig(GameSetType.Thunderball, 14, 1) });
      
            var mockAlgorithm = new Mock<IAlgorithm>();
            mockAlgorithm.Setup(x => x.Type).Returns(AlgorithmType.Combination);
            mockAlgorithm.Setup(x => x.Generate(It.IsAny<int>(), It.IsAny<int>())).Returns(this.GetTestNumbers());

            //Act
            var numbers = this._engine.GenerateNumbers(mockGame.Object, mockAlgorithm.Object, 10);


            //Assert
            Assert.IsNotNull(numbers);
        }

        List<List<int>> GetTestNumbers()
        {
            var result = new List<List<int>>();

            for (var i = 0; i < 10; i++)
            {
                var list = new List<int>();

                for (int j = 0; j < 10; j++)
                {
                    list.Add(j);
                }

                result.Add(list);
            }

            return result;
        }
    }
}