using KnightMoves.SqlObjects.SqlCode.Security;
using Xunit;

namespace KnightMoves.SqlObjects.Tests.SqlCode.Security;

public class SQLInjCommentRule_Tests
{
    [Fact]
    public void ContainsSqlInjection_Returns_True()
    {
        // ARRANGE
        var rule = new SQLInjCommentRule();

        // ACTION
        var result1 = rule.ContainsSqlInjection("this has a -- comment in it");
        var result2 = rule.ContainsSqlInjection("this has a /* comment start in it");
        var result3 = rule.ContainsSqlInjection("this has a */ comment end in it");

        // ASSERT
        Assert.True(result1);
        Assert.True(result2);
        Assert.True(result3);
    }

    [Fact]
    public void ContainsSqlInjection_Returns_False()
    {
        // ARRANGE
        var rule = new SQLInjCommentRule();

        // ACTION
        var result = rule.ContainsSqlInjection("nothing to see here please move along");

        // ASSERT
        Assert.False(result);
    }
}
