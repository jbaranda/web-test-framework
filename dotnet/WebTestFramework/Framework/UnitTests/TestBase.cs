using Framework.Browser;
using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Linq;

namespace Framework.UnitTests
{
    public class TestBase
    {
        protected static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        
        protected BaseBrowser Browser { get; set; }

        //Div Buttons Locators
        protected readonly By NavMenuDivButton = By.Id("nav-menu-btn");
        protected readonly By TextFieldDivButton = By.Id("text-field-btn");
        protected readonly By TextInputDivButton = By.Id("text-input-btn");
        protected readonly By SelectDivButton = By.Id("select-input-btn");
        protected readonly By CheckboxDivButton = By.Id("checkbox-input-btn");
        protected readonly By RadioDivButton = By.Id("radio-input-btn");
        protected readonly By ImageDivButton = By.Id("img-btn");

        //Collapsable Divs Locators
        protected readonly By NavMenuDiv = By.Id("navDivId");
        protected readonly By TextFieldDiv = By.Id("textDivContainer");
        protected readonly By TextInputDiv = By.Id("textInputDivContainer");
        protected readonly By SelectDiv = By.Id("selectDivContainer");
        protected readonly By CheckboxDiv = By.Id("checkboxDivContainer");
        protected readonly By RadioDiv = By.Id("radioDivContainer");
        protected readonly By ImageDiv = By.Id("imgDivContainer");

        protected static string GetTestHtmlFolderPath()
        {
            var pathSegments = new Uri(typeof(TestBase).Assembly.CodeBase).Segments;
            var filteredSegments = pathSegments.Take(pathSegments.Length - 6).Aggregate((current, next) => current + next);
            return filteredSegments.Substring(1, filteredSegments.Length - 1);
        }

        [OneTimeSetUp]
        public void SetupBrowser()
        {
            Log.Info($"START: {TestContext.CurrentContext.Test.ClassName}");
            Log.Info($"EXECUTING: SetupBrowser(): {BrowserType.Chrome}");
            Browser = new BaseBrowser(WebDriverFactory.GetBrowser(BrowserType.Chrome));
            Browser.Driver.Navigate().GoToUrl($"{GetTestHtmlFolderPath()}/TestHtml/testpage.html");
            Log.Info($"Browser URL={Browser.Driver.Url}");
        }

        [SetUp]
        public void LogTestStart()
        {
            Log.Info($"START: {TestContext.CurrentContext.Test.Name}");
        }

        [TearDown]
        public void LogTestFinish()
        {
            Log.Info($"FINISH: {TestContext.CurrentContext.Test.Name}: {TestContext.CurrentContext.Result.Outcome.Status}");
        }

        [OneTimeTearDown]
        public void CloseBrowser()
        {
            Log.Info($"EXECUTING: CloseBrowser()");
            Browser.Driver.Quit();
            var passCnt = $"PASSED={TestContext.CurrentContext.Result.PassCount}";
            var failCnt = $"FAILED={TestContext.CurrentContext.Result.FailCount}";
            var inconclusiveCnt = $"INCONCLUSIVE={TestContext.CurrentContext.Result.InconclusiveCount}";
            var skippedCnt = $"SKIPPED={TestContext.CurrentContext.Result.SkipCount}";
            var warningCnt = $"WARNING={TestContext.CurrentContext.Result.WarningCount}";

            Log.Info($"FINISH: {TestContext.CurrentContext.Test.ClassName}: {passCnt},{failCnt},{inconclusiveCnt},{skippedCnt},{warningCnt}");
        }

        protected void ExpandDiv(bool expand, By expandButtonSelector, By divContainerSelector)
        {
            var button = Browser.Driver.FindElement(expandButtonSelector);
            var div = Browser.Driver.FindElement(divContainerSelector);

            if (!expand && div.Displayed)
            {
                button.Click();
                Browser.DriverWait.Until(driver => div.GetAttribute("class").Equals("collapse"));
                Browser.DriverWait.Until(driver => !div.Displayed);
                return;
            }

            if (expand && !div.Displayed)
            {
                button.Click();
                Browser.DriverWait.Until(driver => div.GetAttribute("class").Equals("collapse in"));
                Browser.DriverWait.Until(driver => div.Displayed);
            }
        }
    }
}
