using Framework.Browser;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace Framework.Elements
{
    public class SelectInput : BaseElement
    {
        public override string Text => Regex.Replace(Regex.Replace(Element.GetText(), @"\r\n", " "), @"[\s+][^\w]", "");

        public enum ByType
        {
            Index,
            Text,
            Value
        }

        public SelectElement Control { get; }

        public SelectInput(ISearchContext context, By selector) : base(context, selector)
        {
            Control = new SelectElement(Element);
        }

        public void SelectBy(ByType type, object toSelect)
        {
            Log.Info($"{Name}: SelectBy(): {type}, {(toSelect is int ? Convert.ToInt32(toSelect).ToString() : toSelect.ToString())}");
            if (WebDriverSettings.ApplyOutline)
                Outline(true);

            switch (type)
            {
                case ByType.Index:
                    var index = Convert.ToInt32(toSelect);
                    if (index <= 0)
                    {
                        var invalid = $"Invalid Index Value: {index} (Must be Greater than 0)";
                        Log.Error(invalid);
                        throw new Exception(invalid);
                    }                       
                    Control.SelectByIndex((Convert.ToInt32(toSelect) - 1)); //Zero-Based index
                    break;
                case ByType.Text:
                    Control.SelectByText(toSelect.ToString());
                    break;
                case ByType.Value:
                    Control.SelectByValue(toSelect.ToString());
                    break;
                default:
                    var msg = $"{Name}: Unsupported Selection Type={type} used for SelectBy";
                    Log.Error(msg);
                    throw new Exception(msg);
            }

            if (OutlineApplied)
                Outline(false);
        }

        public void SelectAll()
        {
            Log.Info($"{Name}: SelectAll()");
            if (WebDriverSettings.ApplyOutline)
                Outline(true);

            new Actions(Element.GetDriver()).KeyDown(Keys.Shift).Build().Perform();
            foreach (var option in Element.FindElements(By.TagName("option")))
            {
                option.Click();
            }
            new Actions(Element.GetDriver()).KeyUp(Keys.Shift).Build().Perform();

            Log.Info($"SelectedOptions: Count={Control.AllSelectedOptions.Count}");

            if (OutlineApplied)
                Outline(false);

        }

        public void DeselectBy(ByType type, object toDeselect)
        {
            Log.Info($"{Name}: DeselectBy(): {type}, {(toDeselect is int ? Convert.ToInt32(toDeselect).ToString() : toDeselect.ToString())}");
            if (WebDriverSettings.ApplyOutline)
                Outline(true);

            switch (type)
            {
                case ByType.Index:
                    Control.DeselectByIndex((Convert.ToInt32(toDeselect) - 1));  //Zero-Based index
                    break;
                case ByType.Text:
                    Control.DeselectByText(toDeselect.ToString());
                    break;
                case ByType.Value:
                    Control.DeselectByValue(toDeselect.ToString());
                    break;
                default:
                    var msg = $"{Name}: Unsupported Selection Type={type} used for DeselectBy";
                    Log.Error(msg);
                    throw new Exception(msg);
            }

            if (OutlineApplied)
                Outline(false);
        }

        public void DeselectAll()
        {
            Log.Info($"{Name}: SelectAll()");
            if (WebDriverSettings.ApplyOutline)
                Outline(true);

            Control.DeselectAll();
            Log.Info($"SelectedOptions: Count={Control.AllSelectedOptions.Count}");

            if (OutlineApplied)
                Outline(false);
        }

        public string GetSelectedOptionText()
        {
            Log.Info($"{Name}: GetSelectedOptionText()");
            var text = Control.SelectedOption.Text;
            Log.Info($"{Name}: Text={text}");
            return text;
        }

        public string GetSelectedOptionValue()
        {
            Log.Info($"{Name}: GetSelectedOptionValue()");
            var value = Control.SelectedOption.GetValue();
            Log.Info($"{Name}: Value={value}");
            return value;
        }

        public bool IsOptionSelected(string textValue)
        {
            Log.Info($"{Name}: IsOptionSelected(): {textValue}");
            var foundSelectedOption = Control.AllSelectedOptions.Any(option => option.Text.Equals(textValue) || option.GetValue().Equals(textValue));
            Log.Info($"Option Selected={foundSelectedOption}");
            return foundSelectedOption;
        }

        public bool AreAllOptionsSelected()
        {
            Log.Info($"{Name}: AreAllOptionsSelected()");
            var optionsCnt = Element.FindElements(By.TagName("option")).Count();
            var selectedOptionsCnt = Control.AllSelectedOptions.Count();
            var allOptions = optionsCnt.Equals(selectedOptionsCnt);

            Log.Info($"All Options Selected={allOptions}");
            return allOptions;
        }

        public bool AreNoOptionsSelected()
        {
            Log.Info($"{Name}: AreNoOptionsSelected()");
            var noOptions = !Control.AllSelectedOptions.Any();

            Log.Info($"No Options Selected={noOptions}");
            return noOptions;
        }
    }
}
