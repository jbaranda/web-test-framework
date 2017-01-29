using Framework.Elements;
using Framework.Interfaces;
using Framework.UnitTests.PageObjects;
using NUnit.Framework;
using OpenQA.Selenium;

namespace Framework.UnitTests
{
    [TestFixture]
    [Parallelizable]
    public class NavigationMenuTests : TestBase
    {
        private IMenuExpandable _navMenu => new MainNavMenu(Browser.Driver, By.Id("mainNavId"));
        private IMenu _subNavMenu => new MainNavSubMenu(Browser.Driver, By.Id("subNavId"));

        [Test]
        public void GetItemTest()
        {
            Log.Info($"PageObject Under Test: {_navMenu}");
            Assert.That(_navMenu.GetItem(MainNavMenu.Menu.Item1).GetText(), Is.EqualTo("Item 1"));
            Assert.That(_navMenu.GetItem(MainNavMenu.Menu.Item2).GetText(), Is.EqualTo("Item 2"));
            Assert.That(_navMenu.GetItem(MainNavMenu.Menu.Item3).GetText().Contains("Item 3"), Is.True);

            Log.Info($"PageObject Under Test: {_subNavMenu}");
            Assert.That(_subNavMenu.GetItem(MainNavSubMenu.Menu.SubItem1).GetText(), Is.EqualTo("SubItem 1"));
            Assert.That(_subNavMenu.GetItem(MainNavSubMenu.Menu.SubItem2).GetText(), Is.EqualTo("SubItem 2"));
            Assert.That(_subNavMenu.GetItem(MainNavSubMenu.Menu.SubItem3).GetText(), Is.EqualTo("SubItem 3"));
        }

        [Test]
        public void SelectItemTest()
        {
            Log.Info($"PageObject Under Test: {_navMenu}");
            page.ExpandDiv(DivSection.TestNavMenu, true);
            Assert.DoesNotThrow(() => _navMenu.SelectItem(MainNavMenu.Menu.Item1));
        }

        [Test]
        public void ExpandMenuTest()
        {
            Log.Info($"PageObject Under Test: {_navMenu}");
            page.ExpandDiv(DivSection.TestNavMenu, true);
            _navMenu.ExpandMenu(MainNavMenu.Menu.Item3, false);

            Log.Info($"Element Under Test: {_subNavMenu}");
            Assert.That(_subNavMenu.GetItem(MainNavSubMenu.Menu.SubItem1).Displayed, Is.True);
            Assert.That(_subNavMenu.GetItem(MainNavSubMenu.Menu.SubItem2).Displayed, Is.True);
            Assert.That(_subNavMenu.GetItem(MainNavSubMenu.Menu.SubItem3).Displayed, Is.True);
        }

        [Test]
        public void NavigateToTest()
        {
            Log.Info($"PageObject Under Test: {_navMenu}");
            var href = $"{GetTestHtmlFolderPath()}/TestHtml/pagelink.html";
            page.ExpandDiv(DivSection.TestNavMenu, true);

            var pagelinkpage = _navMenu.NavigateTo<PagelinkPage>(MainNavSubMenu.Menu.SubItem2);
            Assert.That(pagelinkpage.Url.Contains(href), Is.True);
            Assert.That(pagelinkpage.Title, Is.EqualTo("Web-Test-Framework: Pagelink Page"));
            Assert.That(pagelinkpage.SuccessText.IsVisible(), Is.True);

            page = pagelinkpage.ReturnLink.ClickTo<TestHtmlPage>();

            Log.Info($"PageObject Under Test: {_subNavMenu}");
            page.ExpandDiv(DivSection.TestNavMenu, true);
            _navMenu.ExpandMenu(MainNavMenu.Menu.Item3, false);
            pagelinkpage = _subNavMenu.NavigateTo<PagelinkPage>(MainNavSubMenu.Menu.SubItem2);
            Assert.That(pagelinkpage.Url.Contains(href), Is.True);
            Assert.That(pagelinkpage.Title, Is.EqualTo("Web-Test-Framework: Pagelink Page"));
            Assert.That(pagelinkpage.SuccessText.IsVisible(), Is.True);

            page = pagelinkpage.ReturnLink.ClickTo<TestHtmlPage>();
        }
    }
}
