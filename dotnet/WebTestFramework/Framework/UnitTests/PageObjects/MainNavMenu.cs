using Framework.PageObjects;
using OpenQA.Selenium;

namespace Framework.UnitTests.PageObjects
{
    public class MainNavMenu : BaseMenuExpandable
    {
        public enum Menu
        {
            Item1 = 1,
            Item2 = 2,
            Item3 = 3
        }

        public MainNavMenu(IWebDriver browser, By selector) : base(browser, selector) { }

        public override string ToString()
        {
            return $"{base.ToString()},Menu: [{Menu.Item1},{Menu.Item2},{Menu.Item3}]";
        }
    }
}
