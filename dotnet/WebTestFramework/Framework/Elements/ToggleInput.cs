using Framework.Browser;
using OpenQA.Selenium;

namespace Framework.Elements
{
    public class ToggleInput : BaseElement
    {
        public ToggleInput(ISearchContext context, By selector) : base(context, selector) { }

        public void ToggleOn()
        {
            Log.Info($"{Name}: ToggleOn()");
            if (WebDriverSettings.ApplyOutline)
                Outline(true);

            if (!IsOn())
                Element.Click();
            else
                Log.Warn($"{Name}: Element is already toggled on/checked");

            if (OutlineApplied)
                Outline(false);
        }

        public void ToggleOff()
        {
            Log.Info($"{Name}: ToggleOff()");
            if (WebDriverSettings.ApplyOutline)
                Outline(true);

            if (IsOn())
                Element.Click();
            else
                Log.Warn($"{Name}: Element is already toggled off/unchecked");

            if (OutlineApplied)
                Outline(false);
        }

        public bool IsOn()
        {
            Log.Info($"{Name}: IsOn()");
            var check = Element.Selected;
            Log.Info($"{Name}: Toggled/Checked={check}");
            return check;
        }

        public override string ToString()
        {
            return $"{base.ToString()},Toggled/Checked={Element.Selected}";
        }
    }
}
