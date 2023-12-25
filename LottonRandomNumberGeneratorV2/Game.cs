﻿using LottonRandomNumberGeneratorV2.Enums;

namespace LottonRandomNumberGeneratorV2
{
    public class Game
    {
        public Game(int id, string name, int mainMaxNumber, int mainCombinatioLength, int bonusMaxNumber, int bonusCombinatioLength, AlgorithmType algorithmType = AlgorithmType.Combination)
        {
            this.Id = id;
            this.Name = name;

            this.MainMaxNumber = mainMaxNumber;
            this.MainCombinatioLength = mainCombinatioLength;

            this.BonusMaxNumber = bonusMaxNumber;
            this.BonusCombinatioLength = bonusCombinatioLength;

            this.Algorithm = algorithmType;
        }

        public int Id { get; private set; }

        public string Name { get; set; }    

        public int MainMaxNumber { get; set; }

        public int MainCombinatioLength { get; set; }

        public int BonusMaxNumber { get; set; }

        public int BonusCombinatioLength { get; set; }

        public int Take { get; set; } = 1;

        public AlgorithmType Algorithm { get; set; } = AlgorithmType.Combination;
    }
}