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
using KnightMoves.SqlObjects.SqlCode.Security;
using Xunit;

namespace KnightMoves.SqlObjects.Tests.SqlCode.Security;

public class SQLValidGuidRule_Tests
{
    [Fact]
    public void SanitizeInput_T_Executes_Overloaded_Method()
    {
        // ARRANGE
        var guid = Guid.NewGuid().ToString();

        var guidRule = new SQLValidGuidRule();

        // ACTION
        var result = guidRule.SanitizeInput<Guid>(guid);

        // ASSERT
        Assert.Equal(guid, result);
    }

    [Fact]
    public void SanitizeInput_Returns_Guid_OnValidInput()
    {
        // ARRANGE
        var guid = Guid.NewGuid().ToString();

        var guidRule = new SQLValidGuidRule();

        // ACTION
        var result = guidRule.SanitizeInput(typeof(Guid), guid);

        // ASSERT
        Assert.Equal(guid, result);
    }

    [Fact]
    public void SanitizeInput_Returns_EmptyGuid_OnBadInput()
    {
        // ARRANGE
        var guidRule = new SQLValidGuidRule();

        // ACTION
        var result = guidRule.SanitizeInput(typeof(Guid), "X");

        // ASSERT
        Assert.Equal(Guid.Empty.ToString(), result);
    }

    [Fact]
    public void CheckInput_T_Executes_Overloaded_Method()
    {
        // ARRANGE
        var guid = Guid.NewGuid().ToString();

        var guidRule = new SQLValidGuidRule();

        // ACTION
        var (result, errors) = guidRule.CheckInput<Guid>(guid);

        // ASSERT
        Assert.True(result);
        Assert.Empty(errors);
    }

    [Fact]
    public void CheckInput_Returns_True()
    {
        // ARRANGE
        var guid = Guid.NewGuid().ToString();

        var guidRule = new SQLValidGuidRule();

        // ACTION
        var (result, errors) = guidRule.CheckInput(typeof(Guid), guid);
        Assert.Empty(errors);

        // ASSERT
        Assert.True(result);
        Assert.Empty(errors);
    }

    [Fact]
    public void CheckInput_Returns_False()
    {
        // ARRANGE
        var guidRule = new SQLValidGuidRule();

        // ACTION
        var (result, errors) = guidRule.CheckInput(typeof(Guid), "X");

        // ASSERT
        Assert.False(result);
        Assert.NotEmpty(errors);
    }
}
