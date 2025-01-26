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

using KnightMoves.SqlObjects.SqlCode;
using System;
using System.Data;
using Xunit;

namespace KnightMoves.SqlObjects.Tests.TSql.Fluent;

public class FluentApi_Case_Tests
{
    private readonly string NL = Environment.NewLine;

    [Fact]
    public void Select_Case_From_Where()
    {
        // ARRANGE
        var sqlObj =
            TSQL
              .SELECT()
                .CASE(returnType: SqlDbType.VarChar)
                  .WHEN("t", "SomeColumn").IsEqualTo(99).THEN("Blah")
                  .WHEN("t", "SomeColumn").IsEqualTo(88).THEN("Yak")
                  .ELSE("Pooh")
                .END()
              .FROM("MyTable")
              .WHERE("t", "MyColumn").IsEqualTo(123);

        // ACTION
        var sql = sqlObj.Build();

        var expected = $"  SELECT{NL}" +
                       $"   CASE{NL}" +
                       $"    WHEN [t].[SomeColumn] = 99 THEN 'Blah'{NL}" +
                       $"    WHEN [t].[SomeColumn] = 88 THEN 'Yak'{NL}" +
                       $"    ELSE 'Pooh'{NL}" +
                       $"   END{NL}" +
                       $"  FROM [MyTable]{NL}" +
                       $"  WHERE 1=1{NL}" +
                       $"   AND [t].[MyColumn] = 123{NL}";

        // ASSERT
        Assert.Equal(expected, sql);
        TestHelper.Assert.SerializationWorks(expected, sqlObj);
    }

    [Fact]
    public void Select_CaseAs_From_Where()
    {
        // ARRANGE
        var sqlObj =
            TSQL
              .SELECT()
                .CASE(returnType: SqlDbType.VarChar)
                  .WHEN("t", "SomeColumn").IsEqualTo(99).THEN("Blah")
                  .WHEN("t", "SomeColumn").IsEqualTo(88).THEN("Yak")
                  .ELSE("Pooh")
                .END().AS("CaseResult")
              .FROM("MyTable")
              .WHERE("t", "MyColumn").IsEqualTo(123);

        // ACTION
        var sql = sqlObj.Build();

        var expected = $"  SELECT{NL}" +
                       $"   CASE{NL}" +
                       $"    WHEN [t].[SomeColumn] = 99 THEN 'Blah'{NL}" +
                       $"    WHEN [t].[SomeColumn] = 88 THEN 'Yak'{NL}" +
                       $"    ELSE 'Pooh'{NL}" +
                       $"   END AS [CaseResult]{NL}" +
                       $"  FROM [MyTable]{NL}" +
                       $"  WHERE 1=1{NL}" +
                       $"   AND [t].[MyColumn] = 123{NL}";

        // ASSERT
        Assert.Equal(expected, sql);
        TestHelper.Assert.SerializationWorks(expected, sqlObj);
    }

    [Fact]
    public void Select_Star_From_Where_String_IsEqualTo_Case()
    {
        // ARRANGE
        var sqlObj =
            TSQL
              .SELECT()
                .STAR()
              .FROM("MyTable")
              .WHERE("Something").IsEqualTo()
                .CASE(returnType: SqlDbType.VarChar)
                  .WHEN("t", "SomeColumn").IsEqualTo(99).THEN("Blah")
                  .WHEN("t", "SomeColumn").IsEqualTo(88).THEN("Yak")
                  .ELSE("Pooh")
                .END();

        // ACTION
        var sql = sqlObj.Build();

        var expected = $"  SELECT{NL}" +
                       $"   *{NL}" +
                       $"  FROM [MyTable]{NL}" +
                       $"  WHERE 1=1{NL}" +
                       $"   AND 'Something' = CASE{NL}" +
                       $"     WHEN [t].[SomeColumn] = 99 THEN 'Blah'{NL}" +
                       $"     WHEN [t].[SomeColumn] = 88 THEN 'Yak'{NL}" +
                       $"     ELSE 'Pooh'{NL}" +
                       $"    END{NL}";

        // ASSERT
        Assert.Equal(expected, sql);
        TestHelper.Assert.SerializationWorks(expected, sqlObj);
    }

    [Fact]
    public void Select_Star_From_Where_Int_IsEqualTo_Case()
    {
        // ARRANGE
        var sqlObj =
            TSQL
              .SELECT()
                .STAR()
              .FROM("MyTable")
              .WHERE(1).IsEqualTo()
                .CASE(returnType: SqlDbType.Int)
                  .WHEN("t", "SomeColumn").IsEqualTo(99).THEN(1)
                  .WHEN("t", "SomeColumn").IsEqualTo(88).THEN(2)
                  .ELSE(3)
                .END();

        // ACTION
        var sql = sqlObj.Build();

        var expected = $"  SELECT{NL}" +
                       $"   *{NL}" +
                       $"  FROM [MyTable]{NL}" +
                       $"  WHERE 1=1{NL}" +
                       $"   AND 1 = CASE{NL}" +
                       $"     WHEN [t].[SomeColumn] = 99 THEN 1{NL}" +
                       $"     WHEN [t].[SomeColumn] = 88 THEN 2{NL}" +
                       $"     ELSE 3{NL}" +
                       $"    END{NL}";

        // ASSERT
        Assert.Equal(expected, sql);
        TestHelper.Assert.SerializationWorks(expected, sqlObj);
    }

    [Fact]
    public void Select_Star_From_Where_DateTime_IsEqualTo_Case()
    {
        // ARRANGE
        var TODAY = DateTime.Today;
        var YESTERDAY = TODAY.AddDays(-1);
        var TOMORROW = TODAY.AddDays(1);

        var sqlObj =
            TSQL
              .SELECT()
                .STAR()
              .FROM("MyTable")
              .WHERE(TODAY).IsEqualTo()
                .CASE(returnType: SqlDbType.DateTime)
                  .WHEN("t", "SomeColumn").IsEqualTo(99).THEN(YESTERDAY)
                  .WHEN("t", "SomeColumn").IsEqualTo(88).THEN(TODAY)
                  .ELSE(TOMORROW)
                .END();

        // ACTION
        var sql = sqlObj.Build();

        var expected = $"  SELECT{NL}" +
                       $"   *{NL}" +
                       $"  FROM [MyTable]{NL}" +
                       $"  WHERE 1=1{NL}" +
                       $"   AND '{TODAY.ToString(SqlDataType.SQL_DATE_STRING_FORMAT)}' = CASE{NL}" +
                       $"     WHEN [t].[SomeColumn] = 99 THEN '{YESTERDAY.ToString(SqlDataType.SQL_DATE_STRING_FORMAT)}'{NL}" +
                       $"     WHEN [t].[SomeColumn] = 88 THEN '{TODAY.ToString(SqlDataType.SQL_DATE_STRING_FORMAT)}'{NL}" +
                       $"     ELSE '{TOMORROW.ToString(SqlDataType.SQL_DATE_STRING_FORMAT)}'{NL}" +
                       $"    END{NL}";

        // ASSERT
        Assert.Equal(expected, sql);
        TestHelper.Assert.SerializationWorks(expected, sqlObj);
    }

    [Fact]
    public void Select_Star_From_Where_Bool_IsEqualTo_Case()
    {
        // ARRANGE
        var sqlObj =
            TSQL
              .SELECT()
                .STAR()
              .FROM("MyTable")
              .WHERE(true).IsEqualTo()
                .CASE(returnType: SqlDbType.Bit)
                  .WHEN("t", "SomeColumn").IsEqualTo(99).THEN(false)
                  .WHEN("t", "SomeColumn").IsEqualTo(88).THEN(true)
                  .ELSE(true)
                .END();

        // ACTION
        var sql = sqlObj.Build();

        var expected = $"  SELECT{NL}" +
                       $"   *{NL}" +
                       $"  FROM [MyTable]{NL}" +
                       $"  WHERE 1=1{NL}" +
                       $"   AND 1 = CASE{NL}" +
                       $"     WHEN [t].[SomeColumn] = 99 THEN 0{NL}" +
                       $"     WHEN [t].[SomeColumn] = 88 THEN 1{NL}" +
                       $"     ELSE 1{NL}" +
                       $"    END{NL}";

        // ASSERT
        Assert.Equal(expected, sql);
        TestHelper.Assert.SerializationWorks(expected, sqlObj);
    }

    [Fact]
    public void Select_Star_From_Where_Long_IsEqualTo_Case()
    {
        // ARRANGE
        var sqlObj =
            TSQL
              .SELECT()
                .STAR()
              .FROM("MyTable")
              .WHERE(long.MinValue).IsEqualTo()
                .CASE(returnType: SqlDbType.BigInt)
                  .WHEN("t", "SomeColumn").IsEqualTo(99).THEN(long.MinValue)
                  .WHEN("t", "SomeColumn").IsEqualTo(88).THEN(long.MaxValue)
                  .ELSE(long.MinValue)
                .END();

        // ACTION
        var sql = sqlObj.Build();

        var expected = $"  SELECT{NL}" +
                       $"   *{NL}" +
                       $"  FROM [MyTable]{NL}" +
                       $"  WHERE 1=1{NL}" +
                       $"   AND {long.MinValue} = CASE{NL}" +
                       $"     WHEN [t].[SomeColumn] = 99 THEN {long.MinValue}{NL}" +
                       $"     WHEN [t].[SomeColumn] = 88 THEN {long.MaxValue}{NL}" +
                       $"     ELSE {long.MinValue}{NL}" +
                       $"    END{NL}";

        // ASSERT
        Assert.Equal(expected, sql);
        TestHelper.Assert.SerializationWorks(expected, sqlObj);
    }

    [Fact]
    public void Select_Star_From_Where_Guid_IsEqualTo_Case()
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
              .WHERE(guid1).IsEqualTo()
                .CASE(returnType: SqlDbType.UniqueIdentifier)
                  .WHEN("t", "SomeColumn").IsEqualTo(99).THEN(guid2)
                  .WHEN("t", "SomeColumn").IsEqualTo(88).THEN(guid3)
                  .ELSE(guid4)
                .END();

        // ACTION
        var sql = sqlObj.Build();

        var expected = $"  SELECT{NL}" +
                       $"   *{NL}" +
                       $"  FROM [MyTable]{NL}" +
                       $"  WHERE 1=1{NL}" +
                       $"   AND '{guid1}' = CASE{NL}" +
                       $"     WHEN [t].[SomeColumn] = 99 THEN '{guid2}'{NL}" +
                       $"     WHEN [t].[SomeColumn] = 88 THEN '{guid3}'{NL}" +
                       $"     ELSE '{guid4}'{NL}" +
                       $"    END{NL}";

        // ASSERT
        Assert.Equal(expected, sql);
        TestHelper.Assert.SerializationWorks(expected, sqlObj);
    }

    [Fact]
    public void Select_Star_From_Where_Char_IsEqualTo_Case()
    {
        // ARRANGE
        var sqlObj =
            TSQL
              .SELECT()
                .STAR()
              .FROM("MyTable")
              .WHERE('A').IsEqualTo()
                .CASE(returnType: SqlDbType.Char)
                  .WHEN("t", "SomeColumn").IsEqualTo(99).THEN('A')
                  .WHEN("t", "SomeColumn").IsEqualTo(88).THEN('B')
                  .ELSE('C')
                .END();

        // ACTION
        var sql = sqlObj.Build();

        var expected = $"  SELECT{NL}" +
                       $"   *{NL}" +
                       $"  FROM [MyTable]{NL}" +
                       $"  WHERE 1=1{NL}" +
                       $"   AND 'A' = CASE{NL}" +
                       $"     WHEN [t].[SomeColumn] = 99 THEN 'A'{NL}" +
                       $"     WHEN [t].[SomeColumn] = 88 THEN 'B'{NL}" +
                       $"     ELSE 'C'{NL}" +
                       $"    END{NL}";

        // ASSERT
        Assert.Equal(expected, sql);
        TestHelper.Assert.SerializationWorks(expected, sqlObj);
    }

    [Fact]
    public void Select_Star_From_Where_Decimal_IsEqualTo_Case()
    {
        // ARRANGE
        var sqlObj =
            TSQL
              .SELECT()
                .STAR()
              .FROM("MyTable")
              .WHERE(decimal.MinValue).IsEqualTo()
                .CASE(returnType: SqlDbType.Decimal)
                  .WHEN("t", "SomeColumn").IsEqualTo(99).THEN(decimal.MinValue)
                  .WHEN("t", "SomeColumn").IsEqualTo(88).THEN(decimal.MaxValue)
                  .ELSE(decimal.MinValue)
                .END();

        // ACTION
        var sql = sqlObj.Build();

        var expected = $"  SELECT{NL}" +
                       $"   *{NL}" +
                       $"  FROM [MyTable]{NL}" +
                       $"  WHERE 1=1{NL}" +
                       $"   AND {decimal.MinValue} = CASE{NL}" +
                       $"     WHEN [t].[SomeColumn] = 99 THEN {decimal.MinValue}{NL}" +
                       $"     WHEN [t].[SomeColumn] = 88 THEN {decimal.MaxValue}{NL}" +
                       $"     ELSE {decimal.MinValue}{NL}" +
                       $"    END{NL}";

        // ASSERT
        Assert.Equal(expected, sql);
        TestHelper.Assert.SerializationWorks(expected, sqlObj);
    }
}
