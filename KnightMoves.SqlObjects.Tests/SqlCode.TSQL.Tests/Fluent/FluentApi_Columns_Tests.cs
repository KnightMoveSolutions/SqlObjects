using System;
using System.Collections.Generic;
using Xunit;
using KnightMoves.SqlObjects.SqlCode.TSQL;

namespace KnightMoves.SqlObjects.Tests.TSql.Fluent;

public class FluentApi_Columns_Tests
{
    private readonly string NL = Environment.NewLine;

    [Fact]
    public void Select_ColumnObjects_From()
    {
        // ARRANGE
        var columns = new List<TSQLColumn>
        {
            new TSQLColumn { MultiPartIdentifier = "t", ColumnName = "ColumnA", Alias = "A" },
            new TSQLColumn { MultiPartIdentifier = "t", ColumnName = "ColumnB", Alias = "B" },
            new TSQLColumn { MultiPartIdentifier = "t", ColumnName = "ColumnC", Alias = "C" }
        };

        var sqlObj =
            TSQL
              .SELECT()
                .COLUMNS(columns)
              .FROM("dbo", "MyTable", "t");

        // ACTION
        var sql = sqlObj.Build();

        var expected = $"  SELECT{NL}" +
                       $"   [t].[ColumnA] AS [A],{NL}" +
                       $"   [t].[ColumnB] AS [B],{NL}" +
                       $"   [t].[ColumnC] AS [C]{NL}" +
                       $"  FROM [dbo].[MyTable] t{NL}";

        Assert.Equal(expected, sql);
        TestHelper.Assert.SerializationWorks(expected, sqlObj);
    }

    [Fact]
    public void Select_Columns()
    {
        // ARRANGE
        var sqlObj =
            TSQL
              .SELECT()
                .COLUMN("Id").AS("UserId")
                .COLUMN("Name");

        // ACTION
        var sql = sqlObj.Build();

        var expected = $"  SELECT{NL}" +
                       $"   [Id] AS [UserId],{NL}" +
                       $"   [Name]{NL}";

        // ASSERT
        Assert.Equal(expected, sql);
        TestHelper.Assert.SerializationWorks(expected, sqlObj);
    }

}
