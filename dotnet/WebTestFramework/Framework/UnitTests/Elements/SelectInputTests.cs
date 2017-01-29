using Framework.Elements;
using Framework.UnitTests.PageObjects;
using NUnit.Framework;

namespace Framework.UnitTests.Elements
{
    [TestFixture]
    [Parallelizable]
    public class SelectInputTests : TestBase
    {
        [Test]
        public void NameTest()
        {
            Log.Info($"Element Under Test: {page.SelectInput}");
            Assert.That(page.SelectInput.Name, Is.EqualTo("selectName"));
        }

        [Test]
        public void ValueTest()
        {
            Log.Info($"Element Under Test: {page.SelectInput}");
            page.SelectInput.SelectBy(SelectInput.ByType.Value, "value1");
            Assert.That(page.SelectInput.Value, Is.EqualTo("value1"));
        }

        [Test]
        public void TextTest()
        {
            Log.Info($"Element Under Test: {page.SelectInput}");
            page.SelectInput.SelectBy(SelectInput.ByType.Text, "Option 1");
            Assert.That(page.SelectInput.Text, Does.Contain("Option 1"));
        }

        [Test]
        public void IsLoadedTest()
        {
            Log.Info($"Element Under Test: {page.SelectInput}");
            SelectInput selectInputLoaded;
            Assert.DoesNotThrow(() => selectInputLoaded = page.SelectInput);
            Assert.That(page.SelectInput.IsLoaded(), Is.True);
        }

        [Test]
        [TestCase(true)]
        [TestCase(false)]
        public void IsVisibleTest(bool expand)
        {
            Log.Info($"Element Under Test: {page.SelectInput}");
            page.ExpandDiv(DivSection.TestSelectInput, expand);
            Assert.That(page.SelectInput.IsVisible(), Is.EqualTo(expand));
        }

        [Test]
        public void SelectByTest()
        {
            Log.Info($"Element Under Test: {page.SelectInput}");
            page.ExpandDiv(DivSection.TestSelectInput, true);

            page.SelectInput.SelectBy(SelectInput.ByType.Index, 4);
            Assert.That(page.SelectInput.IsOptionSelected("Option 4"), Is.True);
            Assert.That(page.SelectInput.GetSelectedOptionText(), Is.EqualTo("Option 4"));
            Assert.That(page.SelectInput.GetSelectedOptionValue(), Is.EqualTo("value4"));

            page.SelectInput.SelectBy(SelectInput.ByType.Value, "value2");
            Assert.That(page.SelectInput.IsOptionSelected("Option 2"), Is.True);
            Assert.That(page.SelectInput.GetSelectedOptionText(), Is.EqualTo("Option 2"));
            Assert.That(page.SelectInput.GetSelectedOptionValue(), Is.EqualTo("value2"));

            page.SelectInput.SelectBy(SelectInput.ByType.Text, "Option 3");
            Assert.That(page.SelectInput.IsOptionSelected("Option 3"), Is.True);
            Assert.That(page.SelectInput.GetSelectedOptionText(), Is.EqualTo("Option 3"));
            Assert.That(page.SelectInput.GetSelectedOptionValue(), Is.EqualTo("value3"));
        }

        [Test]
        public void MultiSelectTest()
        {
            Log.Info($"Element Under Test: {page.MultiSelect}");
            page.ExpandDiv(DivSection.TestSelectInput, true);

            page.MultiSelect.SelectBy(SelectInput.ByType.Index, 4);
            Assert.That(page.MultiSelect.IsOptionSelected("Option 4"), Is.True);
            Assert.That(page.MultiSelect.GetSelectedOptionText(), Is.EqualTo("Option 4"));
            Assert.That(page.MultiSelect.GetSelectedOptionValue(), Is.EqualTo("value4"));

            page.MultiSelect.DeselectBy(SelectInput.ByType.Index, 4);
            Assert.That(page.MultiSelect.IsOptionSelected("Option 4"), Is.False);

            page.MultiSelect.SelectBy(SelectInput.ByType.Value, "value2");
            Assert.That(page.MultiSelect.IsOptionSelected("Option 2"), Is.True);
            Assert.That(page.MultiSelect.GetSelectedOptionText(), Is.EqualTo("Option 2"));
            Assert.That(page.MultiSelect.GetSelectedOptionValue(), Is.EqualTo("value2"));

            page.MultiSelect.DeselectBy(SelectInput.ByType.Value, "value2");
            Assert.That(page.MultiSelect.IsOptionSelected("Option 2"), Is.False);

            page.MultiSelect.SelectBy(SelectInput.ByType.Text, "Option 3");
            Assert.That(page.MultiSelect.IsOptionSelected("Option 3"), Is.True);
            Assert.That(page.MultiSelect.GetSelectedOptionText(), Is.EqualTo("Option 3"));
            Assert.That(page.MultiSelect.GetSelectedOptionValue(), Is.EqualTo("value3"));

            page.MultiSelect.DeselectBy(SelectInput.ByType.Text, "Option 3");
            Assert.That(page.MultiSelect.IsOptionSelected("Option 3"), Is.False);

            page.MultiSelect.SelectAll();
            Assert.That(page.MultiSelect.AreAllOptionsSelected(), Is.True);

            page.MultiSelect.DeselectAll();
            Assert.That(page.MultiSelect.AreNoOptionsSelected(), Is.True);
        }
    }
}
