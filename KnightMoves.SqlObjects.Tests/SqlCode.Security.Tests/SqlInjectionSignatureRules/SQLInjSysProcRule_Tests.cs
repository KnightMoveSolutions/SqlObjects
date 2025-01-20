using KnightMoves.SqlObjects.SqlCode.Security;
using Xunit;

namespace KnightMoves.SqlObjects.Tests.SqlCode.Security;

public class SQLInjSysProcRule_Tests
{
    [Fact]
    public void ContainsSqlInjection_Returns_True()
    {
        // ARRANGE
        var rule = new SQLInjSysProcRule();

        // ACTION
        var result1 = rule.ContainsSqlInjection("this has sp_danger stored proc call in it");
        var result2 = rule.ContainsSqlInjection("this has xsp_danger stored proc call in it");

        // ASSERT
        Assert.True(result1);
        Assert.True(result2);
    }

    [Fact]
    public void ContainsSqlInjection_Returns_False()
    {
        // ARRANGE
        var rule = new SQLInjSysProcRule();

        // ACTION
        var result1 = rule.ContainsSqlInjection("nothing to see here please move along");
        var result2 = rule.ContainsSqlInjection("do not confuse with spaghetti or spell or jack sparrow");


        // ASSERT
        Assert.False(result1);
        Assert.False(result2);
    }
}
