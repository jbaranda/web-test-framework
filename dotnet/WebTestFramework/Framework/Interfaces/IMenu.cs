using OpenQA.Selenium;
using System;

namespace Framework.Interfaces
{
    public interface IMenu
    {
        IWebElement GetItem(Enum item);
        void SelectItem(Enum itemToClick);
        T NavigateTo<T>(Enum itemToClick);
    }
}
