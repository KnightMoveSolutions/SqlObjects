using System;
using System.Collections.Generic;
using KnightMoves.SqlObjects.SqlCode;
using KnightMoves.SqlObjects.SqlCode.TSQL;
using Xunit;

namespace KnightMoves.SqlObjects.Tests.TSql.Fluent
{
    public class FluentApi_OrderBy_Tests
    {
        private readonly string NL = Environment.NewLine;

        [Fact]
        public void Select_From_OrderBy_Fluent_Expressions()
        {
            // ARRANGE
            var sqlObj =
                TSQL
                  .SELECT()
                    .COLUMN("ColumnA")
                    .COLUMN("ColumnB")
                    .Calculate("ColumnC").Times("ColumnD").AS("CalcResult")
                    .COLUMN("t", "ColumnE")
                  .FROM("dbo", "MyTable", "t")
                  .ORDERBY()
                    .COLUMN("ColumnB").ASC()
                    .COLUMN("CalcResult").ASC()
                    .COLUMN("t", "ColumnE").ASC();

            // ACTION
            var sql = sqlObj.Build();

            var expected = $"  SELECT{NL}" + 
                           $"   [ColumnA],{NL}" +
                           $"   [ColumnB],{NL}" +
                           $"   ([ColumnC] * [ColumnD]) AS [CalcResult],{NL}" +
                           $"   [t].[ColumnE]{NL}" +
                           $"  FROM [dbo].[MyTable] t{NL}" +
                           $"  ORDER BY{NL}" +
                           $"   [ColumnB] ASC,{NL}" + 
                           $"   [CalcResult] ASC,{NL}" +
                           $"   [t].[ColumnE] ASC{NL}";

            // ASSERT
            Assert.Equal(expected, sql);
            TestHelper.Assert.SerializationWorks(expected, sqlObj);
        }

        [Fact]
        public void Select_From_OrderBy_String_Expression()
        {
            // ARRANGE
            var sqlObj =
                TSQL
                  .SELECT()
                    .COLUMN("ColumnA")
                    .COLUMN("ColumnB")
                  .FROM("dbo", "MyTable", "t")
                  .ORDERBY("ColumnB").ASC();

            // ACTION
            var sql = sqlObj.Build();

            var expected = $"  SELECT{NL}" +
                           $"   [ColumnA],{NL}" +
                           $"   [ColumnB]{NL}" +
                           $"  FROM [dbo].[MyTable] t{NL}" +
                           $"  ORDER BY{NL}" +
                           $"   [ColumnB] ASC{NL}";

            // ASSERT
            Assert.Equal(expected, sql);
            TestHelper.Assert.SerializationWorks(expected, sqlObj);
        }

        [Fact]
        public void Select_From_OrderBy_Column_Args()
        {
            // ARRANGE
            var sqlObj =
                TSQL
                  .SELECT()
                    .COLUMN("ColumnA")
                    .COLUMN("t", "ColumnB")
                  .FROM("dbo", "MyTable", "t")
                  .ORDERBY("t", "ColumnB").ASC();

            // ACTION
            var sql = sqlObj.Build();

            var expected = $"  SELECT{NL}" +
                           $"   [ColumnA],{NL}" +
                           $"   [t].[ColumnB]{NL}" +
                           $"  FROM [dbo].[MyTable] t{NL}" +
                           $"  ORDER BY{NL}" +
                           $"   [t].[ColumnB] ASC{NL}";

            // ASSERT
            Assert.Equal(expected, sql);
            TestHelper.Assert.SerializationWorks(expected, sqlObj);
        }

        [Fact]
        public void Select_From_OrderBy_QueryExpression()
        {
            // ARRANGE
            var queryExpression = new TSQLColumn { MultiPartIdentifier = "t", ColumnName = "ColumnB" };

            var sqlObj =
                TSQL
                  .SELECT()
                    .COLUMN("ColumnA")
                    .COLUMN("t", "ColumnB")
                  .FROM("dbo", "MyTable", "t")
                  .ORDERBY(queryExpression).ASC();

            // ACTION
            var sql = sqlObj.Build();

            var expected = $"  SELECT{NL}" +
                           $"   [ColumnA],{NL}" +
                           $"   [t].[ColumnB]{NL}" +
                           $"  FROM [dbo].[MyTable] t{NL}" +
                           $"  ORDER BY{NL}" +
                           $"   [t].[ColumnB] ASC{NL}";

            // ASSERT
            Assert.Equal(expected, sql);
            TestHelper.Assert.SerializationWorks(expected, sqlObj);
        }

        [Fact]
        public void Select_From_OrderBy_OrderByExpressions()
        {
            // ARRANGE 
            var expression1 = new TSQLOrderByExpression();
            var expression2 = new TSQLOrderByExpression();

            expression1.Children.Add(new TSQLColumn { ColumnName = "ColumnA" });
            expression2.Children.Add(new TSQLColumn { MultiPartIdentifier = "t", ColumnName = "ColumnB" });

            var orderByExpressions = new List<ISqlOrderByExpression> { expression1, expression2 };

            // ACTION
            var sqlObj =
                TSQL
                  .SELECT()
                    .COLUMN("ColumnA")
                    .COLUMN("t", "ColumnB")
                  .FROM("dbo", "MyTable", "t")
                  .ORDERBY(orderByExpressions).ASC();

            // ACTION
            var sql = sqlObj.Build();

            var expected = $"  SELECT{NL}" +
                           $"   [ColumnA],{NL}" +
                           $"   [t].[ColumnB]{NL}" +
                           $"  FROM [dbo].[MyTable] t{NL}" +
                           $"  ORDER BY{NL}" +
                           $"   [ColumnA] ASC,{NL}" +
                           $"   [t].[ColumnB] ASC{NL}";

            // ASSERT
            Assert.Equal(expected, sql);
            TestHelper.Assert.SerializationWorks(expected, sqlObj);
        }

        [Fact]
        public void Select_From_OrderBy_DESC()
        {
            // ARRANGE
            var sqlObj =
                TSQL
                  .SELECT()
                    .COLUMN("ColumnA")
                    .COLUMN("ColumnB")
                  .FROM("dbo", "MyTable", "t")
                  .ORDERBY("ColumnA").DESC()
                    .COLUMN("t", "ColumnB").DESC();

            // ACTION
            var sql = sqlObj.Build();

            var expected = $"  SELECT{NL}" +
                           $"   [ColumnA],{NL}" +
                           $"   [ColumnB]{NL}" +
                           $"  FROM [dbo].[MyTable] t{NL}" +
                           $"  ORDER BY{NL}" +
                           $"   [ColumnA] DESC,{NL}" +
                           $"   [t].[ColumnB] DESC{NL}";

            // ASSERT
            Assert.Equal(expected, sql);
            TestHelper.Assert.SerializationWorks(expected, sqlObj);
        }
    }
}
