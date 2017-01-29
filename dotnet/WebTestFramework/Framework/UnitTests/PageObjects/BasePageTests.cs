using NUnit.Framework;

namespace Framework.UnitTests.PageObjects
{
    [TestFixture]
    [Parallelizable]
    public class BasePageTests : TestBase
    {
        [SetUp]
        public void LogPageObject()
        {
            Log.Info($"PageObject Under Test: {page}");
        }

        [Test]
        public void UrlTest()
        {
            var href = $"{GetTestHtmlFolderPath()}/TestHtml/testpage.html";
            Assert.That(page.Url.Contains(href), Is.True);
        }

        [Test]
        public void TitleTest()
        {
            Assert.That(page.Title, Is.EqualTo("Web-Test-Framework: Test HTML Page"));
        }

        [Test]
        public void IsLoadedTest()
        {
            Assert.That(page.IsLoaded(), Is.True);
        }
    }
}
