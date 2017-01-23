using OpenQA.Selenium;
using System;

namespace Framework.Interfaces
{
    interface ITable
    {
        IWebElement GetRow(int row);
        IWebElement GetCellByRow(Enum column, int row);
        IWebElement GetCellByText(Enum column, string text);
    }
}
