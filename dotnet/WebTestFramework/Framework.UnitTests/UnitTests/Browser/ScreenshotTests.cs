using Framework.Browser;
using NUnit.Framework;
using System;
using System.IO;
namespace Framework.UnitTests.Browser
{
    [TestFixture]
    [Parallelizable]
    public class ScreenshotTests : TestBase
    {
        [Test]
        public void TakeScreenshotTest()
        {
            var fileName = $"{TestContext.CurrentContext.Test.Name}.jpg";
            Browser.Driver.TakeScreenshot(fileName);

            var screenshotFile = Path.GetFileName(new Uri(typeof(WebDriverExtensions).Assembly.CodeBase).LocalPath + @"\Screenshots\" + fileName);

            Assert.That(screenshotFile, Is.EqualTo(fileName));
        }
    }
}
