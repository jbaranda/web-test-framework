using Framework.Elements;
using Framework.Interfaces;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Framework.PageObjects
{
    public abstract class BaseTable : BaseElement, ITable
    {
        protected BaseTable(ISearchContext context, By tableSelector) : base(context, tableSelector) { }

        public List<IWebElement> ColumnHeaders => Element.FindElements(By.CssSelector("tr:nth-child(1) th")).ToList();
        public List<IWebElement> Rows => Element.FindElements(By.XPath(".//tr[position() > 1]")).ToList();

        public IWebElement GetCellByRow(Enum column, int row)
        {
            Log.Info($"{GetType().Name}: GetCellByRow(): {column},{row}");
            var cellByRow = $"tbody tr:nth-child({row + 1}) td:nth-child({column.GetHashCode()})";

            try
            {
                return Element.FindElement(By.CssSelector(cellByRow));
            }
            
            catch (NoSuchElementException e)
            {
                var msg = $"{GetType().Name}: No Table Cells found for Column={column} at Row={row}";
                Log.Error(msg);
                throw new NoSuchElementException(msg, e);
            }
        }

        public IWebElement GetCellByText(Enum column, string text)
        {
            Log.Info($"{GetType().Name}: GetCellByText(): {column},{text}");
            var columnCells = $"tbody tr td:nth-child({column.GetHashCode()})";
            var cellsMatched = Element.FindElements(By.CssSelector(columnCells)).Where(cell => cell.GetText().Equals(text));
            
            if(!cellsMatched.Any())
            {
                var msg = $"{GetType().Name}: No Table Cells found for Column={column} with Text={text}";
                Log.Error(msg);
                throw new NoSuchElementException(msg);
            }

            if(cellsMatched.Count() > 1)
            {
                var msg = $"{GetType().Name}: Ambiguous - Multiple Tables Cells found for Column={column} with Text={text}";
                Log.Error(msg);
                throw new Exception(msg);
            }

            return cellsMatched.First();
        }

        public IWebElement GetRow(int row)
        {
            Log.Info($"{GetType().Name}: GetRow(): {row}");
            var tableRow = $"tbody tr:nth-child({row + 1})";

            try
            {
                return Element.FindElement(By.CssSelector(tableRow));
            }

            catch (NoSuchElementException e)
            {
                var msg = $"{GetType().Name}: No Table Rows found at Row={row}";
                Log.Error(msg);
                throw new NoSuchElementException(msg, e);
            }
        }
    }
}
