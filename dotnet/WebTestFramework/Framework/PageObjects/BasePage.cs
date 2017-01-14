using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;

namespace Framework.PageObjects
{
    public abstract class BasePage
    {
        protected static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        protected IWebDriver Browser { get; set; }
        protected WebDriverWait BrowserWait { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        
        public BasePage(IWebDriver driver)
        {
            Browser = driver;
            BrowserWait = new WebDriverWait(Browser, TimeSpan.FromSeconds(5));
            Title = driver.Title;
            Url = driver.Url;
        }

        public abstract bool IsLoaded();
    }
}
