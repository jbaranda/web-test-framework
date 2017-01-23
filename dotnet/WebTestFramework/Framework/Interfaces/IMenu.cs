using OpenQA.Selenium;
using System;

namespace Framework.Interfaces
{
    interface IMenu
    {
        IWebElement GetItem(Enum item);
        void Select(Enum itemToClick);
        T NavigateTo<T>(Enum itemToClick);
    }
}
