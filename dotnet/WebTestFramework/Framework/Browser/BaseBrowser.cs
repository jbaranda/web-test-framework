using Newtonsoft.Json.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace Framework.Browser
{
    public class BaseBrowser
    {
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private string _sessionId;
        private RestClient _client;

        public IWebDriver Driver { get; set; }
        public WebDriverWait DriverWait { get; set; }
        public List<LogEntry> Logs => Driver.GetBrowserLogs().ToList();

        public BaseBrowser(IWebDriver driver)
        {
            _client = new RestClient();
            Driver = driver;
            Driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(WebDriverSettings.ImplicitWait));
            DriverWait = new WebDriverWait(driver, TimeSpan.FromSeconds(WebDriverSettings.ExplicitWait));

            _sessionId = ((RemoteWebDriver)Driver).SessionId != null ? ((RemoteWebDriver)Driver).SessionId.ToString() : string.Empty;
        }

        public string GetNodeIp()
        {
            if (string.IsNullOrEmpty(_sessionId))
            {
                return string.Empty;
            }
            
            _client.BaseUrl = new Uri(WebDriverSettings.SeleniumGridServer);
            var request = new RestRequest(Method.GET);
            request.Resource = "grid/api/testsession";
            request.AddQueryParameter("session", _sessionId);

            IRestResponse response = _client.Execute(request);

            if (!response.StatusCode.Equals(HttpStatusCode.OK))
            {
                var msg = $"Failed to get testsession data for session={_sessionId}";
                Log.Error(msg);
                throw new Exception(msg);
            }

            var nodeIp = JObject.Parse(response.Content).GetValue("proxyId").ToString();
            return new Uri(nodeIp).Host;                
        }
    }
}
