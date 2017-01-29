using Framework.Elements;
using NUnit.Framework;
using OpenQA.Selenium;

namespace Framework.UnitTests
{
    [TestFixture]
    [Parallelizable]
    public class SelectInputTests : TestBase
    {
        private readonly By _selectLocator = By.Id("selectId");
        private readonly By _multiSelect = By.Id("multiSelectId");
        public SelectInput selectInput => new SelectInput(Browser.Driver, _selectLocator);
        public SelectInput multiSelect => new SelectInput(Browser.Driver, _multiSelect);

        [Test]
        public void NameTest()
        {
            Log.Info($"Element Under Test: {selectInput}");
            Assert.That(selectInput.Name, Is.EqualTo("selectName"));
        }

        [Test]
        public void ValueTest()
        {
            Log.Info($"Element Under Test: {selectInput}");
            selectInput.SelectBy(SelectInput.ByType.Value, "value1");
            Assert.That(selectInput.Value, Is.EqualTo("value1"));
        }

        [Test]
        public void TextTest()
        {
            Log.Info($"Element Under Test: {selectInput}");
            selectInput.SelectBy(SelectInput.ByType.Text, "Option 1");
            Assert.That(selectInput.Text, Does.Contain("Option 1"));
        }

        [Test]
        public void IsLoadedTest()
        {
            Log.Info($"Element Under Test: {selectInput}");
            SelectInput selectInputLoaded;
            Assert.DoesNotThrow(() => selectInputLoaded = selectInput);
            Assert.That(selectInput.IsLoaded(), Is.True);
        }

        [Test]
        [TestCase(true)]
        [TestCase(false)]
        public void IsVisibleTest(bool expand)
        {
            Log.Info($"Element Under Test: {selectInput}");
            ExpandDiv(expand, SelectDivButton, SelectDiv);
            Assert.That(selectInput.IsVisible(), Is.EqualTo(expand));
        }

        [Test]
        public void SelectByTest()
        {
            Log.Info($"Element Under Test: {selectInput}");
            ExpandDiv(true, SelectDivButton, SelectDiv);

            selectInput.SelectBy(SelectInput.ByType.Index, 4);
            Assert.That(selectInput.IsOptionSelected("Option 4"), Is.True);
            Assert.That(selectInput.GetSelectedOptionText(), Is.EqualTo("Option 4"));
            Assert.That(selectInput.GetSelectedOptionValue(), Is.EqualTo("value4"));

            selectInput.SelectBy(SelectInput.ByType.Value, "value2");
            Assert.That(selectInput.IsOptionSelected("Option 2"), Is.True);
            Assert.That(selectInput.GetSelectedOptionText(), Is.EqualTo("Option 2"));
            Assert.That(selectInput.GetSelectedOptionValue(), Is.EqualTo("value2"));

            selectInput.SelectBy(SelectInput.ByType.Text, "Option 3");
            Assert.That(selectInput.IsOptionSelected("Option 3"), Is.True);
            Assert.That(selectInput.GetSelectedOptionText(), Is.EqualTo("Option 3"));
            Assert.That(selectInput.GetSelectedOptionValue(), Is.EqualTo("value3"));
        }

        [Test]
        public void MultiSelectTest()
        {
            Log.Info($"Element Under Test: {multiSelect}");
            ExpandDiv(true, SelectDivButton, SelectDiv);

            multiSelect.SelectBy(SelectInput.ByType.Index, 4);
            Assert.That(multiSelect.IsOptionSelected("Option 4"), Is.True);
            Assert.That(multiSelect.GetSelectedOptionText(), Is.EqualTo("Option 4"));
            Assert.That(multiSelect.GetSelectedOptionValue(), Is.EqualTo("value4"));

            multiSelect.DeselectBy(SelectInput.ByType.Index, 4);
            Assert.That(multiSelect.IsOptionSelected("Option 4"), Is.False);

            multiSelect.SelectBy(SelectInput.ByType.Value, "value2");
            Assert.That(multiSelect.IsOptionSelected("Option 2"), Is.True);
            Assert.That(multiSelect.GetSelectedOptionText(), Is.EqualTo("Option 2"));
            Assert.That(multiSelect.GetSelectedOptionValue(), Is.EqualTo("value2"));

            multiSelect.DeselectBy(SelectInput.ByType.Value, "value2");
            Assert.That(multiSelect.IsOptionSelected("Option 2"), Is.False);

            multiSelect.SelectBy(SelectInput.ByType.Text, "Option 3");
            Assert.That(multiSelect.IsOptionSelected("Option 3"), Is.True);
            Assert.That(multiSelect.GetSelectedOptionText(), Is.EqualTo("Option 3"));
            Assert.That(multiSelect.GetSelectedOptionValue(), Is.EqualTo("value3"));

            multiSelect.DeselectBy(SelectInput.ByType.Text, "Option 3");
            Assert.That(multiSelect.IsOptionSelected("Option 3"), Is.False);

            multiSelect.SelectAll();
            Assert.That(multiSelect.AreAllOptionsSelected(), Is.True);

            multiSelect.DeselectAll();
            Assert.That(multiSelect.AreNoOptionsSelected(), Is.True);
        }
    }
}
