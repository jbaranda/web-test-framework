using Framework.Constants;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;

namespace Framework.PageObjects
{
    public abstract class BasePage
    {
        protected static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public IWebDriver Browser { get; set; }
        public WebDriverWait BrowserWait { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }

        protected BasePage(IWebDriver driver)
        {
            Browser = driver;
            BrowserWait = new WebDriverWait(Browser, TimeSpan.FromSeconds(5));
            Title = driver.Title;
            Url = driver.Url;

            if (!IsLoaded())
            {
                var msg = $"{GetType().Name} was NOT loaded successfully";
                Log.Error(msg);
                throw new NoSuchElementException(msg);
            }

            Log.Info($"{GetType().Name} has loaded successfully");
        }

        public virtual bool IsLoaded()
        {
            var readyState = ((IJavaScriptExecutor)Browser).ExecuteScript("return document.readyState");
            Log.Debug($"{GetType().Name}: document.readyState={readyState}");

            if (readyState.Equals(DocumentReadyState.Complete))
            {           
                return true;
            }

            return false;
        }
    }
}
