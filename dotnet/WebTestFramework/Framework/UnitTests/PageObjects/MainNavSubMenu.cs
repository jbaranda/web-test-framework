using Framework.PageObjects;
using OpenQA.Selenium;

namespace Framework.UnitTests.PageObjects
{
    public class MainNavSubMenu : BaseMenu
    {
        public enum Menu
        {
            SubItem1 = 1,
            SubItem2 = 2,
            SubItem3 = 3
        }

        public MainNavSubMenu(IWebDriver browser, By selector) : base(browser, selector) { }

        public override string ToString()
        {
            return $"{base.ToString()},Menu: [{Menu.SubItem1},{Menu.SubItem2},{Menu.SubItem3}]";
        }
    }
}
