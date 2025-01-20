using System;
using KnightMoves.SqlObjects.SqlCode;
using Xunit;

namespace KnightMoves.SqlObjects.Tests.TSql.Fluent;

public class FluentApi_Ops_NullCheck_Tests
{
    private readonly string NL = Environment.NewLine;

    [Fact]
    public void Select_Star_From_Where_IS_NULL()
    {
        // ARRANGE 
        var sqlObj =
            TSQL
              .SELECT()
                .STAR()
              .FROM(null, "MyTable", "t")
              .WHERE("t", "MyColumn").IS_NULL();

        // ACTION
        var sql = sqlObj.Build();

        var expected = $"  SELECT{NL}" +
                       $"   *{NL}" +
                       $"  FROM [MyTable] t{NL}" +
                       $"  WHERE 1=1{NL}" +
                       $"   AND [t].[MyColumn] IS NULL{NL}";

        // ASSERT
        Assert.Equal(expected, sql);
        TestHelper.Assert.SerializationWorks(expected, sqlObj);
    }

    [Fact]
    public void Select_Star_From_Where_IS_NOT_NULL()
    {
        // ARRANGE
        var sqlObj =
            TSQL
              .SELECT()
                .STAR()
              .FROM(null, "MyTable", "t")
              .WHERE("t", "MyColumn").IS_NOT_NULL();

        // ACTION
        var sql = sqlObj.Build();

        var expected = $"  SELECT{NL}" +
                       $"   *{NL}" +
                       $"  FROM [MyTable] t{NL}" +
                       $"  WHERE 1=1{NL}" +
                       $"   AND [t].[MyColumn] IS NOT NULL{NL}";

        // ASSERT
        Assert.Equal(expected, sql);
        TestHelper.Assert.SerializationWorks(expected, sqlObj);
    }
}
