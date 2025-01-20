using System;
using Xunit;

namespace KnightMoves.SqlObjects.Tests.TSql.Fluent;

public class FluentApi_Union_Tests
{
    private readonly string NL = Environment.NewLine;

    [Fact]
    public void Select_Column_From_Union_Select_Column_From()
    {
        // ARRANGE
        var sqlObj =
            TSQL
              .SELECT()
                .COLUMN("SomeColumn")
              .FROM("TableA")

              .UNION()

              .SELECT()
                .COLUMN("SomeColumn")
              .FROM("TableB");

        // ACTION
        var sql = sqlObj.Build();

        var expected = $"  SELECT{NL}" +
                       $"   [SomeColumn]{NL}" +
                       $"  FROM [TableA]{NL}" +
                       $"{NL}" +
                       $"  UNION{NL}" +
                       $"{NL}" +
                       $"  SELECT{NL}" +
                       $"   [SomeColumn]{NL}" +
                       $"  FROM [TableB]{NL}";

        // ASSERT
        Assert.Equal(expected, sql);
        TestHelper.Assert.SerializationWorks(expected, sqlObj);
    }
}
