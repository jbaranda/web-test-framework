using Framework.Elements;
using Framework.UnitTests.PageObjects;
using NUnit.Framework;

namespace Framework.UnitTests
{
    [TestFixture]
    [Parallelizable]
    public class TextInputTests : TestBase
    {
        [Test]
        public void NameTest()
        {
            Log.Info($"Element Under Test: {page.TextInput}");
            Assert.That(page.TextInput.Name, Is.EqualTo("inputName"));

            Log.Info($"Element Under Test: {page.TextArea}");
            Assert.That(page.TextArea.Name, Is.EqualTo("textareaName"));
        }

        [Test]
        public void IsLoadedTest()
        {
            Log.Info($"Element Under Test: {page.TextInput}");
            TextInput textInputLoaded;
            Assert.DoesNotThrow(() => textInputLoaded = page.TextInput);
            Assert.That(page.TextInput.IsLoaded(), Is.True);

            Log.Info($"Element Under Test: {page.TextArea}");
            TextInput _textAreaLoaded;
            Assert.DoesNotThrow(() => _textAreaLoaded = page.TextArea);
            Assert.That(page.TextArea.IsLoaded(), Is.True);
        }

        [Test]
        [TestCase(true)]
        [TestCase(false)]
        public void IsVisibleTest(bool expand)
        {
            page.ExpandDiv(DivSection.TestTextInput, expand);
            Log.Info($"Element Under Test: {page.TextInput}");
            Assert.That(page.TextInput.IsVisible(), Is.EqualTo(expand));

            Log.Info($"Element Under Test: {page.TextArea}");
            Assert.That(page.TextArea.IsVisible(), Is.EqualTo(expand));
        }

        [Test]
        public void EnterText()
        {
            var text = "test";
            page.ExpandDiv(DivSection.TestTextInput, true);

            Log.Info($"Element Under Test: {page.TextInput}");
            page.TextInput.EnterText(text);
            Assert.That(page.TextInput.Text, Is.EqualTo(text));
            Assert.That(page.TextInput.Value, Is.EqualTo(text));

            Log.Info($"Element Under Test: {page.TextArea}");
            page.TextArea.EnterText(text);
            Assert.That(page.TextArea.Text, Is.EqualTo(text));
            Assert.That(page.TextArea.Value, Is.EqualTo(text));
        }

        [Test]
        public void ClearText()
        {
            page.ExpandDiv(DivSection.TestTextInput, true);

            Log.Info($"Element Under Test: {page.TextInput}");
            page.TextInput.ClearText();
            Assert.That(page.TextInput.Text, Is.EqualTo(string.Empty));
            Assert.That(page.TextInput.Value, Is.EqualTo(string.Empty));

            Log.Info($"Element Under Test: {page.TextArea}");
            page.TextArea.ClearText();
            Assert.That(page.TextArea.Text, Is.EqualTo(string.Empty));
            Assert.That(page.TextArea.Value, Is.EqualTo(string.Empty));
        }
    }
}
