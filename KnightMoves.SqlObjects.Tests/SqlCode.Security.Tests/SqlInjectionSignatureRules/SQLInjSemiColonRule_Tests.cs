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

using KnightMoves.SqlObjects.SqlCode.Security;
using Xunit;

namespace KnightMoves.SqlObjects.Tests.SqlCode.Security;

public class SQLInjSemiColonRule_Tests
{
    [Fact]
    public void ContainsSqlInjection_Returns_True()
    {
        // ARRANGE
        var rule = new SQLInjSemiColonRule();

        // ACTION
        var result = rule.ContainsSqlInjection("this has a ; in it");

        // ASSERT
        Assert.True(result);
    }

    [Fact]
    public void ContainsSqlInjection_Returns_False()
    {
        // ARRANGE
        var rule = new SQLInjSemiColonRule();

        // ACTION
        var result = rule.ContainsSqlInjection("nothing to see here please move along");

        // ASSERT
        Assert.False(result);
    }
}
