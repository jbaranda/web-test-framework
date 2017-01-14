using OpenQA.Selenium;
using System;

namespace Framework.Interfaces
{
    interface IMenu
    {
        IWebElement GetItem(Enum item);
        T NavigateTo<T>(Enum itemToClick);
    }
}
