using System;
using KnightMoves.SqlObjects.SqlCode;
using Xunit;

namespace KnightMoves.SqlObjects.Tests.TSql.Fluent;

public class FluentApi_Ops_IsLessThanOrEqualTo_Tests
{
    private readonly string NL = Environment.NewLine;

    [Fact]
    public void Select_Star_From_Where_IsLessThanOrEqualTo_String()
    {
        // ARRANGE
        var sqlObj =
            TSQL
              .SELECT()
                .STAR()
              .FROM("MyTable")
              .WHERE("t", "MyColumn").IsLessThanOrEqualTo("string");

        // ACTION
        var sql = sqlObj.Build();

        var expected = $"  SELECT{NL}" +
                       $"   *{NL}" +
                       $"  FROM [MyTable]{NL}" +
                       $"  WHERE 1=1{NL}" +
                       $"   AND [t].[MyColumn] <= 'string'{NL}";

        // ASSERT
        Assert.Equal(expected, sql);
        TestHelper.Assert.SerializationWorks(expected, sqlObj);
    }

    [Fact]
    public void Select_Star_From_Where_IsLessThanOrEqualTo_Int()
    {
        // ARRANGE
        var sqlObj =
            TSQL
              .SELECT()
                .STAR()
              .FROM("MyTable")
              .WHERE("t", "MyColumn").IsLessThanOrEqualTo(99);

        // ACTION
        var sql = sqlObj.Build();

        var expected = $"  SELECT{NL}" +
                       $"   *{NL}" +
                       $"  FROM [MyTable]{NL}" +
                       $"  WHERE 1=1{NL}" +
                       $"   AND [t].[MyColumn] <= 99{NL}";

        // ASSERT
        Assert.Equal(expected, sql);
        TestHelper.Assert.SerializationWorks(expected, sqlObj);
    }

    [Fact]
    public void Select_Star_From_Where_IsLessThanOrEqualTo_DateTime()
    {
        // ARRANGE
        var TEST_DATE = DateTime.Parse("2021-01-15 11:59:59");
        var DATE_STRING = TEST_DATE.ToString(SqlDataType.SQL_DATE_STRING_FORMAT);

        var sqlObj =
            TSQL
              .SELECT()
                .STAR()
              .FROM("MyTable")
              .WHERE("t", "MyColumn").IsLessThanOrEqualTo(TEST_DATE);

        // ACTION
        var sql = sqlObj.Build();

        var expected = $"  SELECT{NL}" +
                       $"   *{NL}" +
                       $"  FROM [MyTable]{NL}" +
                       $"  WHERE 1=1{NL}" +
                       $"   AND [t].[MyColumn] <= '{DATE_STRING}'{NL}";

        // ASSERT
        Assert.Equal(expected, sql);
        TestHelper.Assert.SerializationWorks(expected, sqlObj);
    }

    [Fact]
    public void Select_Star_From_Where_IsLessThanOrEqualTo_Bool_True()
    {
        // ARRANGE
        var sqlObj =
            TSQL
              .SELECT()
                .STAR()
              .FROM("MyTable")
              .WHERE("t", "MyColumn").IsLessThanOrEqualTo(true);

        // ACTION
        var sql = sqlObj.Build();

        var expected = $"  SELECT{NL}" +
                       $"   *{NL}" +
                       $"  FROM [MyTable]{NL}" +
                       $"  WHERE 1=1{NL}" +
                       $"   AND [t].[MyColumn] <= 1{NL}";

        // ASSERT
        Assert.Equal(expected, sql);
        TestHelper.Assert.SerializationWorks(expected, sqlObj);
    }

    [Fact]
    public void Select_Star_From_Where_IsLessThanOrEqualTo_Bool_False()
    {
        // ARRANGE
        var sqlObj =
            TSQL
              .SELECT()
                .STAR()
              .FROM("MyTable")
              .WHERE("t", "MyColumn").IsLessThanOrEqualTo(false);

        // ACTION
        var sql = sqlObj.Build();

        var expected = $"  SELECT{NL}" +
                       $"   *{NL}" +
                       $"  FROM [MyTable]{NL}" +
                       $"  WHERE 1=1{NL}" +
                       $"   AND [t].[MyColumn] <= 0{NL}";

        // ASSERT
        Assert.Equal(expected, sql);
        TestHelper.Assert.SerializationWorks(expected, sqlObj);
    }

    [Fact]
    public void Select_Star_From_Where_IsLessThanOrEqualTo_Long()
    {
        // ARRANGE
        var sqlObj =
            TSQL
              .SELECT()
                .STAR()
              .FROM("MyTable")
              .WHERE("t", "MyColumn").IsLessThanOrEqualTo(long.MinValue);

        // ACTION
        var sql = sqlObj.Build();

        var expected = $"  SELECT{NL}" +
                       $"   *{NL}" +
                       $"  FROM [MyTable]{NL}" +
                       $"  WHERE 1=1{NL}" +
                       $"   AND [t].[MyColumn] <= {long.MinValue}{NL}";

        // ASSERT
        Assert.Equal(expected, sql);
        TestHelper.Assert.SerializationWorks(expected, sqlObj);
    }

    [Fact]
    public void Select_Star_From_Where_IsLessThanOrEqualTo_Guid()
    {
        // ARRANGE
        var TEST_GUID = Guid.NewGuid();

        var sqlObj =
            TSQL
              .SELECT()
                .STAR()
              .FROM("MyTable")
              .WHERE("t", "MyColumn").IsLessThanOrEqualTo(TEST_GUID);

        // ACTION
        var sql = sqlObj.Build();

        var expected = $"  SELECT{NL}" +
                       $"   *{NL}" +
                       $"  FROM [MyTable]{NL}" +
                       $"  WHERE 1=1{NL}" +
                       $"   AND [t].[MyColumn] <= '{TEST_GUID}'{NL}";

        // ASSERT
        Assert.Equal(expected, sql);
        TestHelper.Assert.SerializationWorks(expected, sqlObj);
    }

    [Fact]
    public void Select_Star_From_Where_IsLessThanOrEqualTo_Char()
    {
        // ARRANGE
        var sqlObj =
            TSQL
              .SELECT()
                .STAR()
              .FROM("MyTable")
              .WHERE("t", "MyColumn").IsLessThanOrEqualTo('X');

        // ACTION
        var sql = sqlObj.Build();

        var expected = $"  SELECT{NL}" +
                       $"   *{NL}" +
                       $"  FROM [MyTable]{NL}" +
                       $"  WHERE 1=1{NL}" +
                       $"   AND [t].[MyColumn] <= 'X'{NL}";

        // ASSERT
        Assert.Equal(expected, sql);
        TestHelper.Assert.SerializationWorks(expected, sqlObj);
    }

    [Fact]
    public void Select_Star_From_Where_IsLessThanOrEqualTo_Decimal()
    {
        // ARRANGE
        var sqlObj =
            TSQL
              .SELECT()
                .STAR()
              .FROM("MyTable")
              .WHERE("t", "MyColumn").IsLessThanOrEqualTo(decimal.MinValue);

        // ACTION
        var sql = sqlObj.Build();

        var expected = $"  SELECT{NL}" +
                       $"   *{NL}" +
                       $"  FROM [MyTable]{NL}" +
                       $"  WHERE 1=1{NL}" +
                       $"   AND [t].[MyColumn] <= {decimal.MinValue}{NL}";

        // ASSERT
        Assert.Equal(expected, sql);
        TestHelper.Assert.SerializationWorks(expected, sqlObj);
    }

    [Fact]
    public void Select_Star_From_Where_IsLessThanOrEqualTo_Column_with_MultipartIdentifier()
    {
        // ARRANGE
        var sqlObj =
            TSQL
              .SELECT()
                .STAR()
              .FROM("MyTable")
              .WHERE("t", "MyColumn").IsLessThanOrEqualTo("t", "AnotherColumn");

        // ACTION
        var sql = sqlObj.Build();

        var expected = $"  SELECT{NL}" +
                       $"   *{NL}" +
                       $"  FROM [MyTable]{NL}" +
                       $"  WHERE 1=1{NL}" +
                       $"   AND [t].[MyColumn] <= [t].[AnotherColumn]{NL}";

        // ASSERT
        Assert.Equal(expected, sql);
        TestHelper.Assert.SerializationWorks(expected, sqlObj);
    }

    [Fact]
    public void Select_Star_From_Where_IsLessThanOrEqualTo_Column_Delimited_with_Brackets()
    {
        // ARRANGE
        var sqlObj =
            TSQL
              .SELECT()
                .STAR()
              .FROM("MyTable")
              .WHERE("t", "MyColumn").IsLessThanOrEqualTo("[AnotherColumn]");

        // ACTION
        var sql = sqlObj.Build();

        var expected = $"  SELECT{NL}" +
                       $"   *{NL}" +
                       $"  FROM [MyTable]{NL}" +
                       $"  WHERE 1=1{NL}" +
                       $"   AND [t].[MyColumn] <= [AnotherColumn]{NL}";

        // ASSERT
        Assert.Equal(expected, sql);
        TestHelper.Assert.SerializationWorks(expected, sqlObj);
    }

    [Fact]
    public void Select_Star_From_Where_IsLessThanOrEqualTo_Var()
    {
        // ARRANGE
        var sqlObj =
            TSQL
              .SELECT()
                .STAR()
              .FROM("MyTable")
              .WHERE("t", "MyColumn").IsLessThanOrEqualTo("@someVar");

        // ACTION
        var sql = sqlObj.Build();

        var expected = $"  SELECT{NL}" +
                       $"   *{NL}" +
                       $"  FROM [MyTable]{NL}" +
                       $"  WHERE 1=1{NL}" +
                       $"   AND [t].[MyColumn] <= @someVar{NL}";

        // ASSERT
        Assert.Equal(expected, sql);
        TestHelper.Assert.SerializationWorks(expected, sqlObj);
    }
}
