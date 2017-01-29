using Framework.Elements;
using Framework.UnitTests.PageObjects;
using NUnit.Framework;
using OpenQA.Selenium;

namespace Framework.UnitTests
{
    [TestFixture]
    [Parallelizable]
    public class PageLinkTests : TestBase
    {
        private const string PagelinkHref = "pagelink.html";
        private readonly By _pagelinkLocator = By.Id("textLinkId");
        private readonly By _imageLinkLocator = By.Id("imgLinkId");
        public PageLink pageLink => new PageLink(Browser.Driver, _pagelinkLocator);
        public Image imageLink => new Image(Browser.Driver, _imageLinkLocator);

        [Test]
        public void NameTest()
        {
            Log.Info($"Element Under Test: {pageLink}");
            Assert.That(pageLink.Name, Is.EqualTo("textLinkName"));
        }

        [Test]
        public void ValueTest()
        {
            Log.Info($"Element Under Test: {pageLink}");
            Assert.That(pageLink.Value, Is.Null);
        }

        [Test]
        public void TextTest()
        {
            Log.Info($"Element Under Test: {pageLink}");
            Assert.That(pageLink.Text, Is.EqualTo("Text Link"));
        }

        [Test]
        public void IsLoadedTest()
        {
            Log.Info($"Element Under Test: {pageLink}");
            PageLink pageLinkLoaded;
            Assert.DoesNotThrow(() => pageLinkLoaded = pageLink);
            Assert.That(pageLink.IsLoaded(), Is.True);
        }

        [Test]
        [TestCase(true)]
        [TestCase(false)]
        public void IsVisibleTest(bool expand)
        {
            Log.Info($"Element Under Test: {pageLink}");
            ExpandDiv(expand, TextFieldDivButton, TextFieldDiv);
            Assert.That(pageLink.IsVisible(), Is.EqualTo(expand));
        }

        [Test]
        public void HrefTest()
        {
            Assert.That(pageLink.Href.Contains(PagelinkHref), Is.True);
        }

        [Test]
        public void ClickToTest()
        {
            Log.Info($"Element Under Test: {pageLink}");
            var href = $"{GetTestHtmlFolderPath()}/TestHtml/pagelink.html";
            ExpandDiv(true, TextFieldDivButton, TextFieldDiv);

            var pagelinkPage = pageLink.ClickTo<PagelinkPage>();
            Assert.That(pagelinkPage.Url.Contains(href), Is.True);
            Assert.That(pagelinkPage.SuccessText.IsVisible(), Is.True);

            pagelinkPage.ReturnLink.Click();

            Log.Info($"Element Under Test: {imageLink}");
            ExpandDiv(true, ImageDivButton, ImageDiv);

            pagelinkPage = imageLink.ClickTo<PagelinkPage>();
            Assert.That(pagelinkPage.Url.Contains(href), Is.True);
            Assert.That(pagelinkPage.SuccessText.IsVisible(), Is.True);

            pagelinkPage.ReturnLink.Click();
        }
    }
}
