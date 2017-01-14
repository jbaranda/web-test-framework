using OpenQA.Selenium;
using System;
using Framework.Interfaces;

namespace Framework.Elements
{
    public class PageLink : BaseElement, ILink
    {
        public string Href => Element.GetHref();

        public PageLink(ISearchContext context, By selector) : base(context, selector) { }

        public T ClickTo<T>()
        {
            Log.Info($"[EXECUTING] ClickTo<{typeof(T).Name}>()");
            return (T)Activator.CreateInstance(typeof(T), Context);
        }

        public override string ToString()
        {
            return $"{base.ToString()},Href={Href}";
        }
    }
}
