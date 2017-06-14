using Framework.Elements;
using Framework.PageObjects;
using OpenQA.Selenium;

namespace Framework.UnitTests.PageObjects
{
    public class PagelinkPage : BasePage
    {
        public string PageTitle = "Web-Test-Framework: Pagelink Page";

        private readonly By _successText = By.ClassName("bg-success");
        private readonly By _returnLink = By.Id("return-link-id");

        public TextField SuccessText => new TextField(Browser, _successText);
        public PageLink ReturnLink => new PageLink(Browser, _returnLink);

        public PagelinkPage(IWebDriver browser) : base(browser) { }
        public PagelinkPage(IWebDriver browser, bool trackBrowserLogs) : base(browser, trackBrowserLogs: trackBrowserLogs) { }
    }
}
