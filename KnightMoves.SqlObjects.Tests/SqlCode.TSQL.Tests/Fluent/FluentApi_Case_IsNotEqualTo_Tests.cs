/*
    THE LICENSED WORK IS PROVIDED UNDER THE TERMS OF THE DEVELOPER TOOL LIMITED 
    LICENSE (“LICENSE”) AS FIRST COMPLETED BY THE ORIGINAL AUTHOR. ANY USE, PUBLIC 
    DISPLAY, PUBLIC PERFORMANCE, REPRODUCTION OR DISTRIBUTION OF, OR PREPARATION OF 
    DERIVATIVE WORKS BASED ON THE LICENSED WORK CONSTITUTES RECIPIENT’S ACCEPTANCE 
    OF THIS LICENSE AND ITS TERMS, WHETHER OR NOT SUCH RECIPIENT READS THE TERMS OF THE 
    LICENSE. “LICENSED WORK” AND “RECIPIENT” ARE DEFINED IN THE LICENSE. A COPY OF THE 
    LICENSE IS LOCATED IN THE TEXT FILE ENTITLED “LICENSE.TXT” ACCOMPANYING THE CONTENTS 
    OF THIS FILE. IF A COPY OF THE LICENSE DOES NOT ACCOMPANY THIS FILE, A COPY OF THE 
    LICENSE MAY ALSO BE OBTAINED AT THE FOLLOWING WEB SITE:  
 
    https://docs.knightmovesolutions.com/sql-objects/license.html
*/

using System;
using System.Data;
using KnightMoves.SqlObjects.SqlCode;
using Xunit;

namespace KnightMoves.SqlObjects.Tests.TSql.Fluent;

public class FluentApi_Case_IsNotEqualTo_Tests
{
    private readonly string NL = Environment.NewLine;

    [Fact]
    public void Select_Star_From_Where_Case_IsNotEqualTo_String()
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
                .END().IsNotEqualTo("Yak");

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
                       $"    END <> 'Yak'{NL}";

        // ASSERT
        Assert.Equal(expected, sql);
        TestHelper.Assert.SerializationWorks(expected, sqlObj);
    }

    [Fact]
    public void Select_Star_From_Where_Case_IsNotEqualTo_Column()
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
                .END().IsNotEqualTo("t", "OtherColumn");

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
                       $"    END <> [t].[OtherColumn]{NL}";

        // ASSERT
        Assert.Equal(expected, sql);
        TestHelper.Assert.SerializationWorks(expected, sqlObj);
    }

    [Fact]
    public void Select_Star_From_Where_Case_IsNotEqualTo_Int()
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
                .END().IsNotEqualTo(3);

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
                       $"    END <> 3{NL}";

        // ASSERT
        Assert.Equal(expected, sql);
        TestHelper.Assert.SerializationWorks(expected, sqlObj);
    }

    [Fact]
    public void Select_Star_From_Where_Case_IsNotEqualTo_DateTime()
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
                .END().IsNotEqualTo(TEST_DATE);

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
                       $"    END <> '{TEST_DATE.ToString(SqlDataType.SQL_DATE_STRING_FORMAT)}'{NL}";

        // ASSERT
        Assert.Equal(expected, sql);
        TestHelper.Assert.SerializationWorks(expected, sqlObj);
    }

    [Fact]
    public void Select_Star_From_Where_Case_IsNotEqualTo_Bool()
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
                .END().IsNotEqualTo(true);

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
                       $"    END <> 1{NL}";

        // ASSERT
        Assert.Equal(expected, sql);
        TestHelper.Assert.SerializationWorks(expected, sqlObj);
    }

    [Fact]
    public void Select_Star_From_Where_Case_IsNotEqualTo_Long()
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
                .END().IsNotEqualTo(long.MaxValue);

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
                       $"    END <> {long.MaxValue}{NL}";

        // ASSERT
        Assert.Equal(expected, sql);
        TestHelper.Assert.SerializationWorks(expected, sqlObj);
    }

    [Fact]
    public void Select_Star_From_Where_Case_IsNotEqualTo_Guid()
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
                .END().IsNotEqualTo(guid4);

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
                       $"    END <> '{guid4}'{NL}";

        // ASSERT
        Assert.Equal(expected, sql);
        TestHelper.Assert.SerializationWorks(expected, sqlObj);
    }

    [Fact]
    public void Select_Star_From_Where_Case_IsNotEqualTo_Char()
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
                .END().IsNotEqualTo('C');

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
                       $"    END <> 'C'{NL}";

        // ASSERT
        Assert.Equal(expected, sql);
        TestHelper.Assert.SerializationWorks(expected, sqlObj);
    }

    [Fact]
    public void Select_Star_From_Where_Case_IsNotEqualTo_Decimal()
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
                .END().IsNotEqualTo(decimal.MaxValue);

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
                       $"    END <> {decimal.MaxValue}{NL}";

        // ASSERT
        Assert.Equal(expected, sql);
        TestHelper.Assert.SerializationWorks(expected, sqlObj);
    }
}
