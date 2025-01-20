using System;
using System.Data;
using KnightMoves.SqlObjects.SqlCode;
using KnightMoves.SqlObjects.SqlCode.TSQL;
using Xunit;

namespace KnightMoves.SqlObjects.Tests.TSql.Fluent;

public class FluentApi_Where_Tests
{
    private readonly string NL = Environment.NewLine;

    [Fact]
    public void Select_Star_From_Where()
    {
        // ARRANGE
        var sqlObj =
            TSQL
              .SELECT()
                .STAR()
              .FROM("Customers")
              .WHERE();

        // ACTION
        var sql = sqlObj.Build();

        var expected = $"  SELECT{NL}" +
                       $"   *{NL}" +
                       $"  FROM [Customers]{NL}" +
                       $"  WHERE 1=1{NL}";

        // ASSERT
        Assert.Equal(expected, sql);
        TestHelper.Assert.SerializationWorks(expected, sqlObj);
    }

    [Fact]
    public void Select_Star_From_Where_Literal_Throws_Exception()
    {
        // ARRANGE
        var threwException = false;

        // ACTION
        try
        {
            var sql =
                TSQL
                  .SELECT()
                    .STAR()
                  .FROM("Customers")
                  .WHERE(new TSQLLiteral { Value = "@someVar" }).IsEqualTo(99)
                  .Build();
        }
        catch (ArgumentException)
        {
            threwException = true;
        }

        // ASSERT
        Assert.True(threwException);
    }

    [Fact]
    public void Select_Star_From_Where_Literal()
    {
        // ARRANGE
        var sqlObj =
            TSQL
              .SELECT()
                .STAR()
              .FROM("Customers")
              .WHERE(new TSQLLiteral { DataType = new TSQLDataType(SqlDbType.Variant), Value = "@someVar" }).IsEqualTo(99);

        // ACTION
        var sql = sqlObj.Build();

        var expected = $"  SELECT{NL}" +
                       $"   *{NL}" +
                       $"  FROM [Customers]{NL}" +
                       $"  WHERE 1=1{NL}" +
                       $"   AND @someVar = 99{NL}";

        // ASSERT
        Assert.Equal(expected, sql);
        TestHelper.Assert.SerializationWorks(expected, sqlObj);
    }

    [Fact]
    public void Select_Star_From_Where_Basic()
    {
        // ARRANGE
        var sqlObj =
            TSQL
              .SELECT()
                .STAR()
              .FROM("Customers")
              .WHERE("c", "Id").IsEqualTo(99);

        // ACTION
        var sql = sqlObj.Build();

        var expected = $"  SELECT{NL}" +
                       $"   *{NL}" +
                       $"  FROM [Customers]{NL}" +
                       $"  WHERE 1=1{NL}" +
                       $"   AND [c].[Id] = 99{NL}";

        // ASSERT
        Assert.Equal(expected, sql);
        TestHelper.Assert.SerializationWorks(expected, sqlObj);
    }

    [Fact]
    public void Select_Star_From_Where_MultipleConditions()
    {
        // ARRANGE
        var sqlObj =
            TSQL
              .SELECT()
                .STAR()
              .FROM("dbo", "Customers", "c")
              .WHERE("c", "Id").IsEqualTo(99).AND()
                .COLUMN("LastName").IsEqualTo("Jones").AND()
                .COLUMN("FirstName").LIKE("M%").AND()
                .COLUMN("Age").IsGreaterThanOrEqualTo(21).AND()
                .Calculate(2).Times(2).IsEqualTo(4).AND()
                .Literal("21").IsLessThan("c","Age").AND()
                .Literal("@someVar").LIKE("%pattern%").AND()
                .StartConditionScope()
                    .COLUMN("Gender").IsEqualTo("M").OR()
                    .COLUMN("Gender").IsEqualTo("")
                .EndConditionScope().AND()
                .COLUMN("SomeColumn").IN(1,2,3).AND()
                .DATEPART(DateParts.Month, "c", "DOB").IsEqualTo(8).AND()
                .SubQueryStart()
                    .SELECT()
                      .COUNT("*")
                    .FROM("dbo", "Categories")
                .SubQueryEnd().IsGreaterThan(5).AND()
                .CASE(SqlDbType.VarChar)
                    .WHEN("c", "Gender").IsEqualTo("M").THEN("Male")
                    .WHEN("c", "Gender").IsEqualTo("F").THEN("Female")
                    .WHEN("c", "Gender").IsEqualTo("").THEN("Unspecified")
                    .ELSE("Other")
                .END().IsEqualTo("Male");

        // ACTION
        var sql = sqlObj.Build();

        var expected = $"  SELECT{NL}" +
                       $"   *{NL}" +
                       $"  FROM [dbo].[Customers] c{NL}" +
                       $"  WHERE 1=1{NL}" +
                       $"   AND [c].[Id] = 99{NL}" +
                       $"   AND [LastName] = 'Jones'{NL}" +
                       $"   AND [FirstName] LIKE 'M%'{NL}" + 
                       $"   AND [Age] >= 21{NL}" +
                       $"   AND (2 * 2) = 4{NL}" +
                       $"   AND 21 < [c].[Age]{NL}" +
                       $"   AND @someVar LIKE '%pattern%'{NL}" +
                       $"   AND{NL}" +
                       $"   ({NL}" +
                       $"    [Gender] = 'M' OR{NL}" +
                       $"    [Gender] = ''{NL}" +
                       $"   ){NL}" +
                       $"   AND [SomeColumn] IN (1,2,3){NL}" +
                       $"   AND DATEPART(Month, [c].[DOB]) = 8{NL}" +
                       $"   AND ({NL}" +
                       $"     SELECT{NL}" +
                       $"      COUNT(*){NL}" +
                       $"     FROM [dbo].[Categories]{NL}" +
                       $"    ) > 5{NL}" +
                       $"   AND CASE{NL}" +
                       $"     WHEN [c].[Gender] = 'M' THEN 'Male'{NL}" +
                       $"     WHEN [c].[Gender] = 'F' THEN 'Female'{NL}" +
                       $"     WHEN [c].[Gender] = '' THEN 'Unspecified'{NL}" +
                       $"     ELSE 'Other'{NL}" +
                       $"    END = 'Male'{NL}";

        // ASSERT
        Assert.Equal(expected, sql);
        TestHelper.Assert.SerializationWorks(expected, sqlObj);
    }

    [Fact]
    public void Select_Star_From_Where_RightOp_Is_QueryExpression()
    {
        // ARRANGE
        var sqlObj =
            TSQL
              .SELECT()
                .STAR()
              .FROM("Customers")
              .WHERE("c", "Id").IsEqualTo()
                .CASE(SqlDbType.Int)
                    .WHEN("[ColumnA]").IsEqualTo("A").THEN(1)
                    .ELSE(0)
                .END().AND()
                .COLUMN("ColumnB").IsLessThan()
                    .SubQueryStart()
                        .SELECT().COUNT("*").FROM("SomeTable")
                    .SubQueryEnd()
                .AND("[ColumnC]").IsLessThan()
                    .SubQueryStart()
                        .SELECT().COUNT("*").FROM("SomeTable")
                    .SubQueryEnd()
                .AND("[ColumnD]").IsGreaterThan()
                    .CASE(SqlDbType.Int)
                        .WHEN("[ColumnA]").IsEqualTo("A").THEN(1)
                        .ELSE(0)
                    .END();

        // ACTION
        var sql = sqlObj.Build();

        var expected = $"  SELECT{NL}" +
                       $"   *{NL}" +
                       $"  FROM [Customers]{NL}" +
                       $"  WHERE 1=1{NL}" +
                       $"   AND [c].[Id] = CASE{NL}" +
                       $"     WHEN [ColumnA] = 'A' THEN 1{NL}" +
                       $"     ELSE 0{NL}" +
                       $"    END{NL}" + 
                       $"   AND [ColumnB] < ({NL}" +
                       $"     SELECT{NL}" +
                       $"      COUNT(*){NL}" +
                       $"     FROM [SomeTable]{NL}" +
                       $"    ){NL}" +
                       $"   AND [ColumnC] < ({NL}" +
                       $"     SELECT{NL}" +
                       $"      COUNT(*){NL}" +
                       $"     FROM [SomeTable]{NL}" +
                       $"    ){NL}" +
                       $"   AND [ColumnD] > CASE{NL}" +
                       $"     WHEN [ColumnA] = 'A' THEN 1{NL}" +
                       $"     ELSE 0{NL}" +
                       $"    END{NL}";

        // ASSERT
        Assert.Equal(expected, sql);
        TestHelper.Assert.SerializationWorks(expected, sqlObj);
    }
}
