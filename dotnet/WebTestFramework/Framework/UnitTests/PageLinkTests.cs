using Framework.Elements;
using Framework.UnitTests.PageObjects;
using NUnit.Framework;

namespace Framework.UnitTests
{
    [TestFixture]
    [Parallelizable]
    public class PageLinkTests : TestBase
    {
        [Test]
        public void NameTest()
        {
            Log.Info($"Element Under Test: {page.PageLink}");
            Assert.That(page.PageLink.Name, Is.EqualTo("textLinkName"));
        }

        [Test]
        public void ValueTest()
        {
            Log.Info($"Element Under Test: {page.PageLink}");
            Assert.That(page.PageLink.Value, Is.Null);
        }

        [Test]
        public void TextTest()
        {
            Log.Info($"Element Under Test: {page.PageLink}");
            Assert.That(page.PageLink.Text, Is.EqualTo("Text Link"));
        }

        [Test]
        public void IsLoadedTest()
        {
            Log.Info($"Element Under Test: {page.PageLink}");
            PageLink pageLinkLoaded;
            Assert.DoesNotThrow(() => pageLinkLoaded = page.PageLink);
            Assert.That(page.PageLink.IsLoaded(), Is.True);
        }

        [Test]
        [TestCase(true)]
        [TestCase(false)]
        public void IsVisibleTest(bool expand)
        {
            Log.Info($"Element Under Test: {page.PageLink}");
            page.ExpandDiv(DivSection.TestTextField, expand);
            Assert.That(page.PageLink.IsVisible(), Is.EqualTo(expand));
        }

        [Test]
        public void HrefTest()
        {
            Assert.That(page.PageLink.Href.Contains(TestHtmlPage.PagelinkHref), Is.True);
        }

        [Test]
        public void ClickToTest()
        {
            Log.Info($"Element Under Test: {page.PageLink}");
            var href = $"{GetTestHtmlFolderPath()}/TestHtml/pagelink.html";
            page.ExpandDiv(DivSection.TestTextField, true);

            var pagelinkPage = page.PageLink.ClickTo<PagelinkPage>();
            Assert.That(pagelinkPage.Url.Contains(href), Is.True);
            Assert.That(pagelinkPage.Title, Is.EqualTo("Web-Test-Framework: Pagelink Page"));
            Assert.That(pagelinkPage.SuccessText.IsVisible(), Is.True);

            pagelinkPage.ReturnLink.Click();

            Log.Info($"Element Under Test: {page.ImageLink}");
            page.ExpandDiv(DivSection.TestImage, true);

            pagelinkPage = page.ImageLink.ClickTo<PagelinkPage>();
            Assert.That(pagelinkPage.Url.Contains(href), Is.True);
            Assert.That(pagelinkPage.Title, Is.EqualTo("Web-Test-Framework: Pagelink Page"));
            Assert.That(pagelinkPage.SuccessText.IsVisible(), Is.True);

            pagelinkPage.ReturnLink.Click();
        }
    }
}
