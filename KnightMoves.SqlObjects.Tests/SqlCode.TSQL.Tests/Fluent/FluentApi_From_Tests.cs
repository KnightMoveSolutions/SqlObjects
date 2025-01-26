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
