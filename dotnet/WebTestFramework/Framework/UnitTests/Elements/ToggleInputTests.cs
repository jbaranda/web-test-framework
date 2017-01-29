using Framework.Elements;
using Framework.UnitTests.PageObjects;
using NUnit.Framework;

namespace Framework.UnitTests.Elements
{
    [TestFixture]
    [Parallelizable]
    public class ToggleInputTests : TestBase
    {
        [Test]
        public void NameTest()
        {
            Log.Info($"Element Under Test: {page.Checkbox}");
            Assert.That(page.Checkbox.Name, Is.EqualTo("checkboxName1"));

            Log.Info($"Element Under Test: {page.RadioButton}");
            Assert.That(page.RadioButton.Name, Is.EqualTo("optradio"));
        }

        [Test]
        public void ValueTest()
        {
            Log.Info($"Element Under Test: {page.Checkbox}");
            Assert.That(page.Checkbox.Value, Is.Empty);

            Log.Info($"Element Under Test: {page.RadioButton}");
            Assert.That(page.RadioButton.Value, Is.EqualTo("on"));
        }

        [Test]
        public void TextTest()
        {
            Log.Info($"Element Under Test: {page.Checkbox}");
            Assert.That(page.Checkbox.Text, Is.Empty);

            Log.Info($"Element Under Test: {page.RadioButton}");
            Assert.That(page.RadioButton.Text, Is.EqualTo("on"));
        }

        [Test]
        public void IsLoadedTest()
        {
            Log.Info($"Element Under Test: {page.Checkbox}");

            ToggleInput checkboxLoaded;
            Assert.DoesNotThrow(() => checkboxLoaded = page.Checkbox);
            Assert.That(page.Checkbox.IsLoaded(), Is.True);

            Log.Info($"Element Under Test: {page.RadioButton}");

            ToggleInput radioButtonLoaded;
            Assert.DoesNotThrow(() => radioButtonLoaded = page.RadioButton);
            Assert.That(page.RadioButton.IsLoaded(), Is.True);
        }

        [Test]
        [TestCase(true)]
        [TestCase(false)]
        public void IsVisibleTest(bool expand)
        {
            Log.Info($"Element Under Test: {page.Checkbox}");
            page.ExpandDiv(DivSection.TestCheckboxInput, expand);
            Assert.That(page.Checkbox.IsVisible(), Is.EqualTo(expand));

            Log.Info($"Element Under Test: {page.RadioButton}");
            page.ExpandDiv(DivSection.TestRadioInput, expand);
            Assert.That(page.RadioButton.IsVisible(), Is.EqualTo(expand));
        }

        [Test]
        public void ToggleOnTest()
        {
            Log.Info($"Element Under Test: {page.Checkbox}");
            page.ExpandDiv(DivSection.TestCheckboxInput, true);
            page.Checkbox.ToggleOn();
            Assert.That(page.Checkbox.IsOn(), Is.True);

            Log.Info($"Element Under Test: {page.DisabledCheckbox}");
            page.DisabledCheckbox.ToggleOn();
            Assert.That(page.DisabledCheckbox.IsOn(), Is.False);

            Log.Info($"Element Under Test: {page.RadioButton}");
            page.ExpandDiv(DivSection.TestRadioInput, true);
            page.RadioButton.ToggleOn();
            Assert.That(page.RadioButton.IsOn(), Is.True);
     
            Log.Info($"Element Under Test: {page.DisabledRadioButton}");
            page.DisabledRadioButton.ToggleOn();
            Assert.That(page.DisabledRadioButton.IsOn(), Is.False);
        }

        [Test]
        public void ToggleOffTest()
        {
            Log.Info($"Element Under Test: {page.Checkbox}");
            page.ExpandDiv(DivSection.TestCheckboxInput, true);
            page.Checkbox.ToggleOn();
            page.Checkbox.ToggleOff();

            Assert.That(page.Checkbox.IsOn(), Is.False);

            Log.Info($"Element Under Test: {page.DisabledCheckbox}");
            page.DisabledCheckbox.ToggleOn();
            Assert.That(page.DisabledCheckbox.IsOn(), Is.False);
            page.DisabledCheckbox.ToggleOff();
            Assert.That(page.DisabledCheckbox.IsOn(), Is.False);
        }
    }
}
