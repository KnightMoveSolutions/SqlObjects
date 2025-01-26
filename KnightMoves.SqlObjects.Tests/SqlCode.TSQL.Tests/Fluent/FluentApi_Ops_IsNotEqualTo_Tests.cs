﻿/*
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
using KnightMoves.SqlObjects.SqlCode;
using Xunit;

namespace KnightMoves.SqlObjects.Tests.TSql.Fluent;

public class FluentApi_Ops_IsNotEqualTo_Tests
{
    private readonly string NL = Environment.NewLine;

    [Fact]
    public void Select_Star_From_Where_IsNotEqualTo_String()
    {
        // ARRANGE
        var sqlObj =
            TSQL
              .SELECT()
                .STAR()
              .FROM("MyTable")
              .WHERE("t", "MyColumn").IsNotEqualTo("string");

        // ACTION
        var sql = sqlObj.Build();

        var expected = $"  SELECT{NL}" +
                       $"   *{NL}" +
                       $"  FROM [MyTable]{NL}" +
                       $"  WHERE 1=1{NL}" +
                       $"   AND [t].[MyColumn] <> 'string'{NL}";

        // ASSERT
        Assert.Equal(expected, sql);
        TestHelper.Assert.SerializationWorks(expected, sqlObj);
    }

    [Fact]
    public void Select_Star_From_Where_IsNotEqualTo_Int()
    {
        // ARRANGE
        var sqlObj =
            TSQL
              .SELECT()
                .STAR()
              .FROM("MyTable")
              .WHERE("t", "MyColumn").IsNotEqualTo(99);

        // ACTION
        var sql = sqlObj.Build();

        var expected = $"  SELECT{NL}" +
                       $"   *{NL}" +
                       $"  FROM [MyTable]{NL}" +
                       $"  WHERE 1=1{NL}" +
                       $"   AND [t].[MyColumn] <> 99{NL}";

        // ASSERT
        Assert.Equal(expected, sql);
        TestHelper.Assert.SerializationWorks(expected, sqlObj);
    }

    [Fact]
    public void Select_Star_From_Where_IsNotEqualTo_DateTime()
    {
        // ARRANGE
        var TEST_DATE = DateTime.Parse("2021-01-15 11:59:59");
        var DATE_STRING = TEST_DATE.ToString(SqlDataType.SQL_DATE_STRING_FORMAT);

        var sqlObj =
            TSQL
              .SELECT()
                .STAR()
              .FROM("MyTable")
              .WHERE("t", "MyColumn").IsNotEqualTo(TEST_DATE);

        // ACTION
        var sql = sqlObj.Build();

        var expected = $"  SELECT{NL}" +
                       $"   *{NL}" +
                       $"  FROM [MyTable]{NL}" +
                       $"  WHERE 1=1{NL}" +
                       $"   AND [t].[MyColumn] <> '{DATE_STRING}'{NL}";

        // ASSERT
        Assert.Equal(expected, sql);
        TestHelper.Assert.SerializationWorks(expected, sqlObj);
    }

    [Fact]
    public void Select_Star_From_Where_IsNotEqualTo_Bool_True()
    {
        // ARRANGE
        var sqlObj =
            TSQL
              .SELECT()
                .STAR()
              .FROM("MyTable")
              .WHERE("t", "MyColumn").IsNotEqualTo(true);

        // ACTION
        var sql = sqlObj.Build();

        var expected = $"  SELECT{NL}" +
                       $"   *{NL}" +
                       $"  FROM [MyTable]{NL}" +
                       $"  WHERE 1=1{NL}" +
                       $"   AND [t].[MyColumn] <> 1{NL}";

        // ASSERT
        Assert.Equal(expected, sql);
        TestHelper.Assert.SerializationWorks(expected, sqlObj);
    }

    [Fact]
    public void Select_Star_From_Where_IsNotEqualTo_Bool_False()
    {
        // ARRANGE
        var sqlObj =
            TSQL
              .SELECT()
                .STAR()
              .FROM("MyTable")
              .WHERE("t", "MyColumn").IsNotEqualTo(false);

        // ACTION
        var sql = sqlObj.Build();

        var expected = $"  SELECT{NL}" +
                       $"   *{NL}" +
                       $"  FROM [MyTable]{NL}" +
                       $"  WHERE 1=1{NL}" +
                       $"   AND [t].[MyColumn] <> 0{NL}";

        // ASSERT
        Assert.Equal(expected, sql);
        TestHelper.Assert.SerializationWorks(expected, sqlObj);
    }

    [Fact]
    public void Select_Star_From_Where_IsNotEqualTo_Long()
    {
        // ARRANGE
        var sqlObj =
            TSQL
              .SELECT()
                .STAR()
              .FROM("MyTable")
              .WHERE("t", "MyColumn").IsNotEqualTo(long.MinValue);

        // ACTION
        var sql = sqlObj.Build();

        var expected = $"  SELECT{NL}" +
                       $"   *{NL}" +
                       $"  FROM [MyTable]{NL}" +
                       $"  WHERE 1=1{NL}" +
                       $"   AND [t].[MyColumn] <> {long.MinValue}{NL}";

        // ASSERT
        Assert.Equal(expected, sql);
        TestHelper.Assert.SerializationWorks(expected, sqlObj);
    }

    [Fact]
    public void Select_Star_From_Where_IsNotEqualTo_Guid()
    {
        // ARRANGE
        var TEST_GUID = Guid.NewGuid();

        var sqlObj =
            TSQL
              .SELECT()
                .STAR()
              .FROM("MyTable")
              .WHERE("t", "MyColumn").IsNotEqualTo(TEST_GUID);

        // ACTION
        var sql = sqlObj.Build();

        var expected = $"  SELECT{NL}" +
                       $"   *{NL}" +
                       $"  FROM [MyTable]{NL}" +
                       $"  WHERE 1=1{NL}" +
                       $"   AND [t].[MyColumn] <> '{TEST_GUID}'{NL}";

        // ASSERT
        Assert.Equal(expected, sql);
        TestHelper.Assert.SerializationWorks(expected, sqlObj);
    }

    [Fact]
    public void Select_Star_From_Where_IsNotEqualTo_Char()
    {
        // ARRANGE
        var sqlObj =
            TSQL
              .SELECT()
                .STAR()
              .FROM("MyTable")
              .WHERE("t", "MyColumn").IsNotEqualTo('X');

        // ACTION
        var sql = sqlObj.Build();

        var expected = $"  SELECT{NL}" +
                       $"   *{NL}" +
                       $"  FROM [MyTable]{NL}" +
                       $"  WHERE 1=1{NL}" +
                       $"   AND [t].[MyColumn] <> 'X'{NL}";

        // ASSERT
        Assert.Equal(expected, sql);
        TestHelper.Assert.SerializationWorks(expected, sqlObj);
    }

    [Fact]
    public void Select_Star_From_Where_IsNotEqualTo_Decimal()
    {
        // ARRANGE
        var sqlObj =
            TSQL
              .SELECT()
                .STAR()
              .FROM("MyTable")
              .WHERE("t", "MyColumn").IsNotEqualTo(decimal.MinValue);

        // ACTION
        var sql = sqlObj.Build();

        var expected = $"  SELECT{NL}" +
                       $"   *{NL}" +
                       $"  FROM [MyTable]{NL}" +
                       $"  WHERE 1=1{NL}" +
                       $"   AND [t].[MyColumn] <> {decimal.MinValue}{NL}";

        // ASSERT
        Assert.Equal(expected, sql);
        TestHelper.Assert.SerializationWorks(expected, sqlObj);
    }

    [Fact]
    public void Select_Star_From_Where_IsNotEqualTo_Column_with_MultipartIdentifier()
    {
        // ARRANGE
        var sqlObj =
            TSQL
              .SELECT()
                .STAR()
              .FROM("MyTable")
              .WHERE("t", "MyColumn").IsNotEqualTo("t", "AnotherColumn");

        // ACTION
        var sql = sqlObj.Build();

        var expected = $"  SELECT{NL}" +
                       $"   *{NL}" +
                       $"  FROM [MyTable]{NL}" +
                       $"  WHERE 1=1{NL}" +
                       $"   AND [t].[MyColumn] <> [t].[AnotherColumn]{NL}";

        // ASSERT
        Assert.Equal(expected, sql);
        TestHelper.Assert.SerializationWorks(expected, sqlObj);
    }

    [Fact]
    public void Select_Star_From_Where_IsNotEqualTo_Column_Delimited_with_Brackets()
    {
        // ARRANGE
        var sqlObj =
            TSQL
              .SELECT()
                .STAR()
              .FROM("MyTable")
              .WHERE("t", "MyColumn").IsNotEqualTo("[AnotherColumn]");

        // ACTION
        var sql = sqlObj.Build();

        var expected = $"  SELECT{NL}" +
                       $"   *{NL}" +
                       $"  FROM [MyTable]{NL}" +
                       $"  WHERE 1=1{NL}" +
                       $"   AND [t].[MyColumn] <> [AnotherColumn]{NL}";

        // ASSERT
        Assert.Equal(expected, sql);
        TestHelper.Assert.SerializationWorks(expected, sqlObj);
    }

    [Fact]
    public void Select_Star_From_Where_IsNotEqualTo_Var()
    {
        // ARRANGE
        var sqlObj =
            TSQL
              .SELECT()
                .STAR()
              .FROM("MyTable")
              .WHERE("t", "MyColumn").IsNotEqualTo("@someVar");

        // ACTION
        var sql = sqlObj.Build();

        var expected = $"  SELECT{NL}" +
                       $"   *{NL}" +
                       $"  FROM [MyTable]{NL}" +
                       $"  WHERE 1=1{NL}" +
                       $"   AND [t].[MyColumn] <> @someVar{NL}";

        // ASSERT
        Assert.Equal(expected, sql);
        TestHelper.Assert.SerializationWorks(expected, sqlObj);
    }
}
