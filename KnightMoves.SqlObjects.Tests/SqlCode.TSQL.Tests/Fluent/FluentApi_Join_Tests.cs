using System;
using Xunit;

namespace KnightMoves.SqlObjects.Tests.TSql.Fluent;

public class FluentApi_Join_Tests
{
    private readonly string NL = Environment.NewLine;

    [Fact]
    public void Select_Columns_From_with_InnerJoins()
    {
        // ARRANGE
        var sqlObj =
            TSQL
            .SELECT()
                .COLUMN("c", "Id")
            .FROM("dbo", "Customers", "c")
            .INNERJOIN("Profile").ON("ProfileId").IsEqualTo("c", "ProfileId")
            .INNERJOIN("Address", "a").ON("CustomerId").IsEqualTo("c", "Id")
            .INNERJOIN("dbo", "AddressLine", "al").ON("al", "AddressId").IsEqualTo("a", "Id");

        // ACTION
        var sql = sqlObj.Build();

        var expected = $"  SELECT{NL}" +
                       $"   [c].[Id]{NL}" +
                       $"  FROM [dbo].[Customers] c{NL}" +
                       $"  INNER JOIN [Profile] ON [ProfileId] = [c].[ProfileId]{NL}" +
                       $"  INNER JOIN [Address] a ON [CustomerId] = [c].[Id]{NL}" +
                       $"  INNER JOIN [dbo].[AddressLine] al ON [al].[AddressId] = [a].[Id]{NL}";

        // ASSERT
        Assert.Equal(expected, sql);
        TestHelper.Assert.SerializationWorks(expected, sqlObj);
    }

    [Fact]
    public void Select_Columns_From_with_RightJoins()
    {
        // ARRANGE
        var sqlObj =
            TSQL
            .SELECT()
                .COLUMN("c", "Id")
            .FROM("dbo", "Customers", "c")
            .RIGHTJOIN("Profile").ON("ProfileId").IsEqualTo("c", "ProfileId")
            .RIGHTJOIN("dbo", "Address", "a").ON("CustomerId").IsEqualTo("c", "Id")
            .RIGHTJOIN("dbo", "AddressLine", "al").ON("al", "AddressId").IsEqualTo("a", "Id");

        // ACTION
        var sql = sqlObj.Build();

        var expected = $"  SELECT{NL}" +
                       $"   [c].[Id]{NL}" +
                       $"  FROM [dbo].[Customers] c{NL}" +
                       $"  RIGHT JOIN [Profile] ON [ProfileId] = [c].[ProfileId]{NL}" +
                       $"  RIGHT JOIN [dbo].[Address] a ON [CustomerId] = [c].[Id]{NL}" +
                       $"  RIGHT JOIN [dbo].[AddressLine] al ON [al].[AddressId] = [a].[Id]{NL}";

        // ASSERT
        Assert.Equal(expected, sql);
        TestHelper.Assert.SerializationWorks(expected, sqlObj);
    }

    [Fact]
    public void Select_Columns_From_with_LeftJoins()
    {
        // ARRANGE
        var sqlObj =
            TSQL
            .SELECT()
                .COLUMN("c", "Id")
            .FROM("dbo", "Customers", "c")
            .LEFTJOIN("Profile").ON("ProfileId").IsEqualTo("c", "ProfileId")
            .LEFTJOIN("dbo", "Address", "a").ON("CustomerId").IsEqualTo("c", "Id")
            .LEFTJOIN("dbo", "AddressLine", "al").ON("al", "AddressId").IsEqualTo("a", "Id");

        // ACTION
        var sql = sqlObj.Build();

        var expected = $"  SELECT{NL}" +
                       $"   [c].[Id]{NL}" +
                       $"  FROM [dbo].[Customers] c{NL}" +
                       $"  LEFT JOIN [Profile] ON [ProfileId] = [c].[ProfileId]{NL}" +
                       $"  LEFT JOIN [dbo].[Address] a ON [CustomerId] = [c].[Id]{NL}" +
                       $"  LEFT JOIN [dbo].[AddressLine] al ON [al].[AddressId] = [a].[Id]{NL}";

        // ASSERT
        Assert.Equal(expected, sql);
        TestHelper.Assert.SerializationWorks(expected, sqlObj);
    }
}
