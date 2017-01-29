using OpenQA.Selenium;

namespace Framework.Elements
{
    public class ToggleInput : BaseElement
    {
        public ToggleInput(ISearchContext context, By selector) : base(context, selector) { }

        public void ToggleOn()
        {
            Log.Info($"{Name}: ToggleOn()");
            if (!IsOn())
                Element.Click();
            else
                Log.Warn($"{Name}: Element is already toggled on/checked");
        }

        public void ToggleOff()
        {
            Log.Info($"{Name}: ToggleOff()");
            if (IsOn())
                Element.Click();
            else
                Log.Warn($"{Name}: Element is already toggled off/unchecked");
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
