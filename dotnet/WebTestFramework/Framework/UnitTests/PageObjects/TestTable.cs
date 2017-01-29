using Framework.PageObjects;
using OpenQA.Selenium;

namespace Framework.UnitTests.PageObjects
{
    public class TestTable : BaseTable
    {
        public enum Column
        {
            Header1 = 1,
            Header2 = 2,
            Header3 = 3
        }

        public TestTable(ISearchContext context, By tableSelector) : base(context, tableSelector) { }

        public override string ToString()
        {
            return $"{base.ToString()},Columns: [{Column.Header1},{Column.Header2},{Column.Header3}]";
        }
    }
}
