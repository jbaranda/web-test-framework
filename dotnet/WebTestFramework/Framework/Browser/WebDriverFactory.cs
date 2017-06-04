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

        private ChromeOptions _chromeOpts;
        private EdgeOptions _edgeOpts;
        private FirefoxOptions _firefoxOpts;
        private PhantomJSOptions _phantomJsOpts;
        private SafariOptions _safariOpts;
        private InternetExplorerOptions _ieOpts;

        private WebDriverFactory()
        {
            _chromeOpts = new ChromeOptions();
            _edgeOpts = new EdgeOptions();
            _firefoxOpts = new FirefoxOptions();
            _phantomJsOpts = new PhantomJSOptions();
            _safariOpts = new SafariOptions();
            _ieOpts = new InternetExplorerOptions
            {
                EnableNativeEvents = false,
                IgnoreZoomLevel = true,
                EnsureCleanSession = true,
                UnexpectedAlertBehavior = InternetExplorerUnexpectedAlertBehavior.Dismiss,
                PageLoadStrategy = InternetExplorerPageLoadStrategy.Normal
            };           
        }

        private void SetupDriverOptions(BrowserType browser, DriverOptions options, LogLevel logLevel)
        {
            switch (browser)
            {
                case BrowserType.Chrome:
                    if (options != null && options is ChromeOptions)
                        _chromeOpts = options as ChromeOptions;
                    _chromeOpts.SetLoggingPreference(LogType.Browser, logLevel);
                    break; 
                case BrowserType.Edge:
                    if (options != null && options is EdgeOptions)
                        _edgeOpts = options as EdgeOptions;
                    _edgeOpts.SetLoggingPreference(LogType.Browser, logLevel);
                    break;
                case BrowserType.Firefox:
                    if (options != null && options is FirefoxOptions)
                        _firefoxOpts = options as FirefoxOptions;
                    _firefoxOpts.SetLoggingPreference(LogType.Browser, logLevel);
                    break;
                case BrowserType.IE:
                    if (options != null && options is InternetExplorerOptions)
                        _ieOpts = options as InternetExplorerOptions;
                    _ieOpts.SetLoggingPreference(LogType.Browser, logLevel);
                    break;
                case BrowserType.Phantomjs:
                    if (options != null && options is PhantomJSOptions)
                        _phantomJsOpts = options as PhantomJSOptions;
                    _phantomJsOpts.SetLoggingPreference(LogType.Browser, logLevel);
                    break;
                case BrowserType.Safari:
                    if (options != null && options is SafariOptions)
                        _safariOpts = options as SafariOptions;
                    _safariOpts.SetLoggingPreference(LogType.Browser, logLevel);
                    break;
            }
        }

        public static WebDriverFactory Factory => new WebDriverFactory();

        public IWebDriver GetBrowser(BrowserType browser, DriverOptions options = null, LogLevel logLevel = LogLevel.Severe)
        {
            if (!string.IsNullOrEmpty(WebDriverSettings.SeleniumGridServer))
            {
                Log.Debug("Selenium Grid Server is specified in app.config, will attempt to retrieve Browser from Grid...");
                return GetRemoteBrowser(browser, WebDriverSettings.BrowserVersion, options, logLevel);
            }
                
            Log.Debug($"EXECUTING: GetBrowser(): {browser},{(options != null ? options.GetType().Name : string.Empty)},{logLevel}");
            var driverPath = Path.GetDirectoryName(new Uri(typeof(WebDriverFactory).Assembly.CodeBase).LocalPath);
            Log.Debug($"Path to Driver binaries: {driverPath}");

            SetupDriverOptions(browser, options, logLevel);

            switch (browser)
            {
                case BrowserType.Chrome:
                    // Prevents "Chrome being controlled by automated test software" message from showing
                    _chromeOpts.AddArgument("disable-infobars");
                    return new ChromeDriver(driverPath, _chromeOpts);
                case BrowserType.Edge:                 
                    return new EdgeDriver(driverPath, _edgeOpts);
                case BrowserType.Firefox:
                    // 60 Seconds is the Default Max Command Timeout for RemoteWebDriver - https://github.com/SeleniumHQ/selenium/blob/master/dotnet/src/webdriver/Remote/RemoteWebDriver.cs#L69
                    return new FirefoxDriver(FirefoxDriverService.CreateDefaultService(driverPath), _firefoxOpts, TimeSpan.FromSeconds(60));
                case BrowserType.IE:                  
                    return new InternetExplorerDriver(driverPath, _ieOpts);
                case BrowserType.Phantomjs:                   
                    return new PhantomJSDriver(driverPath, _phantomJsOpts);
                case BrowserType.Safari:                   
                    return new SafariDriver(driverPath, _safariOpts);
                default:
                    throw new Exception($"Unsupported BrowserType={browser}");
            }
        }

        private IWebDriver GetRemoteBrowser(BrowserType browser, string version = null, DriverOptions options = null, LogLevel logLevel = LogLevel.Severe)
        {
            var gridAddress = new Uri($"{WebDriverSettings.SeleniumGridServer}/wd/hub");
            Log.Debug($"EXECUTING: GetRemoteBrowser(): {gridAddress.AbsoluteUri},{browser},{(!string.IsNullOrEmpty(version) ? version : string.Empty)},{(options != null ? options.GetType().Name : string.Empty)},{logLevel}");
            ICapabilities capabillities;

            SetupDriverOptions(browser, options, logLevel);

            switch (browser)
            {
                case BrowserType.Chrome:
                    capabillities = _chromeOpts.ToCapabilities();
                    break;
                case BrowserType.Edge:
                    capabillities = _edgeOpts.ToCapabilities();
                    break;
                case BrowserType.Firefox:
                    capabillities = _firefoxOpts.ToCapabilities();
                    break;
                case BrowserType.IE:
                    _ieOpts.AddAdditionalCapability("version", !string.IsNullOrEmpty(version) ? version : "11");
                    capabillities = _ieOpts.ToCapabilities();
                    break;
                case BrowserType.Phantomjs:
                    capabillities = _phantomJsOpts.ToCapabilities();
                    break;
                case BrowserType.Safari:
                    capabillities = _safariOpts.ToCapabilities();
                    break;
                default:
                    throw new Exception($"Unsupported BrowserType={browser}");                    
            }

            try
            {
                return new RemoteWebDriver(gridAddress, capabillities);
            }

            catch (Exception e)
            {
                if (e is WebDriverTimeoutException || e is WebDriverException)
                {
                    var msg = $"Unable to connect to Selenium Grid: {gridAddress}, {e.Message}";
                    throw new Exception(msg, e);
                }

                throw new Exception($"An exception has occurred: {e.Message}", e);
            }            
        }
    }
}
