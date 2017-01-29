using OpenQA.Selenium;
using OpenQA.Selenium.Internal;
using System;

namespace Framework.Elements
{
    public static class WebElementExtensions
    {
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public static T ClickTo<T>(this IWebElement element)
        {
            Log.Info($"{element.GetType().Name}: ClickTo<{typeof(T)}>()");
            element.Click();
            return (T)Activator.CreateInstance(typeof(T), element.GetDriver());
        }

        public static IWebDriver GetDriver(this IWebElement element)
        {
            Log.Debug($"{element.GetType().Name}: GetDriver()");
            return ((IWrapsDriver)element).WrappedDriver;
        }

        public static void OutlineElement(this IWebElement element, bool apply)
        {
            Log.Debug($"{element.GetType().Name}: OutlineElement(): {apply}");
            ((IJavaScriptExecutor)element.GetDriver()).ExecuteScript(apply ? "arguments[0].style.outline='3px groove red'" : "arguments[0].style.outline='none'", element);
        }

        public static string GetName(this IWebElement element)
        {
            Log.Debug($"{element.GetType().Name}: GetName()");
            var val = element.GetAttribute("name");
            Log.Debug($"{element.GetType().Name}: Name={val}");
            return val;
        }

        public static string GetValue(this IWebElement element)
        {
            Log.Debug($"{element.GetType().Name}: GetValue()");
            var val = element.GetAttribute("value");
            Log.Debug($"{element.GetType().Name}: Value={val}");
            return val;
        }

        public static string GetHref(this IWebElement element)
        {
            Log.Debug($"{element.GetType().Name}: GetHref()");
            var href = element.GetAttribute("href");
            Log.Debug($"{element.GetType().Name}: Href={href}");
            return href;
        }

        public static string GetSrc(this IWebElement element)
        {
            Log.Debug($"{element.GetType().Name}: GetSrc()");
            var src = element.GetAttribute("src");
            Log.Debug($"{element.GetType().Name}: Src={src}");
            return src;
        }

        public static string GetAlt(this IWebElement element)
        {
            Log.Debug($"{element.GetType().Name}: GetAlt()");
            var alt = element.GetAttribute("alt");
            Log.Debug($"{element.GetType().Name}: Alt={alt}");
            return alt;
        }

        public static string GetText(this IWebElement element)
        {
            Log.Debug($"{element.GetType().Name}: GetText()");
            string text;
            if (!string.IsNullOrEmpty(element.Text))
                text = element.Text;
            else if (!string.IsNullOrEmpty(element.GetAttribute("textContent")))
                text = element.GetAttribute("textContent");
            else if (!string.IsNullOrEmpty(element.GetValue()))
                text = element.GetValue();
            else
                text = string.Empty;

            Log.Debug($"{element.GetType().Name}: Text={text}");
            return text;
        }
    }
}
