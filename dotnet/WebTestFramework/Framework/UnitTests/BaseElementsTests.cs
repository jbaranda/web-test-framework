using Framework.Browser;
using Framework.Elements;
using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.IO;

namespace Framework.UnitTests
{
    [TestFixture]
    public class BaseElementsTests
    {
        private BaseBrowser _browser;
        private readonly string _testpagePath = Path.GetDirectoryName(new Uri(typeof(BaseElementsTests).Assembly.CodeBase).LocalPath);
        private string _testpageHtml;

        private TextField _text;
        private TextInput _textInput;
        private PageLink _pageLink;
        private SelectInput _select;
        private ToggleInput _radio;
        private Image _image;

        [OneTimeSetUp]
        public void SetupBrowser()
        {
            _browser = new BaseBrowser(WebDriverFactory.GetBrowser(BrowserType.Chrome));
            _testpageHtml = $"{_testpagePath}\\UnitTests\\TestHtml\\testpage.html";
            _browser.Driver.Navigate().GoToUrl(_testpageHtml);
        }

        [OneTimeTearDown]
        public void CloserBrowser()
        {
            _browser.Driver.Quit();
        }

        [Test]
        public void NameTest()
        {
            _text = new TextField(_browser.Driver, By.Id("textId"));
            _textInput = new TextInput(_browser.Driver, By.Id("inputId"));
            _pageLink = new PageLink(_browser.Driver, By.Id("textLinkId"));
            _select = new SelectInput(_browser.Driver, By.Id("selectId"));
            _radio = new ToggleInput(_browser.Driver, By.Id("radioId1"));
            _image = new Image(_browser.Driver, By.Id("imgId"));

            Assert.That(_text.Name, Is.EqualTo("textName"));
            Assert.That(_textInput.Name, Is.EqualTo("inputName"));
            Assert.That(_pageLink.Name, Is.EqualTo("textLinkName"));
            Assert.That(_select.Name, Is.EqualTo("selectName"));
            Assert.That(_radio.Name, Is.EqualTo("optradio"));
            Assert.That(_image.Name, Is.EqualTo("imgName"));
        }

        [Test]
        public void ValueTest()
        {
            _text = new TextField(_browser.Driver, By.Id("textId"));
            _textInput = new TextInput(_browser.Driver, By.Id("inputId"));
            _pageLink = new PageLink(_browser.Driver, By.Id("textLinkId"));
            _select = new SelectInput(_browser.Driver, By.Id("selectId"));
            _radio = new ToggleInput(_browser.Driver, By.Id("radioId1"));
            _image = new Image(_browser.Driver, By.Id("imgId"));

            Assert.That(_text.Value, Is.EqualTo("textValue"));
            Assert.That(_textInput.Value, Is.EqualTo("inputValue"));
            Assert.That(_pageLink.Value, Is.Null);
            Assert.That(_select.Value, Is.EqualTo("value1"));
            Assert.That(_radio.Value, Is.EqualTo("on"));
            Assert.That(_image.Value, Is.EqualTo("imgValue"));
        }

        [Test]
        public void TextTest()
        {
            _text = new TextField(_browser.Driver, By.Id("textId"));
            _textInput = new TextInput(_browser.Driver, By.Id("inputId"));
            _pageLink = new PageLink(_browser.Driver, By.Id("textLinkId"));
            _select = new SelectInput(_browser.Driver, By.Id("selectId"));
            _radio = new ToggleInput(_browser.Driver, By.Id("radioId1"));
            _image = new Image(_browser.Driver, By.Id("imgId"));

            Assert.That(_text.Text, Is.EqualTo("Text"));
            Assert.That(_textInput.Text, Is.EqualTo("inputValue"));
            Assert.That(_pageLink.Text, Is.EqualTo("Text Link"));
            Assert.That(_select.Text, Does.Contain("Option 1"));
            Assert.That(_radio.Text, Is.EqualTo("on"));
            Assert.That(_image.Text, Is.EqualTo("imgValue"));
        }

        [Test]
        public void IsLoadedTest()
        {
            Assert.DoesNotThrow(() => _text = new TextField(_browser.Driver, By.Id("textId")));
            Assert.DoesNotThrow(() => _textInput = new TextInput(_browser.Driver, By.Id("inputId")));
            Assert.DoesNotThrow(() => _pageLink = new PageLink(_browser.Driver, By.Id("textLinkId")));
            Assert.DoesNotThrow(() => _select = new SelectInput(_browser.Driver, By.Id("selectId")));
            Assert.DoesNotThrow(() => _radio = new ToggleInput(_browser.Driver, By.Id("radioId1")));
            Assert.DoesNotThrow(() => _image = new Image(_browser.Driver, By.Id("imgId")));

            Assert.That(_text.IsLoaded(), Is.True);
            Assert.That(_textInput.IsLoaded(), Is.True);
            Assert.That(_pageLink.IsLoaded(), Is.True);
            Assert.That(_select.IsLoaded(), Is.True);
            Assert.That(_radio.IsLoaded(), Is.True);
            Assert.That(_image.IsLoaded(), Is.True);
        }

        [Test]
        public void IsVisibleTest()
        {
            _text = new TextField(_browser.Driver, By.Id("textId"));
            _textInput = new TextInput(_browser.Driver, By.Id("inputId"));
            _pageLink = new PageLink(_browser.Driver, By.Id("textLinkId"));
            _select = new SelectInput(_browser.Driver, By.Id("selectId"));
            _radio = new ToggleInput(_browser.Driver, By.Id("radioId1"));
            _image = new Image(_browser.Driver, By.Id("imgId"));

            Assert.That(_text.IsVisible(), Is.False);
            Assert.That(_textInput.IsVisible(), Is.False);
            Assert.That(_pageLink.IsVisible(), Is.False);
            Assert.That(_select.IsVisible(), Is.False);
            Assert.That(_radio.IsVisible(), Is.False);
        }
    }
}
