using Framework.Elements;
using NUnit.Framework;
using OpenQA.Selenium;

namespace Framework.UnitTests
{
    [TestFixture]
    [Parallelizable]
    public class ToggleInputTests : TestBase
    {
        private readonly By _checkboxLocator = By.Id("checkboxId1");
        private readonly By _disabledCheckboxLocator = By.Id("checkboxId3");
        private readonly By _radioLocator = By.Id("radioId1");
        private readonly By _disabledRadioLocator = By.Id("radioId3");

        public ToggleInput checkbox => new ToggleInput(Browser.Driver, _checkboxLocator);
        public ToggleInput disabledCheckbox => new ToggleInput(Browser.Driver, _disabledCheckboxLocator);
        public ToggleInput radioButton => new ToggleInput(Browser.Driver, _radioLocator);
        public ToggleInput disabledRadioButton => new ToggleInput(Browser.Driver, _disabledRadioLocator);

        [Test]
        public void NameTest()
        {
            Log.Info($"Element Under Test: {checkbox}");
            Assert.That(checkbox.Name, Is.EqualTo("checkboxName1"));

            Log.Info($"Element Under Test: {radioButton}");
            Assert.That(radioButton.Name, Is.EqualTo("optradio"));
        }

        [Test]
        public void ValueTest()
        {
            Log.Info($"Element Under Test: {checkbox}");
            Assert.That(checkbox.Value, Is.Empty);

            Log.Info($"Element Under Test: {radioButton}");
            Assert.That(radioButton.Value, Is.EqualTo("on"));
        }

        [Test]
        public void TextTest()
        {
            Log.Info($"Element Under Test: {checkbox}");
            Assert.That(checkbox.Text, Is.Empty);

            Log.Info($"Element Under Test: {radioButton}");
            Assert.That(radioButton.Text, Is.EqualTo("on"));
        }

        [Test]
        public void IsLoadedTest()
        {
            Log.Info($"Element Under Test: {checkbox}");

            ToggleInput checkboxLoaded;
            Assert.DoesNotThrow(() => checkboxLoaded = checkbox);
            Assert.That(checkbox.IsLoaded(), Is.True);

            Log.Info($"Element Under Test: {radioButton}");

            ToggleInput radioButtonLoaded;
            Assert.DoesNotThrow(() => radioButtonLoaded = radioButton);
            Assert.That(radioButton.IsLoaded(), Is.True);
        }

        [Test]
        [TestCase(true)]
        [TestCase(false)]
        public void IsVisibleTest(bool expand)
        {
            Log.Info($"Element Under Test: {checkbox}");            
            ExpandDiv(expand, CheckboxDivButton, CheckboxDiv);
            Assert.That(checkbox.IsVisible(), Is.EqualTo(expand));

            Log.Info($"Element Under Test: {radioButton}");
            ExpandDiv(expand, RadioDivButton, RadioDiv);
            Assert.That(radioButton.IsVisible(), Is.EqualTo(expand));
        }

        [Test]
        public void ToggleOnTest()
        {
            Log.Info($"Element Under Test: {checkbox}");            
            ExpandDiv(true, CheckboxDivButton, CheckboxDiv);
            checkbox.ToggleOn();
            Assert.That(checkbox.IsOn(), Is.True);

            Log.Info($"Element Under Test: {disabledCheckbox}");
            disabledCheckbox.ToggleOn();
            Assert.That(disabledCheckbox.IsOn(), Is.False);

            Log.Info($"Element Under Test: {radioButton}");
            ExpandDiv(true, RadioDivButton, RadioDiv);
            radioButton.ToggleOn();
            Assert.That(radioButton.IsOn(), Is.True);
     
            Log.Info($"Element Under Test: {disabledRadioButton}");
            disabledRadioButton.ToggleOn();
            Assert.That(disabledRadioButton.IsOn(), Is.False);
        }

        [Test]
        public void ToggleOffTest()
        {
            Log.Info($"Element Under Test: {checkbox}");
            ExpandDiv(true, CheckboxDivButton, CheckboxDiv);
            checkbox.ToggleOn();
            checkbox.ToggleOff();

            Assert.That(checkbox.IsOn(), Is.False);

            Log.Info($"Element Under Test: {disabledCheckbox}");
            disabledCheckbox.ToggleOn();
            Assert.That(disabledCheckbox.IsOn(), Is.False);
            disabledCheckbox.ToggleOff();
            Assert.That(disabledCheckbox.IsOn(), Is.False);
        }
    }
}
