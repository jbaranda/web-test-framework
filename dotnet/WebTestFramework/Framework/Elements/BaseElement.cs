using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;

namespace Framework.Elements
{
    public abstract class BaseElement
    {
        protected static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private By _selector;
        private bool _outlineApplied;

        protected ISearchContext Context { get; set; }
        protected IWebElement Element { get; set; }
        protected string Name;

        public string Value => Element.GetValue();
        public string Text => Element.GetText();

        public BaseElement(ISearchContext context, By selector, bool applyOutline = false)
        {
            Name = GetType().Name;
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

        public bool IsLoaded()
        {
            Log.Info($"{Name}: IsLoaded()");
            bool loaded = false;

            try
            {
                Context.FindElement(_selector);
                loaded = true;
            }
            catch (NoSuchElementException)
            {               
                loaded = false;
            }

            Log.Info($"{Name}: Loaded={loaded}");
            return loaded;
        }

        public bool IsVisible()
        {
            Log.Info($"{Name}: IsVisible()");
            var driver = Element.GetDriver();
            var visible = new WebDriverWait(driver, TimeSpan.FromSeconds(2)).Until(element => ExpectedConditions.ElementIsVisible(_selector) != null);
            Log.Info($"{Name}: Visible={visible}");
            return new WebDriverWait(driver, TimeSpan.FromSeconds(2)).Until(element => ExpectedConditions.ElementIsVisible(_selector) != null);
        }

        public override string ToString()
        {
            return $"Name={Name}, Selector={_selector.ToString()} Text={Text}, Value={Value}, OutlineApplied={_outlineApplied}";
        }
    }
}
