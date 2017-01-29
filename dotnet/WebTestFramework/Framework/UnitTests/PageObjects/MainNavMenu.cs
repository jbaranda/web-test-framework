using Framework.PageObjects;
using OpenQA.Selenium;

namespace Framework.UnitTests.PageObjects
{
    public class MainNavMenu : BaseMenuExpandable
    {
        public enum Menu
        {
            Item1,
            Item2,
            Item3
        }

        public MainNavMenu(IWebDriver browser, By selector) : base(browser, selector) { }

        public override string ToString()
        {
            return $"{GetType().Name}: {Menu.Item1},{Menu.Item2},{Menu.Item3}";
        }
    }
}
