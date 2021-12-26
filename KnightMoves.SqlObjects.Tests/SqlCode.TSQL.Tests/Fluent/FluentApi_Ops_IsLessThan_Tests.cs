using System;
using KnightMoves.SqlObjects.SqlCode;
using Xunit;

namespace KnightMoves.SqlObjects.Tests.TSql.Fluent
{
    public class FluentApi_Ops_IsLessThan_Tests
    {
        private readonly string NL = Environment.NewLine;

        [Fact]
        public void Select_Star_From_Where_IsLessThan_String()
        {
            // ARRANGE
            var sqlObj =
                TSQL
                  .SELECT()
                    .STAR()
                  .FROM("MyTable")
                  .WHERE("t", "MyColumn").IsLessThan("string");

            // ACTION
            var sql = sqlObj.Build();

            var expected = $"  SELECT{NL}" +
                           $"   *{NL}" +
                           $"  FROM [MyTable]{NL}" +
                           $"  WHERE 1=1{NL}" +
                           $"   AND [t].[MyColumn] < 'string'{NL}";

            // ASSERT
            Assert.Equal(expected, sql);
            TestHelper.Assert.SerializationWorks(expected, sqlObj);
        }

        [Fact]
        public void Select_Star_From_Where_IsLessThan_Int()
        {
            // ARRANGE
            var sqlObj =
                TSQL
                  .SELECT()
                    .STAR()
                  .FROM("MyTable")
                  .WHERE("t", "MyColumn").IsLessThan(99);

            // ACTION
            var sql = sqlObj.Build();

            var expected = $"  SELECT{NL}" +
                           $"   *{NL}" +
                           $"  FROM [MyTable]{NL}" +
                           $"  WHERE 1=1{NL}" +
                           $"   AND [t].[MyColumn] < 99{NL}";

            // ASSERT
            Assert.Equal(expected, sql);
            TestHelper.Assert.SerializationWorks(expected, sqlObj);
        }

        [Fact]
        public void Select_Star_From_Where_IsLessThan_DateTime()
        {
            // ARRANGE
            var TEST_DATE = DateTime.Parse("2021-01-15 11:59:59");
            var DATE_STRING = TEST_DATE.ToString(SqlDataType.SQL_DATE_STRING_FORMAT);

            var sqlObj =
                TSQL
                  .SELECT()
                    .STAR()
                  .FROM("MyTable")
                  .WHERE("t", "MyColumn").IsLessThan(TEST_DATE);

            // ACTION
            var sql = sqlObj.Build();

            var expected = $"  SELECT{NL}" +
                           $"   *{NL}" +
                           $"  FROM [MyTable]{NL}" +
                           $"  WHERE 1=1{NL}" +
                           $"   AND [t].[MyColumn] < '{DATE_STRING}'{NL}";

            // ASSERT
            Assert.Equal(expected, sql);
            TestHelper.Assert.SerializationWorks(expected, sqlObj);
        }

        [Fact]
        public void Select_Star_From_Where_IsLessThan_Bool_True()
        {
            // ARRANGE
            var sqlObj =
                TSQL
                  .SELECT()
                    .STAR()
                  .FROM("MyTable")
                  .WHERE("t", "MyColumn").IsLessThan(true);

            // ACTION
            var sql = sqlObj.Build();

            var expected = $"  SELECT{NL}" +
                           $"   *{NL}" +
                           $"  FROM [MyTable]{NL}" +
                           $"  WHERE 1=1{NL}" +
                           $"   AND [t].[MyColumn] < 1{NL}";

            // ASSERT
            Assert.Equal(expected, sql);
            TestHelper.Assert.SerializationWorks(expected, sqlObj);
        }

        [Fact]
        public void Select_Star_From_Where_IsLessThan_Bool_False()
        {
            // ARRANGE
            var sqlObj =
                TSQL
                  .SELECT()
                    .STAR()
                  .FROM("MyTable")
                  .WHERE("t", "MyColumn").IsLessThan(false);

            // ACTION
            var sql = sqlObj.Build();

            var expected = $"  SELECT{NL}" +
                           $"   *{NL}" +
                           $"  FROM [MyTable]{NL}" +
                           $"  WHERE 1=1{NL}" +
                           $"   AND [t].[MyColumn] < 0{NL}";

            // ASSERT
            Assert.Equal(expected, sql);
            TestHelper.Assert.SerializationWorks(expected, sqlObj);
        }

        [Fact]
        public void Select_Star_From_Where_IsLessThan_Long()
        {
            // ARRANGE
            var sqlObj =
                TSQL
                  .SELECT()
                    .STAR()
                  .FROM("MyTable")
                  .WHERE("t", "MyColumn").IsLessThan(long.MinValue);

            // ACTION
            var sql = sqlObj.Build();

            var expected = $"  SELECT{NL}" +
                           $"   *{NL}" +
                           $"  FROM [MyTable]{NL}" +
                           $"  WHERE 1=1{NL}" +
                           $"   AND [t].[MyColumn] < {long.MinValue}{NL}";

            // ASSERT
            Assert.Equal(expected, sql);
            TestHelper.Assert.SerializationWorks(expected, sqlObj);
        }

        [Fact]
        public void Select_Star_From_Where_IsLessThan_Guid()
        {
            // ARRANGE
            var TEST_GUID = Guid.NewGuid();

            var sqlObj =
                TSQL
                  .SELECT()
                    .STAR()
                  .FROM("MyTable")
                  .WHERE("t", "MyColumn").IsLessThan(TEST_GUID);

            // ACTION
            var sql = sqlObj.Build();

            var expected = $"  SELECT{NL}" +
                           $"   *{NL}" +
                           $"  FROM [MyTable]{NL}" +
                           $"  WHERE 1=1{NL}" +
                           $"   AND [t].[MyColumn] < '{TEST_GUID}'{NL}";

            // ASSERT
            Assert.Equal(expected, sql);
            TestHelper.Assert.SerializationWorks(expected, sqlObj);
        }

        [Fact]
        public void Select_Star_From_Where_IsLessThan_Char()
        {
            // ARRANGE
            var sqlObj =
                TSQL
                  .SELECT()
                    .STAR()
                  .FROM("MyTable")
                  .WHERE("t", "MyColumn").IsLessThan('X');

            // ACTION
            var sql = sqlObj.Build();

            var expected = $"  SELECT{NL}" +
                           $"   *{NL}" +
                           $"  FROM [MyTable]{NL}" +
                           $"  WHERE 1=1{NL}" +
                           $"   AND [t].[MyColumn] < 'X'{NL}";

            // ASSERT
            Assert.Equal(expected, sql);
            TestHelper.Assert.SerializationWorks(expected, sqlObj);
        }

        [Fact]
        public void Select_Star_From_Where_IsLessThan_Decimal()
        {
            // ARRANGE
            var sqlObj =
                TSQL
                  .SELECT()
                    .STAR()
                  .FROM("MyTable")
                  .WHERE("t", "MyColumn").IsLessThan(decimal.MinValue);

            // ACTION
            var sql = sqlObj.Build();

            var expected = $"  SELECT{NL}" +
                           $"   *{NL}" +
                           $"  FROM [MyTable]{NL}" +
                           $"  WHERE 1=1{NL}" +
                           $"   AND [t].[MyColumn] < {decimal.MinValue}{NL}";

            // ASSERT
            Assert.Equal(expected, sql);
            TestHelper.Assert.SerializationWorks(expected, sqlObj);
        }

        [Fact]
        public void Select_Star_From_Where_IsLessThan_Column_with_MultipartIdentifier()
        {
            // ARRANGE
            var sqlObj =
                TSQL
                  .SELECT()
                    .STAR()
                  .FROM("MyTable")
                  .WHERE("t", "MyColumn").IsLessThan("t", "AnotherColumn");

            // ACTION
            var sql = sqlObj.Build();

            var expected = $"  SELECT{NL}" +
                           $"   *{NL}" +
                           $"  FROM [MyTable]{NL}" +
                           $"  WHERE 1=1{NL}" +
                           $"   AND [t].[MyColumn] < [t].[AnotherColumn]{NL}";

            // ASSERT
            Assert.Equal(expected, sql);
            TestHelper.Assert.SerializationWorks(expected, sqlObj);
        }

        [Fact]
        public void Select_Star_From_Where_IsLessThan_Column_Delimited_with_Brackets()
        {
            // ARRANGE
            var sqlObj =
                TSQL
                  .SELECT()
                    .STAR()
                  .FROM("MyTable")
                  .WHERE("t", "MyColumn").IsLessThan("[AnotherColumn]");

            // ACTION
            var sql = sqlObj.Build();

            var expected = $"  SELECT{NL}" +
                           $"   *{NL}" +
                           $"  FROM [MyTable]{NL}" +
                           $"  WHERE 1=1{NL}" +
                           $"   AND [t].[MyColumn] < [AnotherColumn]{NL}";

            // ASSERT
            Assert.Equal(expected, sql);
            TestHelper.Assert.SerializationWorks(expected, sqlObj);
        }

        [Fact]
        public void Select_Star_From_Where_IsLessThan_Var()
        {
            // ARRANGE
            var sqlObj =
                TSQL
                  .SELECT()
                    .STAR()
                  .FROM("MyTable")
                  .WHERE("t", "MyColumn").IsLessThan("@someVar");

            // ACTION
            var sql = sqlObj.Build();

            var expected = $"  SELECT{NL}" +
                           $"   *{NL}" +
                           $"  FROM [MyTable]{NL}" +
                           $"  WHERE 1=1{NL}" +
                           $"   AND [t].[MyColumn] < @someVar{NL}";

            // ASSERT
            Assert.Equal(expected, sql);
            TestHelper.Assert.SerializationWorks(expected, sqlObj);
        }
    }
}
