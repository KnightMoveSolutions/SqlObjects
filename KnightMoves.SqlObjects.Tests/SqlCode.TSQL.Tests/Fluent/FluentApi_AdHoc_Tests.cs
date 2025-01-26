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
using Xunit;

namespace KnightMoves.SqlObjects.Tests.TSql.Fluent;

public class FluentApi_AdHoc_Tests
{
    private readonly string NL = Environment.NewLine;

    [Fact]
    public void Script()
    {
        // ARRANGE 
        var sqlScript = $"DECLARE @myParam AS INT{NL}" +
                        $"  SET @myParam = 99{NL}" +
                        $"{NL}";

        var sqlObj =
            TSQL
              .Script(sqlScript)
              .SELECT()
                .STAR()
              .FROM("dbo", "Customers", "c")
              .WHERE("c", "ColumnA").IsEqualTo("@myParam")
              .Script($"{NL}  PRINT 'MESSAGE'");

        // ACTION
        var sql = sqlObj.Build();

        var expected = $"  DECLARE @myParam AS INT{NL}" +
                       $"  SET @myParam = 99{NL}" +
                       $"{NL}" +
                       $"  SELECT{NL}" +
                       $"   *{NL}" +
                       $"  FROM [dbo].[Customers] c{NL}" +
                       $"  WHERE 1=1{NL}" +
                       $"   AND [c].[ColumnA] = @myParam{NL}" +
                       $"   {NL}" +
                       $"  PRINT 'MESSAGE'";

        // ASSERT
        Assert.Equal(expected, sql);
        TestHelper.Assert.SerializationWorks(expected, sqlObj);
    }

    [Fact]
    public void Select_Literal_From_Table()
    {
        // ARRANGE 
        var sqlObj =
            TSQL
              .SELECT()
                .Literal("[LiteralColumn]")
              .FROM("Customers");

        // ACTION
        var sql = sqlObj.Build();

        var expected = $"  SELECT{NL}" +
                       $"   [LiteralColumn]{NL}" +
                       $"  FROM [Customers]{NL}";

        // ASSERT
        Assert.Equal(expected, sql);
        TestHelper.Assert.SerializationWorks(expected, sqlObj);
    }

    [Fact]
    public void LiteralSelect_From_Table()
    {
        // ARRANGE 
        var sqlObj =
            TSQL
              .Literal("SELECT *")
              .FROM("Customers");

        // ACTION
        var sql = sqlObj.Build();

        var expected = $"  SELECT *  FROM [Customers]{NL}";

        // ASSERT
        Assert.Equal(expected, sql);
        TestHelper.Assert.SerializationWorks(expected, sqlObj);
    }

    [Fact]
    public void Select_Star_Literal_From()
    {
        // ARRANGE 
        var sqlObj =
            TSQL
              .SELECT()
                .STAR()
                .Literal("GETDATE() AS [Today]")
              .FROM("Customers");

        // ACTION
        var sql = sqlObj.Build();

        var expected = $"  SELECT{NL}" +
                       $"   *,{NL}" +
                       $"   GETDATE() AS [Today]{NL}" +
                       $"  FROM [Customers]{NL}";

        // ASSERT
        Assert.Equal(expected, sql);
        TestHelper.Assert.SerializationWorks(expected, sqlObj);
    }

    [Fact]
    public void Select_Star_From_Where_dotLiteral()
    {
        // ARRANGE 
        var sqlObj =
            TSQL
              .SELECT()
                .STAR()
              .FROM("Customers")
              .WHERE()
                .Literal("Something = 'true'");

        // ACTION
        var sql = sqlObj.Build();

        var expected = $"  SELECT{NL}" +
                       $"   *{NL}" +
                       $"  FROM [Customers]{NL}" +
                       $"  WHERE 1=1{NL}" +
                       $"   AND Something = 'true'{NL}";

        // ASSERT
        Assert.Equal(expected, sql);
        TestHelper.Assert.SerializationWorks(expected, sqlObj);
    }
}
