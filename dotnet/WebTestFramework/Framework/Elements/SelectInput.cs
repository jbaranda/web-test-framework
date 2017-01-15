using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Linq;

namespace Framework.Elements
{
    public class SelectInput : BaseElement
    {
        public enum SelectionType
        {
            Index,
            Text,
            Value,
            All
        }

        public SelectElement Control { get; }

        public SelectInput(ISearchContext context, By selector) : base(context, selector)
        {
            Control = new SelectElement(Element);
        }

        public void SelectBy(SelectionType type, object toSelect)
        {
            Log.Info($"{Name}: SelectBy(): {type}, {(toSelect is int ? Convert.ToInt32(toSelect).ToString() : toSelect.ToString())}");
            switch(type)
            {
                case SelectionType.Index:
                    Control.SelectByIndex(Convert.ToInt32(toSelect));
                    break;
                case SelectionType.Text:
                    Control.SelectByText(toSelect.ToString());
                    break;
                case SelectionType.Value:
                    Control.SelectByValue(toSelect.ToString());
                    break;
                default:
                    var msg = $"{Name}: Unsupported SelectionType={type} used for Selection";
                    Log.Error(msg);
                    throw new Exception(msg);
            }
        }

        public void DeselectBy(SelectionType type, object toDeselect)
        {
            Log.Info($"{Name}: DeselectBy(): {type}, {(toDeselect is int ? Convert.ToInt32(toDeselect).ToString() : toDeselect.ToString())}");
            switch (type)
            {
                case SelectionType.Index:
                    Control.DeselectByIndex(Convert.ToInt32(toDeselect));
                    break;
                case SelectionType.Text:
                    Control.DeselectByText(toDeselect.ToString());
                    break;
                case SelectionType.Value:
                    Control.DeselectByValue(toDeselect.ToString());
                    break;
                case SelectionType.All:
                    Control.DeselectAll();
                    break;
                default:
                    var msg = $"{Name}: Unsupported SelectionType={type} used for Deselection";
                    Log.Error(msg);
                    throw new Exception(msg);
            }
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
    }
}
