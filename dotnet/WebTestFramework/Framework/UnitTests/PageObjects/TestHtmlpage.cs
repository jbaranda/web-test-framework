using Framework.Elements;
using Framework.PageObjects;
using OpenQA.Selenium;
using System;

namespace Framework.UnitTests.PageObjects
{
    public class TestHtmlPage : BasePage
    {
        public const string PagelinkHref = "pagelink.html";

        //Div Buttons Locators
        private readonly By _navMenuDivButton = By.Id("nav-menu-btn");
        private readonly By _textFieldDivButton = By.Id("text-field-btn");
        private readonly By _textInputDivButton = By.Id("text-input-btn");
        private readonly By _selectDivButton = By.Id("select-input-btn");
        private readonly By _checkboxDivButton = By.Id("checkbox-input-btn");
        private readonly By _radioDivButton = By.Id("radio-input-btn");
        private readonly By _imageDivButton = By.Id("img-btn");
        private readonly By _tableDivButton = By.Id("table-btn");
        private readonly By _modalDivButton = By.Id("modal-btn");
        private readonly By _tabMenuDivButton = By.Id("tab-menu-btn");

        //Collapsable Divs Locators
        private readonly By _navMenuDiv = By.Id("navDivId");
        private readonly By _textFieldDiv = By.Id("textDivContainer");
        private readonly By _textInputDiv = By.Id("textInputDivContainer");
        private readonly By _selectDiv = By.Id("selectDivContainer");
        private readonly By _checkboxDiv = By.Id("checkboxDivContainer");
        private readonly By _radioDiv = By.Id("radioDivContainer");
        private readonly By _imageDiv = By.Id("imgDivContainer");
        private readonly By _tableDiv = By.Id("tableDivContainer");
        private readonly By _modalDiv = By.Id("modalDivContainer");
        private readonly By _tabMenuDiv = By.Id("tabmenuNavDivContainer");


        //Elements
        public TextField TextField => new TextField(Browser, By.Id("textId"));
        public TextInput TextInput => new TextInput(Browser, By.Id("inputId"));
        public TextInput TextArea => new TextInput(Browser, By.Id("textareaId"));
        public SelectInput SelectInput => new SelectInput(Browser, By.Id("selectId"));
        public SelectInput MultiSelect => new SelectInput(Browser, By.Id("multiSelectId"));
        public ToggleInput Checkbox => new ToggleInput(Browser, By.Id("checkboxId1"));
        public ToggleInput DisabledCheckbox => new ToggleInput(Browser, By.Id("checkboxId3"));
        public ToggleInput RadioButton => new ToggleInput(Browser, By.Id("radioId1"));
        public ToggleInput DisabledRadioButton => new ToggleInput(Browser, By.Id("radioId3"));
        public Image Image => new Image(Browser, By.Id("imgId"));
        public PageLink PageLink => new PageLink(Browser, By.Id("textLinkId"));
        public Image ImageLink => new Image(Browser, By.Id("imgLinkId"));

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

        public void ExpandDiv(DivSection section, bool expand)
        {
            Log.Info($"{GetType().Name}: ExpandDiv(): {section},{expand}");

            IWebElement button;
            IWebElement div;

            switch (section)
            {
                case DivSection.TestNavMenu:
                    button = Browser.FindElement(_navMenuDivButton);
                    div = Browser.FindElement(_navMenuDiv);
                    break;
                case DivSection.TestTextField:
                    button = Browser.FindElement(_textFieldDivButton);
                    div = Browser.FindElement(_textFieldDiv);
                    break;
                case DivSection.TestTextInput:
                    button = Browser.FindElement(_textInputDivButton);
                    div = Browser.FindElement(_textInputDiv);
                    break;
                case DivSection.TestSelectInput:
                    button = Browser.FindElement(_selectDivButton);
                    div = Browser.FindElement(_selectDiv);
                    break;
                case DivSection.TestCheckboxInput:
                    button = Browser.FindElement(_checkboxDivButton);
                    div = Browser.FindElement(_checkboxDiv);
                    break;
                case DivSection.TestRadioInput:
                    button = Browser.FindElement(_radioDivButton);
                    div = Browser.FindElement(_radioDiv);
                    break;
                case DivSection.TestImage:
                    button = Browser.FindElement(_imageDivButton);
                    div = Browser.FindElement(_imageDiv);
                    break;
                case DivSection.TestTable:
                    button = Browser.FindElement(_tableDivButton);
                    div = Browser.FindElement(_tableDiv);
                    break;
                case DivSection.TestModal:
                    button = Browser.FindElement(_modalDivButton);
                    div = Browser.FindElement(_modalDiv);
                    break;
                case DivSection.TestTabMenuNav:
                    button = Browser.FindElement(_tabMenuDivButton);
                    div = Browser.FindElement(_tabMenuDiv);
                    break;
                default:
                    var msg = $"{GetType().Name}: Unsupported DivSection={section} used for ExpandDiv";
                    Log.Error(msg);
                    throw new Exception(msg);
            }
            
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
