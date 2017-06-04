using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Remote;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Framework.Browser
{
    public static class WebDriverExtensions
    {
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public static T NavigateToUrl<T>(this IWebDriver driver, string path)
        {
            Log.Info($"{driver.GetType().Name}: NavigateToUrl(): {path}");
            driver.Navigate().GoToUrl(path);
            return (T)Activator.CreateInstance(typeof(T), driver);
        }

        public static void ResizeBrowser(this IWebDriver driver, bool maximize = false, int x = 1280, int y = 1024)
        {
            var logMsg = $"{driver.GetType().Name}: ResizeBrowser(): maximize={maximize}";
            if (maximize)
            {
                Log.Info(logMsg);
                driver.Manage().Window.Maximize();
                return;
            }

            Log.Info($"{logMsg},{x},{y}");
            driver.Manage().Window.Size = new Size(x, y);
        }

        public static bool IsAlertPresent(this IWebDriver driver)
        {
            Log.Info($"{driver.GetType().Name}: IsAlertPresent()");
            try
            {
                driver.SwitchTo().Alert();
                Log.Info("Alert Found");
                return true;
            }
            catch (NoAlertPresentException)
            {
                Log.Info("Alert NOT Found");
                return false;
            }
        }

        public static void AcceptAlert(this IWebDriver driver)
        {
            Log.Info($"{driver.GetType().Name}: AcceptAlert()");
            try
            {
                driver.SwitchTo().Alert().Accept();
            }
            catch (NoAlertPresentException)
            {
                Log.Warn($"No Alert message is displayed");
            }
        }

        public static void DismissAlert(this IWebDriver driver)
        {
            Log.Info($"{driver.GetType().Name}: DismissAlert()");
            try
            {
                driver.SwitchTo().Alert().Dismiss();
            }
            catch (NoAlertPresentException)
            {
                Log.Warn($"No Alert message is displayed");
            }
        }

        public static string GetAlertText(this IWebDriver driver)
        {
            Log.Info($"{driver.GetType().Name}: GetAlertText()");
            try
            {
                var text = driver.SwitchTo().Alert().Text;
                Log.Info($"Alert text={text}");
                return text;
            }
            catch (NoAlertPresentException)
            {
                Log.Error($"No Alert message is displayed");
                throw;
            }
        }

        private static bool ContainsCookies(IWebDriver driver)
        {
            Log.Info($"{driver.GetType().Name}: ContainsCookies()");
            if (!driver.Manage().Cookies.AllCookies.Any()) return false;
            return true;
        }

        public static void AddCookie(this IWebDriver driver, string cookieName, string value)
        {
            Log.Info($"{driver.GetType().Name}: AddCookie(): {cookieName}={value}");

            if (driver.Manage().Cookies.GetCookieNamed(cookieName) != null)
            {
                Log.Warn($"Cookie={cookieName} already exists in Browser");
                return;
            }

            driver.Manage().Cookies.AddCookie(new Cookie(cookieName, value));
        }

        public static void ClearCookie(this IWebDriver driver, string cookieName)
        {
            Log.Info($"{driver.GetType().Name}: ClearCookie(): {cookieName}");
            if (!ContainsCookies(driver)) return;

            var cookieJar = driver.Manage().Cookies;
            if (cookieJar.GetCookieNamed(cookieName) == null)
            {
                Log.Warn($"Cookie={cookieName} NOT found in Browser");
                return;
            }

            cookieJar.DeleteCookieNamed(cookieName);
        }

        public static void ClearCookies(this IWebDriver driver)
        {
            Log.Info($"{driver.GetType().Name}: ClearCookies()");
            if (!ContainsCookies(driver)) return;

            driver.Manage().Cookies.DeleteAllCookies();
        }

        public static string GetCookieValue(this IWebDriver driver, string cookieName)
        {
            Log.Info($"{driver.GetType().Name}: GetCookieValue(): {cookieName}");
            if (!ContainsCookies(driver))
            {
                var msg = "No Cookies were found in Browser";
                Log.Error(msg);
                throw new Exception(msg);
            }

            if (driver.Manage().Cookies.GetCookieNamed(cookieName) == null)
            {
                var msg = $"Cookie={cookieName} NOT found in Browser";
                Log.Error(msg);
                throw new Exception(msg);
            }

            var cookieVal = driver.Manage().Cookies.GetCookieNamed(cookieName).Value;
            Log.Info($"{cookieName}={cookieVal}");
            return cookieVal;
        }

        public static void SwitchToFrameByName(this IWebDriver driver, string name)
        {
            Log.Info($"{driver.GetType().Name}: SwitchToFrameByName(): {name}");
            SwitchToFrame(driver, By.Name(name));
        }

        public static void SwitchToFrameById(this IWebDriver driver, string id)
        {
            Log.Info($"{driver.GetType().Name}: SwitchToFrameById(): {id}");
            SwitchToFrame(driver, By.Id(id));
        }

        public static void SwitchToFrameByClass(this IWebDriver driver, string className)
        {
            Log.Info($"{driver.GetType().Name}: SwitchToFrameByClass(): {className}");
            SwitchToFrame(driver, By.ClassName(className));
        }

        private static void SwitchToFrame(IWebDriver driver, By frameSelector)
        {
            Log.Info($"{driver.GetType().Name}: SwicthToFrame(): {frameSelector}");
            var frames = driver.FindElements(frameSelector);

            if (frames.Count == 0)
            {
                Log.Warn($"No elements found with selector: {frameSelector}");
            }

            if (frames.Count > 1)
            {
                Log.Warn($"More than ONE element found with selector: {frameSelector}");
            }

            else
            {
                var frameToSelect = frames.First();
                Log.Debug("Frame to Select: " + frameToSelect.GetAttribute("name"));
                driver.SwitchTo().Frame(frameToSelect);
            }
        }

        public static void SwitchToDefaultContent(this IWebDriver driver)
        {
            Log.Debug($"{driver.GetType().Name}: SwitchToDefaultContent()");
            driver.SwitchTo().DefaultContent();
        }

        public static void MoveToElement(this IWebDriver driver, IWebElement webElement)
        {
            Log.Debug($"{driver.GetType().Name}: MoveToElement(): {webElement.TagName}");
            new Actions(driver).MoveToElement(webElement).Build().Perform();
        }

        public static void DragAndDrop(this IWebDriver driver, IWebElement source, IWebElement target)
        {
            Log.Debug($"{driver.GetType().Name}: DragAndDrop():{source.TagName},{target.TagName}");
            new Actions(driver).DragAndDrop(source, target).Build().Perform();
        }

        public static BrowserType GetBrowserType(this IWebDriver driver)
        {
            Log.Info($"{driver.GetType().Name}: GetBrowserType()");

            var capabilities = ((RemoteWebDriver)driver).Capabilities;
            var browserName = Regex.Replace(capabilities.BrowserName.ToLower(), @"\s+", "");

            if (browserName.Equals("chrome"))
            {
                Log.Info($"BrowserType={BrowserType.Chrome}");
                return BrowserType.Chrome;
            }

            if (browserName.Equals("internetexplorer"))
            {
                Log.Info($"BrowserType={BrowserType.IE} {capabilities.Version}");
                return BrowserType.IE;
            }

            if (browserName.Equals("edge"))
            {
                Log.Info($"BrowserType={BrowserType.Edge}");
                return BrowserType.Edge;
            }

            if (browserName.Equals("phantomjs"))
            {
                Log.Info($"BrowserType={BrowserType.Phantomjs}");
                return BrowserType.Phantomjs;
            }

            if (browserName.Equals("safari"))
            {
                Log.Info($"BrowserType={BrowserType.Safari}");
                return BrowserType.Safari;
            }

            Log.Debug($"BrowserType={BrowserType.Firefox}");
            return BrowserType.Firefox;
        }

        public static void TakeScreenshot(this IWebDriver driver, string screenshotFileName)
        {
            Log.Info($"{driver.GetType().Name}: TakeScreenshot(): {screenshotFileName}");
            if (string.IsNullOrEmpty(screenshotFileName))
            {
                var msg = "Cannot specify a Screenshot Filename that is NULL or empty";
                Log.Error(msg);
                throw new Exception(msg);
            }

            var screenshotPath = Path.GetDirectoryName(new Uri(typeof(WebDriverExtensions).Assembly.CodeBase).LocalPath) + @"\Screenshots";

            var camera = (ITakesScreenshot)driver;
            var screenshot = camera.GetScreenshot();

            if (!Directory.Exists(screenshotPath))
                Directory.CreateDirectory(screenshotPath);

            var outputPath = Path.Combine(screenshotPath, screenshotFileName);

            var pathChars = Path.GetInvalidPathChars();

            var stringBuilder = new StringBuilder(outputPath);

            foreach (var item in pathChars)
            {
                stringBuilder.Replace(item, '.');
            }

            var screenShotPath = stringBuilder.ToString();
            screenshot.SaveAsFile(screenShotPath, ScreenshotImageFormat.Jpeg);
        }

        public static ReadOnlyCollection<LogEntry> GetBrowserLogs(this IWebDriver driver)
        {
            return (driver.GetBrowserType() != BrowserType.Firefox)
                ? driver.Manage().Logs.GetLog(LogType.Browser) : new List<LogEntry>().AsReadOnly();
        }
    }
}
