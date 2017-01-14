using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.PhantomJS;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Safari;
using System;
using System.IO;

namespace Framework.Browser
{
    public class WebDriverFactory
    {
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private static InternetExplorerOptions ieOptions = new InternetExplorerOptions
        {
            IgnoreZoomLevel = true,
            EnsureCleanSession = true,
            UnexpectedAlertBehavior = InternetExplorerUnexpectedAlertBehavior.Dismiss,
            PageLoadStrategy = InternetExplorerPageLoadStrategy.Normal
        };

        public static IWebDriver GetBrowser(BrowserType browser)
        {
            Log.Info($"[EXECUTING] GetBrowser(): {browser}");
            var driverPath = Path.GetDirectoryName(new Uri(typeof(WebDriverFactory).Assembly.CodeBase).LocalPath);
            Log.Debug($"Path to Driver binaries: {driverPath}");

            switch (browser)
            {
                case BrowserType.Chrome:
                    return new ChromeDriver(driverPath);
                case BrowserType.Edge:
                    return new EdgeDriver(driverPath);
                case BrowserType.Firefox:
                    return new FirefoxDriver();
                case BrowserType.IE:
                    return new InternetExplorerDriver(driverPath, ieOptions);
                case BrowserType.Phantomjs:
                    return new PhantomJSDriver(driverPath);
                case BrowserType.Safari:
                    return new SafariDriver(driverPath);
                default:
                    throw new Exception($"Unsupported BrowserType={browser}");
            }
        }

        public static IWebDriver GetRemoteBrowser(Uri gridAddress, BrowserType browser, string version = null)
        {
            Log.Info($"[EXECUTING] GetRemoteBrowser(): {gridAddress.AbsoluteUri},{browser},{(!string.IsNullOrEmpty(version) ? version : string.Empty)}");
            ICapabilities capabillities;

            switch (browser)
            {
                case BrowserType.Chrome:
                    capabillities = DesiredCapabilities.Chrome();
                    break;
                case BrowserType.Edge:
                    capabillities = DesiredCapabilities.Edge();
                    break;
                case BrowserType.Firefox:
                    capabillities = DesiredCapabilities.Firefox();
                    break;
                case BrowserType.IE:
                    ieOptions.AddAdditionalCapability("version", !string.IsNullOrEmpty(version) ? version : "11");
                    capabillities = ieOptions.ToCapabilities();
                    break;
                case BrowserType.Phantomjs:
                    capabillities = DesiredCapabilities.PhantomJS();
                    break;
                case BrowserType.Safari:
                    capabillities = DesiredCapabilities.Safari();
                    break;
                default:
                    throw new Exception($"Unsupported BrowserType={browser}");                    
            }

            return new RemoteWebDriver(gridAddress, capabillities);
        }
    }
}
