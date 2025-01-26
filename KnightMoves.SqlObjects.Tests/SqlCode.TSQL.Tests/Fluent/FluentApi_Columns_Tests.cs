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
