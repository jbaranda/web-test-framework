using Framework.Browser;
using Framework.Elements;
using Framework.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using System;

namespace Framework.PageObjects
{
    public abstract class BaseMenuExpandable : BaseMenu, IMenuExpandable
    {
        protected BaseMenuExpandable(ISearchContext context, By ulSelector) : base(context, ulSelector) { }

        public void ExpandMenu(Enum itemToExpand, bool hover)
        {
            Log.Info($"EXECUTING: ExpandMenu(): {itemToExpand}");
            var expandMenu = GetItem(itemToExpand);

            if (WebDriverSettings.ApplyOutline)
                expandMenu.OutlineElement(true);

            if (hover)
            {
                new Actions(expandMenu.GetDriver()).MoveToElement(expandMenu).Build().Perform();
            }
            else
            {
                expandMenu.Click();
            }
        }
    }
}
