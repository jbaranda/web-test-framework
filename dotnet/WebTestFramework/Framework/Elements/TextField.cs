using OpenQA.Selenium;

namespace Framework.Elements
{
    public class TextField : BaseElement
    {
        public TextField(ISearchContext context, By selector) : base(context, selector) { }
    }
}
