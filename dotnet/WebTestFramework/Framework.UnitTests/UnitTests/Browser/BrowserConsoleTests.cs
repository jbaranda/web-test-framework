using Framework.Browser;
using Framework.UnitTests.PageObjects;
using NUnit.Framework;
using OpenQA.Selenium;
using System.Linq;

namespace Framework.UnitTests.Browser
{
    [TestFixture]
    [Parallelizable]
    public class BrowserConsoleTests : TestBase
    {
        [OneTimeSetUp]
        public override void SetupBrowser()
        {
            Log.Info($"START: {TestContext.CurrentContext.Test.ClassName}");
            if (BrowserType == BrowserType.IE || BrowserType == BrowserType.Firefox)
                Assert.Inconclusive($"Currently Browser Console Logs do not work as intended against {BrowserType} (Need to look into later)");
        }

        [SetUp]
        public override void TestStart()
        {
            Log.Info($"START: {TestContext.CurrentContext.Test.Name}");
        }

        [TearDown]
        public override void TestFinish()
        {
            Log.Info($"FINISH: {TestContext.CurrentContext.Test.Name}: {TestContext.CurrentContext.Result.Outcome.Status}");
            Browser.Driver.Quit();
        }

        [OneTimeTearDown]
        public override void CloseBrowser()
        {
            Log.Info($"EXECUTING: CloseBrowser()");
            var passCnt = $"PASSED={TestContext.CurrentContext.Result.PassCount}";
            var failCnt = $"FAILED={TestContext.CurrentContext.Result.FailCount}";
            var inconclusiveCnt = $"INCONCLUSIVE={TestContext.CurrentContext.Result.InconclusiveCount}";
            var skippedCnt = $"SKIPPED={TestContext.CurrentContext.Result.SkipCount}";
            var warningCnt = $"WARNING={TestContext.CurrentContext.Result.WarningCount}";

            Log.Info($"FINISH: {TestContext.CurrentContext.Test.ClassName}: {passCnt},{failCnt},{inconclusiveCnt},{skippedCnt},{warningCnt}");
        }


        private void SetupBrowserWithLoggingLevel(LogLevel level)
        {
            Browser = new BaseBrowser(WebDriverFactory.Factory.GetBrowser(BrowserType, logLevel: level));
            Browser.Driver.Navigate().GoToUrl(TestPageUrl);
            Log.Info($"Browser URL={Browser.Driver.Url}");
            page = new TestHtmlPage(Browser.Driver, true);
        }

        [Test]
        public void InfoTest()
        {
            SetupBrowserWithLoggingLevel(LogLevel.Info);
            page.ExpandDiv(DivSection.TestBrowserConsole, true);
            page.ConsoleInfoButton.Click();
            
            var infoLogs = Browser.Logs;

            Assert.That(infoLogs.Any, Is.True);
            Assert.That(infoLogs.Count, Is.EqualTo(1));
            Assert.That(infoLogs.First().Message, Does.Contain("console.info: triggerInfo() executed"));
        }

        [Test]
        public void WarnTest()
        {
            SetupBrowserWithLoggingLevel(LogLevel.Warning);
            page.ExpandDiv(DivSection.TestBrowserConsole, true);
            page.ConsoleWarnButton.Click();

            var warnLogs = Browser.Logs;

            Assert.That(warnLogs.Any, Is.True);
            Assert.That(warnLogs.Count, Is.EqualTo(1));
            Assert.That(warnLogs.First().Message, Does.Contain("console.warn: triggerWarn() executed"));
        }

        [Test]
        public void ErrorTest()
        {
            SetupBrowserWithLoggingLevel(LogLevel.Severe);
            page.ExpandDiv(DivSection.TestBrowserConsole, true);
            page.ConsoleErrorButton.Click();

            var errorLogs = Browser.Logs;

            Assert.That(errorLogs.Any, Is.True);
            Assert.That(errorLogs.Count, Is.EqualTo(1));
            Assert.That(errorLogs.First().Message, Does.Contain("console.error: triggerError() executed"));
        }
    }
}
