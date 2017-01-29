using Framework.Elements;
using NUnit.Framework;
namespace Framework.UnitTests
{
    [TestFixture]
    [Parallelizable]
    public class TextFieldTests : TestBase
    {
        [SetUp]
        public void LogTextFieldString()
        {
            Log.Info($"Element Under Test: {page.TextField}");
        }

        [Test]
        public void NameTest()
        {
            Assert.That(page.TextField.Name, Is.EqualTo("textName"));
        }

        [Test]
        public void ValueTest()
        {
            Assert.That(page.TextField.Value, Is.EqualTo("textValue"));
        }

        [Test]
        public void TextTest()
        {
            Assert.That(page.TextField.Text, Is.EqualTo("Text"));
        }

        [Test]
        public void IsLoadedTest()
        {
            TextField textFieldLoaded;
            Assert.DoesNotThrow(() => textFieldLoaded = page.TextField);
            Assert.That(page.TextField.IsLoaded(), Is.True);
        }

        [Test]
        [TestCase(true)]
        [TestCase(false)]
        public void IsVisibleTest(bool expand)
        {
            page.ExpandDiv(expand, TextFieldDivButton, TextFieldDiv);
            Assert.That(page.TextField.IsVisible(), Is.EqualTo(expand));
        }
    }
}
