using System;
using Xunit;

namespace KnightMoves.SqlObjects.Tests.TSql.Fluent;

public class FluentApi_SubQuery_Tests
{
    private readonly string NL = Environment.NewLine;

    [Fact]
    public void Select_SubQuery_From()
    {
        // ARRANGE
        var sqlObj =
            TSQL
              .SELECT()
                .SubQueryStart()
                  .SELECT()
                    .COLUMN("SomeColumn")
                  .FROM("s", "SomeTable")
                  .WHERE("Id").IsEqualTo(99)
                .SubQueryEnd().AS("Qry")
              .FROM("dbo", "MyTable", "t");

        // ACTION
        var sql = sqlObj.Build();

        var expected = $"  SELECT{NL}" +
                       $"   ({NL}" +
                       $"    SELECT{NL}" +
                       $"     [SomeColumn]{NL}" +
                       $"    FROM [s].[SomeTable]{NL}" +
                       $"    WHERE 1=1{NL}" +
                       $"     AND [Id] = 99{NL}" +
                       $"   ) AS [Qry]{NL}" +
                       $"  FROM [dbo].[MyTable] t{NL}";

        // ASSERT
        Assert.Equal(expected, sql);
        TestHelper.Assert.SerializationWorks(expected, sqlObj);
    }

    [Fact]
    public void Select_Start_From_SubQuery()
    {
        // ARRANGE
        var sqlObj =
            TSQL
              .SELECT()
                .STAR()
              .FROM()
                .SubQueryStart()
                  .SELECT()
                    .COLUMN("SomeColumn")
                    .COLUMN("AnotherColumn")
                  .FROM("s", "SomeTable")
                  .WHERE("Foo").IsGreaterThan(99)
                .SubQueryEnd().AS("Qry");

        // ACTION
        var sql = sqlObj.Build();

        var expected = $"  SELECT{NL}" +
                       $"   *{NL}" +
                       $"  FROM {NL}" +
                       $"  ({NL}" +
                       $"   SELECT{NL}" +
                       $"    [SomeColumn],{NL}" +
                       $"    [AnotherColumn]{NL}" +
                       $"   FROM [s].[SomeTable]{NL}" +
                       $"   WHERE 1=1{NL}" +
                       $"    AND [Foo] > 99{NL}" +
                       $"  ) AS [Qry]{NL}";

        // ASSERT
        Assert.Equal(expected, sql);
        TestHelper.Assert.SerializationWorks(expected, sqlObj);
    }
}
