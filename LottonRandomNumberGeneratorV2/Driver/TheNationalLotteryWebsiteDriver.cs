using LottonRandomNumberGeneratorV2.Extensions;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace LottonRandomNumberGeneratorV2.Driver
{
    public class TheNationalLotteryWebsiteDriver
    {
        const string HomeUrl = "https://www.national-lottery.co.uk";

        IWebDriver _driver;

        public TheNationalLotteryWebsiteDriver(IWebDriver driver)
        {
            this._driver = driver;
        }

        bool IsEnabled { get; set; } = false;

        public void StartBrowser()
        {
            this._driver = new ChromeDriver();
        }

        public void GoToHomePage()
        {
            if (this.IsEnabled)
            {
                this._driver.Navigate().GoToUrl(HomeUrl);
            }
        }

        public void AcceptCookies()
        {
            if (this.IsEnabled)
            {
                var cookiesElement = this._driver.FindElementWithRetry(By.CssSelector("#cuk_cookie_consent_content_inner > div.cuk.cuk_cookie_consent.cuk_cookie_consent_explicit_screen > div > div > div > div > div.cuk_cookie_consent_sitcky_buttons > a.cuk_btn.cuk_btn_primary.cuk_cookie_consent_accept_all"));
                cookiesElement.Click();
            }
        }

        public void GoToLoginPage()
        {
            if (this.IsEnabled)
            {
                this._driver.Navigate().GoToUrl(HomeUrl + "/sign-in");
            }
        }

        public void Login()
        {
            if (this.IsEnabled)
            {
                var elementUserName = this._driver.FindElementWithRetry(By.CssSelector("#form_username"));
                elementUserName.SendKeys("");

                var elementPassword = this._driver.FindElementWithRetry(By.CssSelector("#form_password"));
                elementPassword.SendKeys("");

                var elementDateOfBirthDay = Task.Run(() => this._driver.FindElementWithRetry(By.CssSelector("#form_dateOfBirth_day"), isIngnoredIfNotFound: true));
                var elementDateOfBirthMonth = Task.Run(() => this._driver.FindElementWithRetry(By.CssSelector("#form_dateOfBirth_month"), isIngnoredIfNotFound: true));
                var elementDateOfBirthYear = Task.Run(() => this._driver.FindElementWithRetry(By.CssSelector("#form_dateOfBirth_year"), isIngnoredIfNotFound: true));

                Task.WaitAll(elementDateOfBirthDay, elementDateOfBirthMonth, elementDateOfBirthYear);

                if (elementDateOfBirthDay.Result != null && elementDateOfBirthMonth.Result != null && elementDateOfBirthYear.Result != null)
                {
                    var selectDateOfBirthDay = new SelectElement(elementDateOfBirthDay.Result);
                    selectDateOfBirthDay.SelectByText("");

                    var selectDateOfBirthMonth = new SelectElement(elementDateOfBirthMonth.Result);
                    selectDateOfBirthMonth.SelectByText("");

                    var selectDateOfBirthYear = new SelectElement(elementDateOfBirthYear.Result);
                    selectDateOfBirthYear.SelectByText("");
                }

                var elementSubmitButton = this._driver.FindElementWithRetry(By.CssSelector("#login_submit_bttn"));
                elementSubmitButton.Click();
            }
        }

        public void GoToEuroMillionsPage()
        {
            if (this.IsEnabled)
            {
                this._driver.Navigate().GoToUrl(HomeUrl + "/games/euromillions");
            }
        }

        public void ChooseNumbersButton(int gameNumber)
        {
            if (this.IsEnabled)
            {
                this.FindElementAndJSClick($"#number_picker_initialiser_{gameNumber}");
            }
        }

        public void ChooseMainNumbers(params string[] numbers)
        {
            if (this.IsEnabled)
            {
                foreach (var number in numbers)
                {
                    this.FindElementAndJSClick($"#pool_0_label_ball_{number.Trim()}");
                }
            }
        }

        public void ChooseLuckyStartNumbers(params string[] numbers)
        {
            if (this.IsEnabled)
            {
                foreach (var number in numbers)
                {
                    this.FindElementAndJSClick($"#pool_1_label_ball_{number.Trim()}");
                }
            }
        }

        public void ConfirmButton()
        {
            if (this.IsEnabled)
            {
                this.FindElementAndJSClick("#number_selection_confirm_button");
            }
        }

        public void GoToGamePage(string url)
        {
            if (this.IsEnabled)
            {
                this._driver.Navigate().GoToUrl(url);
                this.FindElementAndJSClick("#euromillions_dbg_play_page");
            }
        }

        void FindElementAndJSClick(string cssSelector)
        {
            if (this.IsEnabled)
            {
                var element = this._driver.FindElementWithRetry(By.CssSelector(cssSelector));
                this._driver.JSClick(element);
            }
        }

        public void SetEnabledValue(bool isEnabled)
        {
            if(isEnabled)
            {
                this._driver = new ChromeDriver();
            }
            else
            {
                this._driver?.Quit();
            }

            this.IsEnabled = isEnabled;
        }
    }
}