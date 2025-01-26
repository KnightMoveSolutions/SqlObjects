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

using System;
using System.Collections.Generic;
using KnightMoves.SqlObjects.SqlCode;
using KnightMoves.SqlObjects.SqlCode.TSQL;
using Xunit;

namespace KnightMoves.SqlObjects.Tests.TSql.Fluent;

public class FluentApi_GroupBy_Tests
{
    private readonly string NL = Environment.NewLine;

    [Fact]
    public void Select_From_GroupBy_PullsFromSelectList()
    {
        // ARRANGE
        var sqlObj =
            TSQL
              .SELECT()
                .COUNT("*").AS("Total")
                .COLUMN("ColumnA")
                .COLUMN("ColumnB")
                .Calculate("ColumnC").Plus("ColumnD").AS("CalcResult")
              .FROM("Customers")
              .GROUPBY();

        // ACTION
        var sql = sqlObj.Build();

        var expected = $"  SELECT{NL}" +
                       $"   COUNT(*) AS [Total],{NL}" +
                       $"   [ColumnA],{NL}" +
                       $"   [ColumnB],{NL}" +
                       $"   ([ColumnC] + [ColumnD]) AS [CalcResult]{NL}" +
                       $"  FROM [Customers]{NL}" +
                       $"  GROUP BY{NL}" + 
                       $"   [ColumnA],{NL}" + 
                       $"   [ColumnB],{NL}" + 
                       $"   ([ColumnC] + [ColumnD]){NL}";

        // ASSERT
        Assert.Equal(expected, sql);
        TestHelper.Assert.SerializationWorks(expected, sqlObj);
    }

    [Fact]
    public void Select_From_GroupBy_Columns()
    {
        // ARRANGE
        var columns = new List<ISqlColumn>
        {
            new TSQLColumn { ColumnName = "ColumnA" },
            new TSQLColumn { ColumnName = "ColumnB" }
        };

        var sqlObj =
            TSQL
              .SELECT()
                .COUNT("*").AS("Total")
                .COLUMN("ColumnA")
                .COLUMN("ColumnB")
              .FROM("Customers")
              .GROUPBY(columns);

        // ACTION
        var sql = sqlObj.Build();

        var expected = $"  SELECT{NL}" +
                       $"   COUNT(*) AS [Total],{NL}" +
                       $"   [ColumnA],{NL}" +
                       $"   [ColumnB]{NL}" +
                       $"  FROM [Customers]{NL}" +
                       $"  GROUP BY{NL}" +
                       $"   [ColumnA],{NL}" +
                       $"   [ColumnB]{NL}";

        // ASSERT
        Assert.Equal(expected, sql);
        TestHelper.Assert.SerializationWorks(expected, sqlObj);
    }

    [Fact]
    public void Select_From_GroupBy_QueryExpressions()
    {
        // ARRANGE
        var columns = new List<ISqlQueryExpression>
        {
            new TSQLColumn { ColumnName = "ColumnA" },
            new TSQLColumn { ColumnName = "ColumnB" }
        };

        var sqlObj =
            TSQL
              .SELECT()
                .COUNT("*").AS("Total")
                .COLUMN("ColumnA")
                .COLUMN("ColumnB")
              .FROM("Customers")
              .GROUPBY(columns);

        // ACTION
        var sql = sqlObj.Build();

        var expected = $"  SELECT{NL}" +
                       $"   COUNT(*) AS [Total],{NL}" +
                       $"   [ColumnA],{NL}" +
                       $"   [ColumnB]{NL}" +
                       $"  FROM [Customers]{NL}" +
                       $"  GROUP BY{NL}" +
                       $"   [ColumnA],{NL}" +
                       $"   [ColumnB]{NL}";

        // ASSERT
        Assert.Equal(expected, sql);
        TestHelper.Assert.SerializationWorks(expected, sqlObj);
    }

    [Fact]
    public void Select_From_GroupBy_FluentItems()
    {
        // ARRANGE
        var sqlObj =
            TSQL
              .SELECT()
                .COUNT("*").AS("Total")
                .COLUMN("ColumnA")
                .COLUMN("ColumnB")
              .FROM("Customers")
              .GROUPBY()
                .COLUMN("ColumnA")
                .COLUMN("ColumnB");

        // ACTION
        var sql = sqlObj.Build();

        var expected = $"  SELECT{NL}" +
                       $"   COUNT(*) AS [Total],{NL}" +
                       $"   [ColumnA],{NL}" +
                       $"   [ColumnB]{NL}" +
                       $"  FROM [Customers]{NL}" +
                       $"  GROUP BY{NL}" +
                       $"   [ColumnA],{NL}" +
                       $"   [ColumnB]{NL}";

        // ASSERT
        Assert.Equal(expected, sql);
        TestHelper.Assert.SerializationWorks(expected, sqlObj);
    }
}
