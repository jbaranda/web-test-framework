using Framework.Elements;
using Framework.UnitTests.PageObjects;
using NUnit.Framework;

namespace Framework.UnitTests
{
    [TestFixture]
    [Parallelizable]
    public class ImageTests : TestBase
    {
        [Test]
        public void NameTest()
        {
            Log.Info($"Element Under Test: {page.Image}");
            Assert.That(page.Image.Name, Is.EqualTo("imgName"));
        }

        [Test]
        public void ValueTest()
        {
            Log.Info($"Element Under Test: {page.Image}");
            Assert.That(page.Image.Value, Is.EqualTo("imgValue"));
        }

        [Test]
        public void TextTest()
        {
            Log.Info($"Element Under Test: {page.Image}");
            Assert.That(page.Image.Text, Is.EqualTo("imgValue"));
        }

        [Test]
        public void IsLoadedTest()
        {
            Log.Info($"Element Under Test: {page.Image}");
            Image imageLoaded;
            Assert.DoesNotThrow(() => imageLoaded = page.Image);
            Assert.That(page.Image.IsLoaded(), Is.True);
        }

        [Test]
        [TestCase(true)]
        [TestCase(false)]
        public void IsVisibleTest(bool expand)
        {
            Log.Info($"Element Under Test: {page.Image}");
            page.ExpandDiv(DivSection.TestImage, expand);
            Assert.That(page.Image.IsVisible(), Is.EqualTo(expand));
        }

        [Test]
        public void AltTest()
        {
            Log.Info($"Element Under Test: {page.Image}");
            page.ExpandDiv(DivSection.TestImage, true);
            Assert.That(page.Image.Alt, Is.EqualTo("imgAlt"));
        }

        [Test]
        public void SrcTest()
        {
            Log.Info($"Element Under Test: {page.Image}");
            page.ExpandDiv(DivSection.TestImage, true);
            Assert.That(page.Image.Src.Contains("images/image-4x.png"), Is.True);
        }
    }
}
