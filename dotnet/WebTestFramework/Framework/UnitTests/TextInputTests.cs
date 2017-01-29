using Framework.Elements;
using NUnit.Framework;
using OpenQA.Selenium;

namespace Framework.UnitTests
{
    [TestFixture]
    [Parallelizable]
    public class TextInputTests : TestBase
    {
        private readonly By _textInputLocator = By.Id("inputId");
        private readonly By _textareaLocator = By.Id("textareaId");
        public TextInput textInput => new TextInput(Browser.Driver, _textInputLocator);
        public TextInput textArea => new TextInput(Browser.Driver, _textareaLocator);

        [Test]
        public void NameTest()
        {
            Log.Info($"Element Under Test: {textInput}");
            Assert.That(textInput.Name, Is.EqualTo("inputName"));

            Log.Info($"Element Under Test: {textArea}");
            Assert.That(textArea.Name, Is.EqualTo("textareaName"));
        }

        [Test]
        public void IsLoadedTest()
        {
            Log.Info($"Element Under Test: {textInput}");
            TextInput textInputLoaded;
            Assert.DoesNotThrow(() => textInputLoaded = textInput);
            Assert.That(textInput.IsLoaded(), Is.True);

            Log.Info($"Element Under Test: {textArea}");
            TextInput _textAreaLoaded;
            Assert.DoesNotThrow(() => _textAreaLoaded = textArea);
            Assert.That(textArea.IsLoaded(), Is.True);
        }

        [Test]
        [TestCase(true)]
        [TestCase(false)]
        public void IsVisibleTest(bool expand)
        {
            ExpandDiv(expand, TextInputDivButton, TextInputDiv);
            Log.Info($"Element Under Test: {textInput}");
            Assert.That(textInput.IsVisible(), Is.EqualTo(expand));

            Log.Info($"Element Under Test: {textArea}");
            Assert.That(textArea.IsVisible(), Is.EqualTo(expand));
        }

        [Test]
        public void EnterText()
        {
            var text = "test";
            ExpandDiv(true, TextInputDivButton, TextInputDiv);

            Log.Info($"Element Under Test: {textInput}");
            textInput.EnterText(text);
            Assert.That(textInput.Text, Is.EqualTo(text));
            Assert.That(textInput.Value, Is.EqualTo(text));

            Log.Info($"Element Under Test: {textArea}");
            textArea.EnterText(text);
            Assert.That(textArea.Text, Is.EqualTo(text));
            Assert.That(textArea.Value, Is.EqualTo(text));
        }

        [Test]
        public void ClearText()
        {
            ExpandDiv(true, TextInputDivButton, TextInputDiv);

            Log.Info($"Element Under Test: {textInput}");
            textInput.ClearText();
            Assert.That(textInput.Text, Is.EqualTo(string.Empty));
            Assert.That(textInput.Value, Is.EqualTo(string.Empty));

            Log.Info($"Element Under Test: {textArea}");
            textArea.ClearText();
            Assert.That(textArea.Text, Is.EqualTo(string.Empty));
            Assert.That(textArea.Value, Is.EqualTo(string.Empty));
        }
    }
}
