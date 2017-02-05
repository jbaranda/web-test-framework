using OpenQA.Selenium;

namespace Framework.Elements
{
    public class Button : PageLink
    {
        public Button(ISearchContext context, By selector) : base(context, selector) { }
    }
}
