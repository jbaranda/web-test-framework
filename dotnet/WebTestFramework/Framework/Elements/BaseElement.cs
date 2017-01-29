using Framework.Browser;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;

namespace Framework.Elements
{
    public abstract class BaseElement
    {
        protected static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private readonly By _selector;

        protected ISearchContext Context { get; set; }
        protected IWebElement Element { get; set; }
        protected string Type { get; set; }
        protected bool OutlineApplied { get; set; }

        public string Name => !string.IsNullOrEmpty(Element.GetName()) ? Element.GetName() : Type;
        public string Value => Element.GetValue();
        public virtual string Text => Element.GetText();

        protected BaseElement(ISearchContext context, By selector)
        {
            Type = GetType().Name;
            Context = context;
            OutlineApplied = false;
            _selector = selector;

            if (!IsLoaded())
            {
                var msg = $"Web Element={Name} was NOT loaded for selector: {_selector}";
                Log.Error(msg);
                throw new NoSuchElementException(msg);
            }

            Element = Context.FindElement(_selector);
        }

        public void Click()
        {
            Log.Debug($"{Type}: Click()");

            if (WebDriverSettings.ApplyOutline)
                Outline(true);

            Element.Click();
        }

        public bool IsLoaded()
        {
            Log.Debug($"{Type}: IsLoaded()");
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

            Log.Debug($"{Type}: Loaded={loaded}");
            return loaded;
        }

        public bool IsVisible()
        {
            Log.Debug($"{Type}: IsVisible()");
            var driver = Element.GetDriver();
            bool visible = false;
            try
            {
                new WebDriverWait(driver, TimeSpan.FromSeconds(2)).Until(ExpectedConditions.ElementIsVisible(_selector));
                visible = true;
            }
            catch (WebDriverTimeoutException) { }

            Log.Debug($"{Type}: Visible={visible}");
            return visible;
        }

        public void Outline(bool apply)
        {
            if (apply && !OutlineApplied)
            {
                Element.OutlineElement(apply);
                OutlineApplied = true;
                return;
            }

            if (!apply && OutlineApplied)
            {
                Element.OutlineElement(apply);
                OutlineApplied = false;
            }
        }

        public override string ToString()
        {
            return $"Type={Type},Selector={_selector},Name={Name},Text={Text},Value={Value},OutlineApplied={OutlineApplied}";
        }
    }
}
