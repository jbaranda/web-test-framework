using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;

namespace Framework.Browser
{
    public class BaseBrowser
    {
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public IWebDriver Driver { get; set; }
        public WebDriverWait DriverWait { get; set; }

        public BaseBrowser(IWebDriver driver)
        {
            Driver = driver;
            Driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(WebDriverSettings.ImplicitWait));
            DriverWait = new WebDriverWait(driver, TimeSpan.FromSeconds(WebDriverSettings.ExplicitWait));
        }
    }
}
