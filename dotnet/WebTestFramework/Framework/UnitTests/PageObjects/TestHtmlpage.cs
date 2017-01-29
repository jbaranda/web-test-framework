using Framework.Elements;
using Framework.PageObjects;
using OpenQA.Selenium;

namespace Framework.UnitTests.PageObjects
{
    public class TestHtmlPage : BasePage
    {
        //Div Buttons Locators
        private readonly By _navMenuDivButton = By.Id("nav-menu-btn");
        private readonly By _textFieldDivButton = By.Id("text-field-btn");
        private readonly By _textInputDivButton = By.Id("text-input-btn");
        private readonly By _selectDivButton = By.Id("select-input-btn");
        private readonly By _checkboxDivButton = By.Id("checkbox-input-btn");
        private readonly By _radioDivButton = By.Id("radio-input-btn");
        private readonly By _imageDivButton = By.Id("img-btn");
        
        //Collapsable Divs Locators
        private readonly By _navMenuDiv = By.Id("navDivId");
        private readonly By _textFieldDiv = By.Id("textDivContainer");
        private readonly By _textInputDiv = By.Id("textInputDivContainer");
        private readonly By _selectDiv = By.Id("selectDivContainer");
        private readonly By _checkboxDiv = By.Id("checkboxDivContainer");
        private readonly By _radioDiv = By.Id("radioDivContainer");
        private readonly By _imageDiv = By.Id("imgDivContainer");

        //Element Locators
        private readonly By _textLocator = By.Id("textId");


        //Elements
        public TextField TextField => new TextField(Browser, _textLocator);


        public TestHtmlPage(IWebDriver browser) : base(browser) { }

        public void ExpandDiv(bool expand, By expandButtonSelector, By divContainerSelector)
        {
            var button = Browser.FindElement(expandButtonSelector);
            var div = Browser.FindElement(divContainerSelector);

            if (!expand && div.Displayed)
            {
                button.Click();
                BrowserWait.Until(driver => div.GetAttribute("class").Equals("collapse"));
                BrowserWait.Until(driver => !div.Displayed);
                return;
            }

            if (expand && !div.Displayed)
            {
                button.Click();
                BrowserWait.Until(driver => div.GetAttribute("class").Equals("collapse in"));
                BrowserWait.Until(driver => div.Displayed);
            }
        }
    }
}
