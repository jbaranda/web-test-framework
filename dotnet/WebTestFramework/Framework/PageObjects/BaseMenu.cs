using Framework.Browser;
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
        protected virtual By MenuItemsSelector => By.XPath("./li/a");

        protected BaseMenu(ISearchContext context, By ulSelector) : base(context, ulSelector) { }

        public List<IWebElement> Items => Element.FindElements(MenuItemsSelector).ToList();

        public IWebElement GetItem(Enum item)
        {
            Log.Debug($"{GetType().Name}: GetItem(): {item}");
            if (!Items.Any())
            {
                var msg = $"{GetType().Name}: No Tab Menu Items available";
                Log.Error(msg);
                throw new NoSuchElementException(msg);
            }

            try
            {
                return Items[item.GetHashCode() - 1];
            }

            catch (ArgumentOutOfRangeException)
            {
                var msg = $"{GetType().Name}: No Tab Menu Item at position={item.GetHashCode()}";
                Log.Error(msg);
                throw new Exception(msg);
            }
        }

        public T NavigateTo<T>(Enum itemToClick)
        {
            Log.Info($"{GetType().Name}: NavigateTo<{typeof(T).Name}>(): {itemToClick}");
            var item = GetItem(itemToClick);

            if (WebDriverSettings.ApplyOutline)
                item.OutlineElement(true);

            return item.ClickTo<T>();
        }

        public void SelectItem(Enum itemToClick)
        {
            Log.Info($"{GetType().Name}: SelectItem(): {itemToClick}");
            var item = GetItem(itemToClick);

            if (WebDriverSettings.ApplyOutline)
                item.OutlineElement(true);

            item.Click();
        }
    }
}
