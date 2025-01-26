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
using Xunit;

namespace KnightMoves.SqlObjects.Tests.SqlCode.Security;

public class SQLValidNumericRule_Tests
{
    [Fact]
    public void SanitizeInput_T_Executes_Overloaded_Method()
    {
        // ARRANGE
        var numericRule = new SQLValidNumericRule();

        // ACTION
        var result = numericRule.SanitizeInput<int>("1");

        // ASSERT
        Assert.Equal("1", result);
    }

    [Fact]
    public void SanitizeInput_Returns_Zero_OnInvalid_Byte()
    {
        // ARRANGE
        var numericRule = new SQLValidNumericRule();

        // ACTION
        var result = numericRule.SanitizeInput(typeof(byte), "NaN");

        // ASSERT
        Assert.Equal("0", result);
    }

    [Fact]
    public void SanitizeInput_Returns_Zero_OnInvalid_Short()
    {
        // ARRANGE
        var numericRule = new SQLValidNumericRule();

        // ACTION
        var result = numericRule.SanitizeInput(typeof(short), "NaN");

        // ASSERT
        Assert.Equal("0", result);
    }

    [Fact]
    public void SanitizeInput_Returns_Zero_OnInvalid_Int()
    {
        // ARRANGE
        var numericRule = new SQLValidNumericRule();

        // ACTION
        var result = numericRule.SanitizeInput(typeof(int), "NaN");

        // ASSERT
        Assert.Equal("0", result);
    }

    [Fact]
    public void SanitizeInput_Returns_Zero_OnInvalid_Long()
    {
        // ARRANGE
        var numericRule = new SQLValidNumericRule();

        // ACTION
        var result = numericRule.SanitizeInput(typeof(long), "NaN");

        // ASSERT
        Assert.Equal("0", result);
    }

    [Fact]
    public void SanitizeInput_Returns_Zero_OnInvalid_Decimal()
    {
        // ARRANGE
        var numericRule = new SQLValidNumericRule();

        // ACTION
        var result = numericRule.SanitizeInput(typeof(decimal), "NaN");

        // ASSERT
        Assert.Equal("0", result);
    }

    [Fact]
    public void SanitizeInput_Returns_Zero_OnInvalid_Float()
    {
        // ARRANGE
        var numericRule = new SQLValidNumericRule();

        // ACTION
        var result = numericRule.SanitizeInput(typeof(float), "X");

        // ASSERT
        Assert.Equal("0", result);
    }

    [Fact]
    public void SanitizeInput_Returns_Zero_OnInvalid_Double()
    {
        // ARRANGE
        var numericRule = new SQLValidNumericRule();

        // ACTION
        var result = numericRule.SanitizeInput(typeof(double), "X");

        // ASSERT
        Assert.Equal("0", result);
    }

    [Fact]
    public void CheckInput_T_Executes_Overloaded_Method()
    {
        // ARRANGE
        var numericRule = new SQLValidNumericRule();

        // ACTION
        var (result, errors) = numericRule.CheckInput<int>("1");

        // ASSERT
        Assert.True(result);
        Assert.Empty(errors);
    }

    [Fact]
    public void CheckInput_Returns_True()
    {
        // ARRANGE
        var numericRule = new SQLValidNumericRule();

        // ACTION
        var (result, errors) = numericRule.CheckInput(typeof(int), "1");

        // ASSERT
        Assert.True(result);
        Assert.Empty(errors);
    }

    [Fact]
    public void CheckInput_Returns_False()
    {
        // ARRANGE
        var numericRule = new SQLValidNumericRule();

        // ACTION
        var (result, errors) = numericRule.CheckInput(typeof(int), "NaN");

        // ASSERT
        Assert.False(result);
        Assert.NotEmpty(errors);
    }
}
