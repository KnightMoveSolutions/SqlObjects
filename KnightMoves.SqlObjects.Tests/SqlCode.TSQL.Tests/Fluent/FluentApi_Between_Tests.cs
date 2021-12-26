using System;
using KnightMoves.SqlObjects.SqlCode;
using Xunit;

namespace KnightMoves.SqlObjects.Tests.TSql.Fluent
{
    public class FluentApi_Between_Tests
    {
        private readonly string NL = Environment.NewLine;

        [Fact]
        public void Select_Star_From_Where_Between_String()
        {
            // ARRANGE 
            var sqlObj =
                TSQL
                  .SELECT()
                    .STAR()
                  .FROM("MyTable")
                  .WHERE("t", "MyColumn").IsLessThanOrEqualTo(decimal.MinValue)
                    .AND("t", "SomeColumn").BETWEEN("A").AND("F");

            // ACTION
            var sql = sqlObj.Build();

            var expected = $"  SELECT{NL}" +
                           $"   *{NL}" +
                           $"  FROM [MyTable]{NL}" +
                           $"  WHERE 1=1{NL}" +
                           $"   AND [t].[MyColumn] <= {decimal.MinValue}{NL}" +
                           $"   AND [t].[SomeColumn] BETWEEN 'A' AND 'F'{NL}";

            // ASSERT
            Assert.Equal(expected, sql);
            TestHelper.Assert.SerializationWorks(expected, sqlObj);
        }

        [Fact]
        public void Select_Star_From_Where_Between_Int()
        {
            // ARRANGE 
            var sqlObj =
                TSQL
                  .SELECT()
                    .STAR()
                  .FROM("MyTable")
                  .WHERE("t", "MyColumn").IsLessThanOrEqualTo(decimal.MinValue)
                    .AND("t", "SomeColumn").BETWEEN(1).AND(100);

            // ACTION
            var sql = sqlObj.Build();

            var expected = $"  SELECT{NL}" +
                           $"   *{NL}" +
                           $"  FROM [MyTable]{NL}" +
                           $"  WHERE 1=1{NL}" +
                           $"   AND [t].[MyColumn] <= {decimal.MinValue}{NL}" +
                           $"   AND [t].[SomeColumn] BETWEEN 1 AND 100{NL}";

            // ASSERT
            Assert.Equal(expected, sql);
            TestHelper.Assert.SerializationWorks(expected, sqlObj);
        }

        [Fact]
        public void Select_Star_From_Where_Between_DateTime()
        {
            // ARRANGE 
            var TODAY = DateTime.Today;
            var TOMORROW = TODAY.AddDays(1);

            var sqlObj =
                TSQL
                  .SELECT()
                    .STAR()
                  .FROM("MyTable")
                  .WHERE("t", "MyColumn").IsLessThanOrEqualTo(decimal.MinValue)
                    .AND("t", "SomeColumn").BETWEEN(TODAY).AND(TOMORROW);

            // ACTION
            var sql = sqlObj.Build();

            var expected = $"  SELECT{NL}" +
                           $"   *{NL}" +
                           $"  FROM [MyTable]{NL}" +
                           $"  WHERE 1=1{NL}" +
                           $"   AND [t].[MyColumn] <= {decimal.MinValue}{NL}" +
                           $"   AND [t].[SomeColumn] BETWEEN '{TODAY.ToString(SqlDataType.SQL_DATE_STRING_FORMAT)}' AND '{TOMORROW.ToString(SqlDataType.SQL_DATE_STRING_FORMAT)}'{NL}";

            // ASSERT
            Assert.Equal(expected, sql);
            TestHelper.Assert.SerializationWorks(expected, sqlObj);
        }

        [Fact]
        public void Select_Star_From_Where_Between_Long()
        {
            // ARRANGE 
            var sqlObj =
                TSQL
                  .SELECT()
                    .STAR()
                  .FROM("MyTable")
                  .WHERE("t", "MyColumn").IsLessThanOrEqualTo(decimal.MinValue)
                    .AND("t", "SomeColumn").BETWEEN(long.MinValue).AND(long.MaxValue);

            // ACTION
            var sql = sqlObj.Build();

            var expected = $"  SELECT{NL}" +
                           $"   *{NL}" +
                           $"  FROM [MyTable]{NL}" +
                           $"  WHERE 1=1{NL}" +
                           $"   AND [t].[MyColumn] <= {decimal.MinValue}{NL}" +
                           $"   AND [t].[SomeColumn] BETWEEN {long.MinValue} AND {long.MaxValue}{NL}";

            // ASSERT
            Assert.Equal(expected, sql);
            TestHelper.Assert.SerializationWorks(expected, sqlObj);
        }

        [Fact]
        public void Select_Star_From_Where_Between_Char()
        {
            // ARRANGE 
            var sqlObj =
                TSQL
                  .SELECT()
                    .STAR()
                  .FROM("MyTable")
                  .WHERE("t", "MyColumn").IsLessThanOrEqualTo(decimal.MinValue)
                    .AND("t", "SomeColumn").BETWEEN('A').AND('F');

            // ACTION
            var sql = sqlObj.Build();

            var expected = $"  SELECT{NL}" +
                           $"   *{NL}" +
                           $"  FROM [MyTable]{NL}" +
                           $"  WHERE 1=1{NL}" +
                           $"   AND [t].[MyColumn] <= {decimal.MinValue}{NL}" +
                           $"   AND [t].[SomeColumn] BETWEEN 'A' AND 'F'{NL}";

            // ASSERT
            Assert.Equal(expected, sql);
            TestHelper.Assert.SerializationWorks(expected, sqlObj);
        }

        [Fact]
        public void Select_Star_From_Where_Between_Decimal()
        {
            // ARRANGE 
            var sqlObj =
                TSQL
                  .SELECT()
                    .STAR()
                  .FROM("MyTable")
                  .WHERE("t", "MyColumn").IsLessThanOrEqualTo(decimal.MinValue)
                    .AND("t", "SomeColumn").BETWEEN(decimal.MinValue).AND(decimal.MaxValue);

            // ACTION
            var sql = sqlObj.Build();

            var expected = $"  SELECT{NL}" +
                           $"   *{NL}" +
                           $"  FROM [MyTable]{NL}" +
                           $"  WHERE 1=1{NL}" +
                           $"   AND [t].[MyColumn] <= {decimal.MinValue}{NL}" +
                           $"   AND [t].[SomeColumn] BETWEEN {decimal.MinValue} AND {decimal.MaxValue}{NL}";

            // ASSERT
            Assert.Equal(expected, sql);
            TestHelper.Assert.SerializationWorks(expected, sqlObj);
        }


        [Fact]
        public void Select_Star_From_Where_Between_And_Basic_Condition()
        {
            // ARRANGE 
            var sqlObj =
                TSQL
                  .SELECT()
                    .STAR()
                  .FROM("MyTable")
                  .WHERE("t", "MyColumn").IsLessThanOrEqualTo(decimal.MinValue)
                    .AND("t", "SomeColumn").BETWEEN(1).AND(10)
                    .AND("AnotherColumn").IsGreaterThan(100);

            // ACTION
            var sql = sqlObj.Build();

            var expected = $"  SELECT{NL}" +
                           $"   *{NL}" +
                           $"  FROM [MyTable]{NL}" +
                           $"  WHERE 1=1{NL}" +
                           $"   AND [t].[MyColumn] <= {decimal.MinValue}{NL}" +
                           $"   AND [t].[SomeColumn] BETWEEN 1 AND 10{NL}" +
                           $"   AND [AnotherColumn] > 100{NL}";

            // ASSERT
            Assert.Equal(expected, sql);
            TestHelper.Assert.SerializationWorks(expected, sqlObj);
        }
    }
}
