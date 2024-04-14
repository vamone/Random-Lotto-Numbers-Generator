using LottonRandomNumberGeneratorV2.Configs;
using LottonRandomNumberGeneratorV2.Extensions;
using Microsoft.Extensions.Options;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace LottonRandomNumberGeneratorV2.Driver
{
    public class TheNationalLotteryWebsiteDriver
    {
        const string HomeUrl = "https://www.national-lottery.co.uk";

        IWebDriver _driver;

        readonly IOptions<LoginConfig> _loginConfig;

        public TheNationalLotteryWebsiteDriver(IWebDriver driver, IOptions<LoginConfig> loginConfig)
        {
            this._driver = driver;
            this._loginConfig = loginConfig;
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
                elementUserName.SendKeys(this._loginConfig.Value.Email);

                var elementPassword = this._driver.FindElementWithRetry(By.CssSelector("#form_password"));
                elementPassword.SendKeys(this._loginConfig.Value.Password);

                var elementDateOfBirthDay = Task.Run(() => this._driver.FindElementWithRetry(By.CssSelector("#form_dateOfBirth_day"), isIngnoredIfNotFound: true));
                var elementDateOfBirthMonth = Task.Run(() => this._driver.FindElementWithRetry(By.CssSelector("#form_dateOfBirth_month"), isIngnoredIfNotFound: true));
                var elementDateOfBirthYear = Task.Run(() => this._driver.FindElementWithRetry(By.CssSelector("#form_dateOfBirth_year"), isIngnoredIfNotFound: true));

                Task.WaitAll(elementDateOfBirthDay, elementDateOfBirthMonth, elementDateOfBirthYear);

                if (elementDateOfBirthDay.Result != null && elementDateOfBirthMonth.Result != null && elementDateOfBirthYear.Result != null)
                {
                    var selectDateOfBirthDay = new SelectElement(elementDateOfBirthDay.Result);
                    selectDateOfBirthDay.SelectByText(this._loginConfig.Value.DateOfBirth.Day.ToString());

                    var selectDateOfBirthMonth = new SelectElement(elementDateOfBirthMonth.Result);
                    selectDateOfBirthMonth.SelectByText(this._loginConfig.Value.DateOfBirth.Month);

                    var selectDateOfBirthYear = new SelectElement(elementDateOfBirthYear.Result);
                    selectDateOfBirthYear.SelectByText(this._loginConfig.Value.DateOfBirth.Year.ToString());
                }

                var elementSubmitButton = this._driver.FindElementWithRetry(By.CssSelector("#login_submit_bttn"));
                elementSubmitButton.Click();
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
                this.FindElementAndJSClick("#euromillions_dbg_play_page", isIgnoredIfNotFound: true);
                this.FindElementAndJSClick("#js-cuk-htp-modal > div.cuk-htp-first-look-modal__inner > a", isIgnoredIfNotFound: true);
            }
        }

        void FindElementAndJSClick(string cssSelector, bool isIgnoredIfNotFound = false)
        {
            if (this.IsEnabled)
            {
                var element = this._driver.FindElementWithRetry(By.CssSelector(cssSelector), isIngnoredIfNotFound: isIgnoredIfNotFound);
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