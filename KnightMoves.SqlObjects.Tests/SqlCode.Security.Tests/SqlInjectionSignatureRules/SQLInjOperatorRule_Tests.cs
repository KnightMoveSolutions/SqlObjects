using KnightMoves.SqlObjects.SqlCode.Security;
using Xunit;

namespace KnightMoves.SqlObjects.Tests.SqlCode.Security;

public class SQLInjOperatorRule_Tests
{
    [Fact]
    public void ContainsSqlInjection_Returns_True()
    {
        // ARRANGE
        var rule = new SQLInjOperatorRule();

        // ACTION
        var result1 = rule.ContainsSqlInjection("this has the = operator");
        var result2 = rule.ContainsSqlInjection("this has the < operator");
        var result3 = rule.ContainsSqlInjection("this has the > operator");

        // ASSERT
        Assert.True(result1);
        Assert.True(result2);
        Assert.True(result3);
    }

    [Fact]
    public void ContainsSqlInjection_Returns_False()
    {
        // ARRANGE
        var rule = new SQLInjOperatorRule();

        // ACTION
        var result = rule.ContainsSqlInjection("nothing to see here please move along");

        // ASSERT
        Assert.False(result);
    }
}
