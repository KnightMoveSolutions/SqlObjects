﻿/*
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

public class FluentApi_ConditionScope_Tests
{
    private readonly string NL = Environment.NewLine;

    [Fact]
    public void Select_Star_From_Where_Column_And_OrGroup()
    {
        // ARRANGE
        var sqlObj =
            TSQL
              .SELECT()
                .STAR()
              .FROM("MyTable")
              .WHERE("t", "MyColumn").IsEqualTo(123).AND()
                .StartConditionScope()
                  .COLUMN("OtherColumn").IsEqualTo("X").OR()
                  .COLUMN("SomeColumn").IsEqualTo("Y").OR()
                  .COLUMN("t", "ThirdColumn").IsEqualTo("Z")
                .EndConditionScope();

        // ACTION
        var sql = sqlObj.Build();

        var expected = $"  SELECT{NL}" +
                       $"   *{NL}" +
                       $"  FROM [MyTable]{NL}" +
                       $"  WHERE 1=1{NL}" +
                       $"   AND [t].[MyColumn] = 123{NL}" +
                       $"   AND{NL}" +
                       $"   ({NL}" +
                       $"    [OtherColumn] = 'X' OR{NL}" +
                       $"    [SomeColumn] = 'Y' OR{NL}" +
                       $"    [t].[ThirdColumn] = 'Z'{NL}" +
                       $"   ){NL}";

        // ASSERT
        Assert.Equal(expected, sql);
        TestHelper.Assert.SerializationWorks(expected, sqlObj);
    }

    [Fact]
    public void Select_Star_From_Where_Column_Or_AndGroup()
    {
        // ARRANGE
        var sqlObj =
            TSQL
              .SELECT()
                .STAR()
              .FROM("MyTable")
              .WHERE("t", "MyColumn").IsEqualTo(123).OR()
                .StartConditionScope()
                  .COLUMN("OtherColumn").IsEqualTo("X").AND()
                  .COLUMN("t", "SomeColumn").IsEqualTo("Y").AND()
                  .COLUMN("ThirdColumn").IsEqualTo("Z")
                .EndConditionScope();

        // ACTION
        var sql = sqlObj.Build();

        var expected = $"  SELECT{NL}" +
                       $"   *{NL}" +
                       $"  FROM [MyTable]{NL}" +
                       $"  WHERE 1=1{NL}" +
                       $"   AND [t].[MyColumn] = 123{NL}" +
                       $"   OR{NL}" +
                       $"   ({NL}" +
                       $"    [OtherColumn] = 'X' AND{NL}" +
                       $"    [t].[SomeColumn] = 'Y' AND{NL}" +
                       $"    [ThirdColumn] = 'Z'{NL}" +
                       $"   ){NL}";

        // ASSERT
        Assert.Equal(expected, sql);
        TestHelper.Assert.SerializationWorks(expected, sqlObj);
    }
}
