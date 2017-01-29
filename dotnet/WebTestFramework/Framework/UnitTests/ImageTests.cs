using Framework.Elements;
using NUnit.Framework;
using OpenQA.Selenium;

namespace Framework.UnitTests
{
    [TestFixture]
    [Parallelizable]
    public class ImageTests : TestBase
    {
        private readonly By _imageLocator = By.Id("imgId");
        public Image image => new Image(Browser.Driver, _imageLocator);

        [Test]
        public void NameTest()
        {
            Log.Info($"Element Under Test: {image}");
            Assert.That(image.Name, Is.EqualTo("imgName"));
        }

        [Test]
        public void ValueTest()
        {
            Log.Info($"Element Under Test: {image}");
            Assert.That(image.Value, Is.EqualTo("imgValue"));
        }

        [Test]
        public void TextTest()
        {
            Log.Info($"Element Under Test: {image}");
            Assert.That(image.Text, Is.EqualTo("imgValue"));
        }

        [Test]
        public void IsLoadedTest()
        {
            Log.Info($"Element Under Test: {image}");
            Image imageLoaded;
            Assert.DoesNotThrow(() => imageLoaded = image);
            Assert.That(image.IsLoaded(), Is.True);
        }

        [Test]
        [TestCase(true)]
        [TestCase(false)]
        public void IsVisibleTest(bool expand)
        {
            Log.Info($"Element Under Test: {image}");
            ExpandDiv(expand, ImageDivButton, ImageDiv);
            Assert.That(image.IsVisible(), Is.EqualTo(expand));
        }

        [Test]
        public void AltTest()
        {
            Log.Info($"Element Under Test: {image}");
            ExpandDiv(true, ImageDivButton, ImageDiv);
            Assert.That(image.Alt, Is.EqualTo("imgAlt"));
        }

        [Test]
        public void SrcTest()
        {
            Log.Info($"Element Under Test: {image}");
            ExpandDiv(true, ImageDivButton, ImageDiv);
            Assert.That(image.Src.Contains("images/image-4x.png"), Is.True);
        }
    }
}
