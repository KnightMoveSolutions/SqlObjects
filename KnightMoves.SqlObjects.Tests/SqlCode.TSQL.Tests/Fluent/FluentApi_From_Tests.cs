using System;
using Xunit;

namespace KnightMoves.SqlObjects.Tests.TSql.Fluent;

public class FluentApi_From_Tests
{
    private readonly string NL = Environment.NewLine;

    [Fact]
    public void Select_Columns_From()
    {
        // ARRANGE
        var sqlObj =
            TSQL
            .SELECT()
                .COLUMN("Id").AS("UserId")
                .COLUMN("Name")
            .FROM("Customers");

        // ACTION
        var sql = sqlObj.Build();

        var expected = $"  SELECT{NL}" +
                       $"   [Id] AS [UserId],{NL}" +
                       $"   [Name]{NL}" +
                       $"  FROM [Customers]{NL}";

        // ASSERT
        Assert.Equal(expected, sql);
        TestHelper.Assert.SerializationWorks(expected, sqlObj);
    }

    [Fact]
    public void Select_Columns_From_with_Schema()
    {
        // ARRANGE
        var sqlObj =
            TSQL
            .SELECT()
                .COLUMN("Id").AS("UserId")
                .COLUMN("Name")
            .FROM("dbo", "Customers");

        // ACTION
        var sql = sqlObj.Build();

        var expected = $"  SELECT{NL}" +
                       $"   [Id] AS [UserId],{NL}" +
                       $"   [Name]{NL}" +
                       $"  FROM [dbo].[Customers]{NL}";

        // ASSERT
        Assert.Equal(expected, sql);
        TestHelper.Assert.SerializationWorks(expected, sqlObj);
    }

    [Fact]
    public void Select_Columns_From_with_Schema_Alias()
    {
        // ARRANGE
        var sqlObj =
            TSQL
            .SELECT()
                .COLUMN("Id").AS("UserId")
                .COLUMN("Name")
            .FROM("dbo", "Customers", "C");

        // ACTION
        var sql = sqlObj.Build();

        var expected = $"  SELECT{NL}" +
                       $"   [Id] AS [UserId],{NL}" +
                       $"   [Name]{NL}" +
                       $"  FROM [dbo].[Customers] C{NL}";

        // ASSERT
        Assert.Equal(expected, sql);
        TestHelper.Assert.SerializationWorks(expected, sqlObj);
    }
}
