using Framework.Browser;
using Framework.Constants;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Framework.PageObjects
{
    public abstract class BasePage
    {
        protected static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public IWebDriver Browser { get; private set; }
        public WebDriverWait BrowserWait { get; private set; }
        public string Title { get; private set; }
        public string Url { get; private set; }
        public List<LogEntry> BrowserLogs { get; private set; }

        protected BasePage(IWebDriver driver, WebDriverWait driverWait = null, bool trackBrowserLogs = false)
        {
            Browser = driver;
            BrowserWait = driverWait != null ? driverWait : WebDriverWaitFactory.Factory.GetWait(Browser, TimeSpan.FromSeconds(WebDriverSettings.ExplicitWait));

            if (!IsLoaded())
            {
                var msg = $"{GetType().Name} was NOT loaded successfully";
                Log.Error(msg);
                throw new NoSuchElementException(msg);
            }

            Log.Info($"{GetType().Name} has loaded successfully");

            Title = driver.Title;
            Url = driver.Url;
            
            if (trackBrowserLogs)
                BrowserLogs = Browser.GetBrowserLogs().ToList();
        }

        public virtual bool IsLoaded()
        {
            var isLoadedWait = WebDriverWaitFactory.Factory.GetWait(Browser, TimeSpan.FromSeconds(WebDriverSettings.ExplicitWait),
                TimeSpan.FromSeconds(1), ignoreExceptions: new List<Type> { typeof(WebDriverTimeoutException) } );

            var isLoaded = isLoadedWait.Until(driver => ((IJavaScriptExecutor)driver)
            .ExecuteScript("return document.readyState").Equals(DocumentReadyState.Complete));

            Log.Debug($"{GetType().Name}: isLoaded={isLoaded}");
            return isLoaded;
        }

        public override string ToString()
        {
            return $"{GetType().Name}: Title={Title},Url={Url}";
        }
    }
}
