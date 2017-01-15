using Framework.Elements;
using Framework.Interfaces;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Framework.PageObjects
{
    public abstract class BaseMenu : BaseElement, IMenu
    {
        protected virtual By MenuItemsSelector => By.CssSelector("li a");

        protected BaseMenu(ISearchContext context, By ulSelector, bool applyOutline = false) : base(context, ulSelector, applyOutline) { }

        public List<IWebElement> Items => Element.FindElements(MenuItemsSelector).ToList();

        public IWebElement GetItem(Enum item)
        {
            if (!Items.Any())
            {
                var msg = $"{Name}: No Tab Menu Items available";
                Log.Error(msg);
                throw new NoSuchElementException(msg);
            }

            try
            {
                return Items[item.GetHashCode() - 1];
            }

            catch (ArgumentOutOfRangeException)
            {
                var msg = $"{Name}: No Tab Menu Item at position={item.GetHashCode()}";
                Log.Error(msg);
                throw new Exception(msg);
            }
        }

        public T NavigateTo<T>(Enum itemToClick)
        {
            return GetItem(itemToClick).ClickTo<T>();
        }
    }
}
