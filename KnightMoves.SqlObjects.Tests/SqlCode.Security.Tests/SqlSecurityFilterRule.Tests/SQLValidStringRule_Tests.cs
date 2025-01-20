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
