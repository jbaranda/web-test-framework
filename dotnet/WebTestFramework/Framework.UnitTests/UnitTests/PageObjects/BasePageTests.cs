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
            Assert.That(page.Url.Contains(TestPageUrl), Is.True);
        }

        [Test]
        public void TitleTest()
        {
            Assert.That(page.Title, Is.EqualTo(page.PageTitle));
        }

        [Test]
        public void IsLoadedTest()
        {
            Assert.That(page.IsLoaded(), Is.True);
        }
    }
}
