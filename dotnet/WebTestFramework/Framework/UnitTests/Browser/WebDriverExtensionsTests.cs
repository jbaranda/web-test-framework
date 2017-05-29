using Framework.Browser;
using NUnit.Framework;
using System;

namespace Framework.UnitTests.Browser
{
    [TestFixture]
    [Parallelizable]
    public class WebDriverExtensionsTests : TestBase
    {
        [Test]
        public void ResizeBrowserTest()
        {
            var xWidth = 1280;
            var yHeight = 720;

            Browser.Driver.ResizeBrowser(x: xWidth, y: yHeight);
            Assert.That(Browser.Driver.Manage().Window.Size.Width, Is.AtLeast(xWidth));
            Assert.That(Browser.Driver.Manage().Window.Size.Height, Is.AtLeast(yHeight));

            Browser.Driver.ResizeBrowser(true);
            Assert.That(Browser.Driver.Manage().Window.Size.Width, Is.GreaterThan(xWidth));
            Assert.That(Browser.Driver.Manage().Window.Size.Height, Is.GreaterThan(yHeight));
        }

        [Test]
        public void CookiesTests()
        {
            var cookie1 = "newCookie1";
            var value1 = "newValue1";
            var cookie2 = "newCookie2";
            var value2 = "newValue2";

            Browser.Driver.Navigate().GoToUrl("http://www.google.com");

            Browser.Driver.AddCookie(cookie1, value1);
            Browser.Driver.AddCookie(cookie2, value2);
            Assert.That(Browser.Driver.GetCookieValue(cookie1), Is.EqualTo(value1));
            Assert.That(Browser.Driver.GetCookieValue(cookie2), Is.EqualTo(value2));

            Assert.Throws<Exception>(() => Browser.Driver.GetCookieValue("DOES NOT EXIST"));

            Browser.Driver.ClearCookie(cookie1);
            Assert.Throws<Exception>(() => Browser.Driver.GetCookieValue(cookie1));

            Browser.Driver.ClearCookies();
            Assert.Throws<Exception>(() => Browser.Driver.GetCookieValue(cookie2));
        }
    }
}
