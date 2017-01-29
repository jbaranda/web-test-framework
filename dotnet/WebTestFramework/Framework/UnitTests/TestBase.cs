﻿using Framework.Browser;
using Framework.UnitTests.PageObjects;
using NUnit.Framework;
using System;
using System.Linq;

namespace Framework.UnitTests
{
    public class TestBase
    {
        protected static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        
        protected BaseBrowser Browser { get; set; }
        protected TestHtmlPage page => new TestHtmlPage(Browser.Driver);

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
    }
}