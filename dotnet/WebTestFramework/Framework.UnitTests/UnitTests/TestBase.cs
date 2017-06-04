using Framework.Browser;
using Framework.UnitTests.PageObjects;
using NUnit.Framework;
using System;
using System.Linq;

namespace Framework.UnitTests
{
    public class TestBase
    {
        protected static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        
        protected BrowserType BrowserType => BrowserType.Chrome;
        protected BaseBrowser Browser { get; set; }
        protected TestHtmlPage page { get; set; }

        protected string TestPageUrl => (BrowserType == BrowserType.Firefox ? "file:///" : string.Empty)
                + $"{ GetTestHtmlFolderPath()}TestHtml/testpage.html";

        protected string PageLinkPageUrl => (BrowserType == BrowserType.Firefox ? "file:///" : string.Empty)
                + $"{ GetTestHtmlFolderPath()}TestHtml/pagelink.html";

        protected static string GetTestHtmlFolderPath()
        {
            var pathSegments = new Uri(typeof(TestBase).Assembly.CodeBase).Segments;
            var filteredSegments = pathSegments.Take(pathSegments.Length - 6).Aggregate((current, next) => current + next);
            return filteredSegments.Substring(1, filteredSegments.Length - 1);
        }

        [OneTimeSetUp]
        public virtual void SetupBrowser()
        {
            Log.Info($"START: {TestContext.CurrentContext.Test.ClassName}");
            Log.Info($"EXECUTING: SetupBrowser(): {BrowserType}");
            Browser = new BaseBrowser(WebDriverFactory.Factory.GetBrowser(BrowserType));
            Browser.Driver.Navigate().GoToUrl(TestPageUrl);
            Log.Info($"Browser URL={Browser.Driver.Url}");
            page = new TestHtmlPage(Browser.Driver);
        }

        [SetUp]
        public virtual void TestStart()
        {
            Log.Info($"START: {TestContext.CurrentContext.Test.Name}");
        }

        [TearDown]
        public virtual void TestFinish()
        {
            Log.Info($"FINISH: {TestContext.CurrentContext.Test.Name}: {TestContext.CurrentContext.Result.Outcome.Status}");
        }

        [OneTimeTearDown]
        public virtual void CloseBrowser()
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
    }
}
