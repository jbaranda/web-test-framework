using OpenQA.Selenium;
using System;
using Framework.Interfaces;
using Framework.Browser;

namespace Framework.Elements
{
    public class PageLink : BaseElement, ILink
    {
        public string Href => Element.GetHref();

        public PageLink(ISearchContext context, By selector) : base(context, selector) { }

        public TPage ClickTo<TPage>()
        {
            Log.Info($"{Name}: ClickTo<{typeof(TPage).Name}>()");
            if (WebDriverSettings.ApplyOutline)
                Outline(true);

            Element.Click();
            return (TPage)Activator.CreateInstance(typeof(TPage), Context);
        }

        public override string ToString()
        {
            return $"{base.ToString()},Href={Href}";
        }
    }
}
