using System;
using System.Data;
using KnightMoves.SqlObjects.SqlCode;
using Xunit;

namespace KnightMoves.SqlObjects.Tests.TSql.Fluent
{
    public class FluentApi_Case_IsGreaterThanOrEqualTo_Tests
    {
        private readonly string NL = Environment.NewLine;

        [Fact]
        public void Select_Star_From_Where_Case_IsGreaterThanOrEqualTo_String()
        {
            // ARRANGE
            var sqlObj =
                TSQL
                  .SELECT()
                    .STAR()
                  .FROM("MyTable")
                  .WHERE()
                    .CASE(returnType: SqlDbType.VarChar)
                      .WHEN("t", "SomeColumn").IsEqualTo(99).THEN("Blah")
                      .WHEN("t", "SomeColumn").IsEqualTo(88).THEN("Yak")
                      .ELSE("Pooh")
                    .END().IsGreaterThanOrEqualTo("Yak");

            // ACTION
            var sql = sqlObj.Build();

            var expected = $"  SELECT{NL}" +
                           $"   *{NL}" +
                           $"  FROM [MyTable]{NL}" +
                           $"  WHERE 1=1{NL}" +
                           $"   AND CASE{NL}" +
                           $"     WHEN [t].[SomeColumn] = 99 THEN 'Blah'{NL}" +
                           $"     WHEN [t].[SomeColumn] = 88 THEN 'Yak'{NL}" +
                           $"     ELSE 'Pooh'{NL}" +
                           $"    END >= 'Yak'{NL}";

            // ASSERT
            Assert.Equal(expected, sql);
            TestHelper.Assert.SerializationWorks(expected, sqlObj);
        }

        [Fact]
        public void Select_Star_From_Where_Case_IsGreaterThanOrEqualTo_Column()
        {
            // ARRANGE
            var sqlObj =
                TSQL
                  .SELECT()
                    .STAR()
                  .FROM("MyTable")
                  .WHERE()
                    .CASE(returnType: SqlDbType.VarChar)
                      .WHEN("t", "SomeColumn").IsEqualTo(99).THEN("t", "ColumnA")
                      .WHEN("t", "SomeColumn").IsEqualTo(88).THEN("t", "ColumnB")
                      .ELSE("t", "ColumnC")
                    .END().IsGreaterThanOrEqualTo("t", "OtherColumn");

            // ACTION
            var sql = sqlObj.Build();

            var expected = $"  SELECT{NL}" +
                           $"   *{NL}" +
                           $"  FROM [MyTable]{NL}" +
                           $"  WHERE 1=1{NL}" +
                           $"   AND CASE{NL}" +
                           $"     WHEN [t].[SomeColumn] = 99 THEN [t].[ColumnA]{NL}" +
                           $"     WHEN [t].[SomeColumn] = 88 THEN [t].[ColumnB]{NL}" +
                           $"     ELSE [t].[ColumnC]{NL}" +
                           $"    END >= [t].[OtherColumn]{NL}";

            // ASSERT
            Assert.Equal(expected, sql);
            TestHelper.Assert.SerializationWorks(expected, sqlObj);
        }

        [Fact]
        public void Select_Star_From_Where_Case_IsGreaterThanOrEqualTo_Int()
        {
            // ARRANGE
            var sqlObj =
                TSQL
                  .SELECT()
                    .STAR()
                  .FROM("MyTable")
                  .WHERE()
                    .CASE(returnType: SqlDbType.Int)
                      .WHEN("t", "SomeColumn").IsEqualTo(99).THEN(1)
                      .WHEN("t", "SomeColumn").IsEqualTo(88).THEN(2)
                      .ELSE(3)
                    .END().IsGreaterThanOrEqualTo(3);

            // ACTION
            var sql = sqlObj.Build();

            var expected = $"  SELECT{NL}" +
                           $"   *{NL}" +
                           $"  FROM [MyTable]{NL}" +
                           $"  WHERE 1=1{NL}" +
                           $"   AND CASE{NL}" +
                           $"     WHEN [t].[SomeColumn] = 99 THEN 1{NL}" +
                           $"     WHEN [t].[SomeColumn] = 88 THEN 2{NL}" +
                           $"     ELSE 3{NL}" +
                           $"    END >= 3{NL}";

            // ASSERT
            Assert.Equal(expected, sql);
            TestHelper.Assert.SerializationWorks(expected, sqlObj);
        }

        [Fact]
        public void Select_Star_From_Where_Case_IsGreaterThanOrEqualTo_DateTime()
        {
            // ARRANGE
            var TODAY = DateTime.Today;
            var YESTERDAY = TODAY.AddDays(-1);
            var TOMORROW = TODAY.AddDays(1);
            var TEST_DATE = TODAY;

            var sqlObj =
                TSQL
                  .SELECT()
                    .STAR()
                  .FROM("MyTable")
                  .WHERE()
                    .CASE(returnType: SqlDbType.DateTime)
                      .WHEN("t", "SomeColumn").IsEqualTo(99).THEN(YESTERDAY)
                      .WHEN("t", "SomeColumn").IsEqualTo(88).THEN(TODAY)
                      .ELSE(TOMORROW)
                    .END().IsGreaterThanOrEqualTo(TEST_DATE);
            
            // ACTION
            var sql = sqlObj.Build();

            var expected = $"  SELECT{NL}" +
                           $"   *{NL}" +
                           $"  FROM [MyTable]{NL}" +
                           $"  WHERE 1=1{NL}" +
                           $"   AND CASE{NL}" +
                           $"     WHEN [t].[SomeColumn] = 99 THEN '{YESTERDAY.ToString(SqlDataType.SQL_DATE_STRING_FORMAT)}'{NL}" +
                           $"     WHEN [t].[SomeColumn] = 88 THEN '{TODAY.ToString(SqlDataType.SQL_DATE_STRING_FORMAT)}'{NL}" +
                           $"     ELSE '{TOMORROW.ToString(SqlDataType.SQL_DATE_STRING_FORMAT)}'{NL}" +
                           $"    END >= '{TEST_DATE.ToString(SqlDataType.SQL_DATE_STRING_FORMAT)}'{NL}";

            // ASSERT
            Assert.Equal(expected, sql);
            TestHelper.Assert.SerializationWorks(expected, sqlObj);
        }

        [Fact]
        public void Select_Star_From_Where_Case_IsGreaterThanOrEqualTo_Bool()
        {
            // ARRANGE
            var sqlObj =
                TSQL
                  .SELECT()
                    .STAR()
                  .FROM("MyTable")
                  .WHERE()
                    .CASE(returnType: SqlDbType.Bit)
                      .WHEN("t", "SomeColumn").IsEqualTo(99).THEN(true)
                      .WHEN("t", "SomeColumn").IsEqualTo(88).THEN(false)
                      .ELSE(false)
                    .END().IsGreaterThanOrEqualTo(true);

            // ACTION
            var sql = sqlObj.Build();

            var expected = $"  SELECT{NL}" +
                           $"   *{NL}" +
                           $"  FROM [MyTable]{NL}" +
                           $"  WHERE 1=1{NL}" +
                           $"   AND CASE{NL}" +
                           $"     WHEN [t].[SomeColumn] = 99 THEN 1{NL}" +
                           $"     WHEN [t].[SomeColumn] = 88 THEN 0{NL}" +
                           $"     ELSE 0{NL}" +
                           $"    END >= 1{NL}";

            // ASSERT
            Assert.Equal(expected, sql);
            TestHelper.Assert.SerializationWorks(expected, sqlObj);
        }

        [Fact]
        public void Select_Star_From_Where_Case_IsGreaterThanOrEqualTo_Long()
        {
            // ARRANGE
            var sqlObj =
                TSQL
                  .SELECT()
                    .STAR()
                  .FROM("MyTable")
                  .WHERE()
                    .CASE(returnType: SqlDbType.BigInt)
                      .WHEN("t", "SomeColumn").IsEqualTo(99).THEN(long.MinValue)
                      .WHEN("t", "SomeColumn").IsEqualTo(88).THEN(long.MaxValue)
                      .ELSE(long.MinValue)
                    .END().IsGreaterThanOrEqualTo(long.MaxValue);

            // ACTION
            var sql = sqlObj.Build();

            var expected = $"  SELECT{NL}" +
                           $"   *{NL}" +
                           $"  FROM [MyTable]{NL}" +
                           $"  WHERE 1=1{NL}" +
                           $"   AND CASE{NL}" +
                           $"     WHEN [t].[SomeColumn] = 99 THEN {long.MinValue}{NL}" +
                           $"     WHEN [t].[SomeColumn] = 88 THEN {long.MaxValue}{NL}" +
                           $"     ELSE {long.MinValue}{NL}" +
                           $"    END >= {long.MaxValue}{NL}";

            // ASSERT
            Assert.Equal(expected, sql);
            TestHelper.Assert.SerializationWorks(expected, sqlObj);
        }

        [Fact]
        public void Select_Star_From_Where_Case_IsGreaterThanOrEqualTo_Guid()
        {
            // ARRANGE
            var guid1 = Guid.NewGuid();
            var guid2 = Guid.NewGuid();
            var guid3 = Guid.NewGuid();
            var guid4 = Guid.NewGuid();

            var sqlObj =
                TSQL
                  .SELECT()
                    .STAR()
                  .FROM("MyTable")
                  .WHERE()
                    .CASE(returnType: SqlDbType.UniqueIdentifier)
                      .WHEN("t", "SomeColumn").IsEqualTo(99).THEN(guid1)
                      .WHEN("t", "SomeColumn").IsEqualTo(88).THEN(guid2)
                      .ELSE(guid3)
                    .END().IsGreaterThanOrEqualTo(guid4);

            // ACTION
            var sql = sqlObj.Build();

            var expected = $"  SELECT{NL}" +
                           $"   *{NL}" +
                           $"  FROM [MyTable]{NL}" +
                           $"  WHERE 1=1{NL}" +
                           $"   AND CASE{NL}" +
                           $"     WHEN [t].[SomeColumn] = 99 THEN '{guid1}'{NL}" +
                           $"     WHEN [t].[SomeColumn] = 88 THEN '{guid2}'{NL}" +
                           $"     ELSE '{guid3}'{NL}" +
                           $"    END >= '{guid4}'{NL}";

            // ASSERT
            Assert.Equal(expected, sql);
            TestHelper.Assert.SerializationWorks(expected, sqlObj);
        }

        [Fact]
        public void Select_Star_From_Where_Case_IsGreaterThanOrEqualTo_Char()
        {
            // ARRANGE
            var sqlObj =
                TSQL
                  .SELECT()
                    .STAR()
                  .FROM("MyTable")
                  .WHERE()
                    .CASE(returnType: SqlDbType.Char)
                      .WHEN("t", "SomeColumn").IsEqualTo(99).THEN('A')
                      .WHEN("t", "SomeColumn").IsEqualTo(88).THEN('B')
                      .ELSE('C')
                    .END().IsGreaterThanOrEqualTo('C');

            // ACTION
            var sql = sqlObj.Build();

            var expected = $"  SELECT{NL}" +
                           $"   *{NL}" +
                           $"  FROM [MyTable]{NL}" +
                           $"  WHERE 1=1{NL}" +
                           $"   AND CASE{NL}" +
                           $"     WHEN [t].[SomeColumn] = 99 THEN 'A'{NL}" +
                           $"     WHEN [t].[SomeColumn] = 88 THEN 'B'{NL}" +
                           $"     ELSE 'C'{NL}" +
                           $"    END >= 'C'{NL}";

            // ASSERT
            Assert.Equal(expected, sql);
            TestHelper.Assert.SerializationWorks(expected, sqlObj);
        }

        [Fact]
        public void Select_Star_From_Where_Case_IsGreaterThanOrEqualTo_Decimal()
        {
            // ARRANGE
            var sqlObj =
                TSQL
                  .SELECT()
                    .STAR()
                  .FROM("MyTable")
                  .WHERE()
                    .CASE(returnType: SqlDbType.Decimal)
                      .WHEN("t", "SomeColumn").IsEqualTo(99).THEN(decimal.MinValue)
                      .WHEN("t", "SomeColumn").IsEqualTo(88).THEN(decimal.MaxValue)
                      .ELSE(decimal.MinValue)
                    .END().IsGreaterThanOrEqualTo(decimal.MaxValue);

            // ACTION
            var sql = sqlObj.Build();

            var expected = $"  SELECT{NL}" +
                           $"   *{NL}" +
                           $"  FROM [MyTable]{NL}" +
                           $"  WHERE 1=1{NL}" +
                           $"   AND CASE{NL}" +
                           $"     WHEN [t].[SomeColumn] = 99 THEN {decimal.MinValue}{NL}" +
                           $"     WHEN [t].[SomeColumn] = 88 THEN {decimal.MaxValue}{NL}" +
                           $"     ELSE {decimal.MinValue}{NL}" +
                           $"    END >= {decimal.MaxValue}{NL}";

            // ASSERT
            Assert.Equal(expected, sql);
            TestHelper.Assert.SerializationWorks(expected, sqlObj);
        }
    }
}
