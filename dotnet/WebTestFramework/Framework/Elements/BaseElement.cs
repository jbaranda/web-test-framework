using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;

namespace Framework.Elements
{
    public abstract class BaseElement
    {
        protected static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private readonly By _selector;
        private readonly bool _outlineApplied;

        protected ISearchContext Context { get; set; }
        protected IWebElement Element { get; set; }
        protected string Type { get; set; }

        public string Name => Element.GetName();
        public string Value => Element.GetValue();
        public string Text => Element.GetText();

        protected BaseElement(ISearchContext context, By selector, bool applyOutline = false)
        {
            Type = GetType().Name;
            Context = context;
            _selector = selector;
            _outlineApplied = applyOutline;

            if (!IsLoaded())
            {
                var msg = $"Web Element={Name} was NOT loaded for selector: {_selector}";
                Log.Error(msg);
                throw new NoSuchElementException(msg);
            }

            Element = Context.FindElement(_selector);

            if (_outlineApplied)
                Element.OutlineElement(_outlineApplied);
        }

        protected BaseElement(IWebElement element, bool applyOutline = false)
        {
            Type = GetType().Name;
            Context = element;
            Element = element;

            if (_outlineApplied)
                Element.OutlineElement(_outlineApplied);
        }

        public void Click()
        {
            Log.Debug($"{Type}: Click()");
            Element.Click();
        }

        public bool IsLoaded()
        {
            Log.Info($"{Type}: IsLoaded()");
            bool loaded;

            try
            {
                Context.FindElement(_selector);
                loaded = true;
            }
            catch (NoSuchElementException)
            {               
                loaded = false;
            }

            Log.Info($"{Type}: Loaded={loaded}");
            return loaded;
        }

        public bool IsVisible()
        {
            Log.Info($"{Type}: IsVisible()");
            var driver = Element.GetDriver();
            bool visible = false;
            try
            {
                new WebDriverWait(driver, TimeSpan.FromSeconds(2)).Until(ExpectedConditions.ElementIsVisible(_selector));
                visible = true;
            }
            catch (WebDriverTimeoutException) { }

            Log.Info($"{Type}: Visible={visible}");
            return visible;
        }

        public override string ToString()
        {
            return $"Type={Type}, Selector={_selector}, Name={Name}, Text={Text}, Value={Value}, OutlineApplied={_outlineApplied}";
        }
    }
}
