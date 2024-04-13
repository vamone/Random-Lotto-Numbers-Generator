using LottonRandomNumberGeneratorV2.Driver;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
    [TestClass]
    public class TheNationalLotteryWebsiteDriverTests
    {
        readonly TheNationalLotteryWebsiteDriver _website;

        public TheNationalLotteryWebsiteDriverTests()
        {
            this._website = this.GetTheNationalLotteryWebsite();
        }

        [TestMethod]
        public void MyTestMethod()
        { 
        }

        TheNationalLotteryWebsiteDriver GetTheNationalLotteryWebsite()
        {
            return new TheNationalLotteryWebsiteDriver(new ChromeDriver());
        }

        ~TheNationalLotteryWebsiteDriverTests()
        {
            //this._website.();
        }
    }
}