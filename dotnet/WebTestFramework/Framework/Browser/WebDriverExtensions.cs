using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Remote;
using System;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Framework.Browser
{
    public static class WebDriverExtensions
    {
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public static T NavigateToUrl<T>(this IWebDriver browser, string path)
        {
            Log.Info($"{browser.GetType().Name}: NavigateToUrl(): {path}");
            browser.Navigate().GoToUrl(path);
            return (T)Activator.CreateInstance(typeof(T), browser);
        }

        public static void ResizeBrowser(this IWebDriver browser, bool maximize = false, int x = 1280, int y = 1024)
        {
            var logMsg = $"{browser.GetType().Name}: ResizeBrowser(): maximize={maximize}";
            if (maximize)
            {
                Log.Info(logMsg);
                browser.Manage().Window.Maximize();
                return;
            }

            Log.Info($"{logMsg},{x},{y}");
            browser.Manage().Window.Size = new Size(x, y);
        }

        public static bool IsAlertPresent(this IWebDriver browser)
        {
            Log.Info($"{browser.GetType().Name}: IsAlertPresent()");
            try
            {
                browser.SwitchTo().Alert();
                Log.Info("Alert Found");
                return true;
            }
            catch (NoAlertPresentException)
            {
                Log.Info("Alert NOT Found");
                return false;
            }
        }

        public static void AcceptAlert(this IWebDriver browser)
        {
            Log.Info($"{browser.GetType().Name}: AcceptAlert()");
            try
            {
                browser.SwitchTo().Alert().Accept();
            }
            catch (NoAlertPresentException)
            {
                Log.Warn($"No Alert message is displayed");
            }
        }

        public static void DismissAlert(this IWebDriver browser)
        {
            Log.Info($"{browser.GetType().Name}: DismissAlert()");
            try
            {
                browser.SwitchTo().Alert().Dismiss();
            }
            catch (NoAlertPresentException)
            {
                Log.Warn($"No Alert message is displayed");
            }
        }

        public static string GetAlertText(this IWebDriver browser)
        {
            Log.Info($"{browser.GetType().Name}: GetAlertText()");
            try
            {
                var text = browser.SwitchTo().Alert().Text;
                Log.Info($"Alert text={text}");
                return text;
            }
            catch (NoAlertPresentException)
            {
                Log.Error($"No Alert message is displayed");
                throw;
            }
        }

        private static bool ContainsCookies(IWebDriver browser)
        {
            Log.Info($"{browser.GetType().Name}: ContainsCookies()");
            if (!browser.Manage().Cookies.AllCookies.Any()) return false;
            return true;
        }

        public static void AddCookie(this IWebDriver browser, string cookieName, string value)
        {
            Log.Info($"{browser.GetType().Name}: AddCookie(): {cookieName}={value}");

            if (browser.Manage().Cookies.GetCookieNamed(cookieName) != null)
            {
                Log.Warn($"Cookie={cookieName} already exists in Browser");
                return;
            }

            browser.Manage().Cookies.AddCookie(new Cookie(cookieName, value));
        }

        public static void ClearCookie(this IWebDriver browser, string cookieName)
        {
            Log.Info($"{browser.GetType().Name}: ClearCookie(): {cookieName}");
            if (!ContainsCookies(browser)) return;

            var cookieJar = browser.Manage().Cookies;
            if (cookieJar.GetCookieNamed(cookieName) == null)
            {
                Log.Warn($"Cookie={cookieName} NOT found in Browser");
                return;
            }

            cookieJar.DeleteCookieNamed(cookieName);
        }

        public static void ClearCookies(this IWebDriver browser)
        {
            Log.Info($"{browser.GetType().Name}: ClearCookies()");
            if (!ContainsCookies(browser)) return;

            browser.Manage().Cookies.DeleteAllCookies();
        }

        public static string GetCookieValue(this IWebDriver browser, string cookieName)
        {
            Log.Info($"{browser.GetType().Name}: GetCookieValue(): {cookieName}");
            if (!ContainsCookies(browser))
            {
                var msg = "No Cookies were found in Browser";
                Log.Error(msg);
                throw new Exception(msg);
            }

            if (browser.Manage().Cookies.GetCookieNamed(cookieName) == null)
            {
                var msg = $"Cookie={cookieName} NOT found in Browser";
                Log.Error(msg);
                throw new Exception(msg);
            }

            var cookieVal = browser.Manage().Cookies.GetCookieNamed(cookieName).Value;
            Log.Info($"{cookieName}={cookieVal}");
            return cookieVal;
        }

        public static void SwitchToFrameByName(this IWebDriver browser, string name)
        {
            Log.Info($"{browser.GetType().Name}: SwitchToFrameByName(): {name}");
            SwitchToFrame(browser, By.Name(name));
        }

        public static void SwitchToFrameById(this IWebDriver browser, string id)
        {
            Log.Info($"{browser.GetType().Name}: SwitchToFrameById(): {id}");
            SwitchToFrame(browser, By.Id(id));
        }

        public static void SwitchToFrameByClass(this IWebDriver browser, string className)
        {
            Log.Info($"{browser.GetType().Name}: SwitchToFrameByClass(): {className}");
            SwitchToFrame(browser, By.ClassName(className));
        }

        private static void SwitchToFrame(IWebDriver browser, By frameSelector)
        {
            Log.Info($"{browser.GetType().Name}: SwicthToFrame(): {frameSelector}");
            var frames = browser.FindElements(frameSelector);

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
                browser.SwitchTo().Frame(frameToSelect);
            }
        }

        public static void SwitchToDefaultContent(this IWebDriver browser)
        {
            Log.Debug($"{browser.GetType().Name}: SwitchToDefaultContent()");
            browser.SwitchTo().DefaultContent();
        }

        public static void MoveToElement(this IWebDriver browser, IWebElement webElement)
        {
            Log.Debug($"{browser.GetType().Name}: MoveToElement(): {webElement.TagName}");
            new Actions(browser).MoveToElement(webElement).Build().Perform();
        }

        public static void DragAndDrop(this IWebDriver browser, IWebElement source, IWebElement target)
        {
            Log.Debug($"{browser.GetType().Name}: DragAndDrop():{source.TagName},{target.TagName}");
            new Actions(browser).DragAndDrop(source, target).Build().Perform();
        }

        public static BrowserType GetBrowserType(this IWebDriver browser)
        {
            Log.Info($"{browser.GetType().Name}: GetBrowserType()");

            var capabilities = ((RemoteWebDriver)browser).Capabilities;
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

        public static void TakeScreenshot(this IWebDriver browser, string screenshotFileName)
        {
            Log.Info($"{browser.GetType().Name}: TakeScreenshot(): {screenshotFileName}");
            if (string.IsNullOrEmpty(screenshotFileName))
            {
                var msg = "Cannot specify a Screenshot Filename that is NULL or empty";
                Log.Error(msg);
                throw new Exception(msg);
            }

            var screenshotPath = Path.GetDirectoryName(new Uri(typeof(WebDriverExtensions).Assembly.CodeBase).LocalPath) + @"\Screenshots";

            var camera = (ITakesScreenshot)browser;
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
            screenshot.SaveAsFile(screenShotPath, ImageFormat.Jpeg);
        }

        public static ReadOnlyCollection<LogEntry> GetBrowserLogs(this IWebDriver browser)
        {
            return browser.Manage().Logs.GetLog(LogType.Browser);
        }
    }
}
