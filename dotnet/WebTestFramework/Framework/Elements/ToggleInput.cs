using OpenQA.Selenium;

namespace Framework.Elements
{
    public class ToggleInput : BaseElement
    {
        public ToggleInput(ISearchContext context, By selector) : base(context, selector) { }

        public void Check()
        {
            Log.Info($"{Name}: Check()");
            if (!IsChecked())
                Element.Click();
            else
                Log.Warn($"{Name}: Element is already checked");
        }

        public void Uncheck()
        {
            Log.Info($"{Name}: Uncheck()");
            if (IsChecked())
                Element.Click();
            else
                Log.Warn($"{Name}: Element is already unchecked");
        }

        public bool IsChecked()
        {
            Log.Info($"{Name}: IsChecked()");
            var check = Element.Selected;
            Log.Info($"{Name}: Checked={check}");
            return check;
        }

        public override string ToString()
        {
            return $"{base.ToString()},Checked={IsChecked()}";
        }
    }
}
