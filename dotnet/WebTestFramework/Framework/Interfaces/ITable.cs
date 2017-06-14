using OpenQA.Selenium;
using System;
using System.Collections.Generic;

namespace Framework.Interfaces
{
    public interface ITable
    {
        List<IWebElement> ColumnHeaders { get; }
        List<IWebElement> Rows { get; }
        IWebElement GetRow(int row);
        IWebElement GetCellByRow(Enum column, int row);
        IWebElement GetCellByText(Enum column, string text);
    }
}
