using OpenQA.Selenium;
using System.Configuration;

namespace Framework.Browser
{
    public class WebDriverSettings
    {
        public static string SeleniumGridServer => ConfigurationManager.AppSettings["SeleniumGridServer"];
        public static int ExplicitWait => int.Parse(ConfigurationManager.AppSettings["ExplicitWait"]);
        public static LogLevel BrowserLogLevel => GetLogLevel(int.Parse(ConfigurationManager.AppSettings["BrowserLogLevel"]));
        public static string BrowserVersion => ConfigurationManager.AppSettings["BrowserVersion"];
        public static string OutlineColor => ConfigurationManager.AppSettings["OutlineColor"];
        public static bool ApplyOutline => GetBoolValue("ApplyOutline");

        private static bool GetBoolValue(string settingValue, bool defaultValue = false)
        {
            bool result;
            if (!bool.TryParse(ConfigurationManager.AppSettings[settingValue], out result))
            {
                result = defaultValue;
            }
            return result;
        }

        private static LogLevel GetLogLevel(int logValue)
        {
            return (LogLevel)logValue;
        }
    }
}
