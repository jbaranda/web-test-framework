using Framework.Elements;
using NUnit.Framework;
using OpenQA.Selenium;
namespace Framework.UnitTests
{
    [TestFixture]
    [Parallelizable]
    public class TextFieldTests : TestBase
    {
        private readonly By _textLocator = By.Id("textId");
        public TextField textField => new TextField(Browser.Driver, _textLocator);

        [SetUp]
        public void LogTextFieldString()
        {
            Log.Info($"Element Under Test: {textField}");
        }

        [Test]
        public void NameTest()
        {
            Assert.That(textField.Name, Is.EqualTo("textName"));
        }

        [Test]
        public void ValueTest()
        {
            Assert.That(textField.Value, Is.EqualTo("textValue"));
        }

        [Test]
        public void TextTest()
        {
            Assert.That(textField.Text, Is.EqualTo("Text"));
        }

        [Test]
        public void IsLoadedTest()
        {
            TextField textFieldLoaded;
            Assert.DoesNotThrow(() => textFieldLoaded = textField);
            Assert.That(textField.IsLoaded(), Is.True);
        }

        [Test]
        [TestCase(true)]
        [TestCase(false)]
        public void IsVisibleTest(bool expand)
        {
            ExpandDiv(expand, TextFieldDivButton, TextFieldDiv);
            Assert.That(textField.IsVisible(), Is.EqualTo(expand));
        }
    }
}
