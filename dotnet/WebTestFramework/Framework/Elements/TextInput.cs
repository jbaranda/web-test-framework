using Framework.Browser;
using OpenQA.Selenium;

namespace Framework.Elements
{
    public class TextInput : BaseElement
    {
        public TextInput(ISearchContext context, By selector) : base(context, selector) { }

        public void EnterText(string text)
        {
            Log.Info($"{Name}: EnterText(): {text}");
            if (WebDriverSettings.ApplyOutline)
                Outline(true);

            Element.Clear();
            Element.SendKeys(text);

            if (OutlineApplied)
                Outline(false);
        }

        public void ClearText()
        {
            Log.Info($"{Name}: ClearText()");
            if (WebDriverSettings.ApplyOutline)
                Outline(true);

            Element.Clear();

            if (OutlineApplied)
                Outline(false);
        }
    }
}
