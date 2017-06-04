using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Framework.Browser
{
    public class WebDriverWaitFactory
    {
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private WebDriverWaitFactory() { }

        public static WebDriverWaitFactory Factory => new WebDriverWaitFactory();

        public WebDriverWait GetWait(IWebDriver _driver, TimeSpan? timeout = null, TimeSpan? pollingInverval = null, TimeSpan? delayBy = null, string message = null, IList<Type> ignoreExceptions = null)
        {
            var instance = new WebDriverWait(_driver, timeout ?? TimeSpan.FromSeconds(WebDriverSettings.ExplicitWait));

            if (pollingInverval.HasValue)
                instance.PollingInterval = pollingInverval.Value;
            if (message != null)
                instance.Message = message;
            if (ignoreExceptions != null && ignoreExceptions.Any())
                instance.IgnoreExceptionTypes(ignoreExceptions.ToArray());
            if (delayBy.HasValue)
                GetWait(_driver, delayBy.Value, ignoreExceptions: ignoreExceptions).Until(browser => false);
            return instance;
        }
    }
}
