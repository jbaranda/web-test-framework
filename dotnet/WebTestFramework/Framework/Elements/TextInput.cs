using OpenQA.Selenium;

namespace Framework.Elements
{
    public class TextInput : BaseElement
    {
        public TextInput(ISearchContext context, By selector) : base(context, selector) { }

        public void EnterText(string text)
        {
            Log.Info($"{Name}: EnterText(): {text}");
            Element.Clear();
            Element.SendKeys(text);
        }

        public void ClearText()
        {
            Log.Info($"{Name}: ClearText()");
            Element.Clear();
        }
    }
}
