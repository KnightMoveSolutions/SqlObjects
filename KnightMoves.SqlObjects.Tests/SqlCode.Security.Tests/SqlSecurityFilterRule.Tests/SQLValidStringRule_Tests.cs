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

using System.Collections.Generic;
using KnightMoves.SqlObjects.SqlCode.Security;
using Moq;
using Xunit;

namespace KnightMoves.SqlObjects.Tests.SqlCode.Security;

public class SQLValidStringRule_Tests
{
    [Fact]
    public void SanitizeInput_T_Executes_Overloaded_Method()
    {
        // ARRANGE
        var mockSqlInjectionChecker = new Mock<ISQLInjectionSignatureChecker>();

        var stringRule = new SQLValidStringRule(mockSqlInjectionChecker.Object);

        // ACTION
        var result = stringRule.SanitizeInput<string>("test");

        // ASSERT
        Assert.Equal("test", result);
    }

    [Fact]
    public void SanitizeInput_Escapes_SingleQuotes()
    {
        // ARRANGE
        var mockSqlInjectionChecker = new Mock<ISQLInjectionSignatureChecker>();

        var stringRule = new SQLValidStringRule(mockSqlInjectionChecker.Object);

        // ACTION
        var result = stringRule.SanitizeInput(typeof(string), "te'st");

        // ASSERT
        Assert.Equal("te''st", result);
    }

    [Fact]
    public void CheckInput_T_Executes_Overloaded_Method()
    {
        // ARRANGE
        var mockSqlInjectionChecker = new Mock<ISQLInjectionSignatureChecker>();

        var stringRule = new SQLValidStringRule(mockSqlInjectionChecker.Object);

        // ACTION
        var (result, errors) = stringRule.CheckInput<string>("test");

        // ASSERT
        Assert.True(result);
        Assert.Empty(errors);
    }

    [Fact]
    public void CheckInput_Returns_True()
    {
        // ARRANGE
        var mockSqlInjectionChecker = new Mock<ISQLInjectionSignatureChecker>();

        var stringRule = new SQLValidStringRule(mockSqlInjectionChecker.Object);

        // ACTION
        var (result, errors) = stringRule.CheckInput(typeof(string), "test");

        // ASSERT
        Assert.True(result);
        Assert.Empty(errors);
    }

    [Fact]
    public void CheckInput_Returns_False()
    {
        // ARRANGE
        var mockSqlInjectionChecker = new Mock<ISQLInjectionSignatureChecker>();

        mockSqlInjectionChecker
            .Setup(c => c.ContainsSqlInjection(It.IsAny<string>()))
            .Returns((true, new List<string> { "Fake violation notice for testing" }));

        var stringRule = new SQLValidStringRule(mockSqlInjectionChecker.Object);

        // ACTION
        var (result, errors) = stringRule.CheckInput(typeof(string), "te'st");

        // ASSERT
        Assert.False(result);
        Assert.NotEmpty(errors);
    }
}
