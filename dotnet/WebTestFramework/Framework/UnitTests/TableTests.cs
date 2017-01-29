using Framework.Elements;
using Framework.Interfaces;
using Framework.UnitTests.PageObjects;
using NUnit.Framework;
using OpenQA.Selenium;
using System;

namespace Framework.UnitTests
{
    [TestFixture]
    [Parallelizable]
    public class TableTests : TestBase
    {
        private ITable _table => new TestTable(Browser.Driver, By.Id("tableId"));

        [OneTimeSetUp]
        public void ExpandDiv()
        {
            page.ExpandDiv(DivSection.TestTable, true);
        }

        [SetUp]
        public void LogTestTable()
        {
            Log.Info($"PageObject Under Test: {_table}");
        }
     
        [Test]
        public void ColumnHeadersTest()
        {
            Assert.That(_table.ColumnHeaders.Count, Is.EqualTo(3));
            Assert.That(_table.ColumnHeaders[0].GetText(), Is.EqualTo("Header 1"));
            Assert.That(_table.ColumnHeaders[1].GetText(), Is.EqualTo("Header 2"));
            Assert.That(_table.ColumnHeaders[2].GetText(), Is.EqualTo("Header 3"));
        }

        [Test]
        public void RowsTest()
        {
            Assert.That(_table.Rows.Count, Is.EqualTo(4));
        }

        [Test]
        public void GetRowTest()
        {
            Assert.DoesNotThrow(() => _table.GetRow(1));
            Assert.DoesNotThrow(() => _table.GetRow(2));
            Assert.DoesNotThrow(() => _table.GetRow(3));
            Assert.DoesNotThrow(() => _table.GetRow(4));

            Assert.Throws<NoSuchElementException>(() => _table.GetRow(5));
        }

        [Test]
        public void GetCellByRow()
        {
            Assert.That(_table.GetCellByRow(TestTable.Column.Header1, 1).GetText(), Is.EqualTo("Row 1 - Item 1"));
            Assert.That(_table.GetCellByRow(TestTable.Column.Header2, 1).GetText(), Is.EqualTo("Row 1 - Item 2"));
            Assert.That(_table.GetCellByRow(TestTable.Column.Header3, 1).GetText(), Is.EqualTo("Row 1 - Item 3"));

            Assert.That(_table.GetCellByRow(TestTable.Column.Header1, 2).GetText(), Is.EqualTo("Row 2 - Item 1"));
            Assert.That(_table.GetCellByRow(TestTable.Column.Header2, 2).GetText(), Is.EqualTo("Row 2 - Item 2"));
            Assert.That(_table.GetCellByRow(TestTable.Column.Header3, 2).GetText(), Is.EqualTo("Row 2 - Item 3"));

            Assert.That(_table.GetCellByRow(TestTable.Column.Header1, 3).GetText(), Is.EqualTo("Row 3 - Item 1"));
            Assert.That(_table.GetCellByRow(TestTable.Column.Header2, 3).GetText(), Is.EqualTo("Row 3 - Item 2"));
            Assert.That(_table.GetCellByRow(TestTable.Column.Header3, 3).GetText(), Is.EqualTo("SAME VALUE"));

            Assert.That(_table.GetCellByRow(TestTable.Column.Header1, 4).GetText(), Is.EqualTo("Row 4 - Item 1"));
            Assert.That(_table.GetCellByRow(TestTable.Column.Header2, 4).GetText(), Is.EqualTo("Row 4 - Item 2"));
            Assert.That(_table.GetCellByRow(TestTable.Column.Header3, 4).GetText(), Is.EqualTo("SAME VALUE"));

            Assert.Throws<NoSuchElementException>(() => _table.GetCellByRow(TestTable.Column.Header1, 5));
        }

        [Test]
        public void GetCellByText()
        {
            Assert.DoesNotThrow(() => _table.GetCellByText(TestTable.Column.Header1, "Row 1 - Item 1"));
            Assert.DoesNotThrow(() => _table.GetCellByText(TestTable.Column.Header1, "Row 2 - Item 1"));
            Assert.DoesNotThrow(() => _table.GetCellByText(TestTable.Column.Header1, "Row 3 - Item 1"));
            Assert.DoesNotThrow(() => _table.GetCellByText(TestTable.Column.Header1, "Row 4 - Item 1"));

            Assert.DoesNotThrow(() => _table.GetCellByText(TestTable.Column.Header2, "Row 1 - Item 2"));
            Assert.DoesNotThrow(() => _table.GetCellByText(TestTable.Column.Header2, "Row 2 - Item 2"));
            Assert.DoesNotThrow(() => _table.GetCellByText(TestTable.Column.Header2, "Row 3 - Item 2"));
            Assert.DoesNotThrow(() => _table.GetCellByText(TestTable.Column.Header2, "Row 4 - Item 2"));

            Assert.DoesNotThrow(() => _table.GetCellByText(TestTable.Column.Header3, "Row 1 - Item 3"));
            Assert.DoesNotThrow(() => _table.GetCellByText(TestTable.Column.Header3, "Row 2 - Item 3"));

            Assert.Throws<NoSuchElementException>(() => _table.GetCellByText(TestTable.Column.Header1, "DOES NOT EXIST"));
            Assert.Throws<Exception>(() => _table.GetCellByText(TestTable.Column.Header3, "SAME VALUE"));
        }
    }
}
