using KnightMoves.SqlObjects.SqlCode.Security;
using Xunit;

namespace KnightMoves.SqlObjects.Tests.SqlCode.Security
{
    public class SQLInjSelectRule_Tests
    {
        [Fact]
        public void ContainsSqlInjection_Returns_True()
        {
            // ARRANGE
            var rule = new SQLInjSelectRule();

            // ACTION
            var result = rule.ContainsSqlInjection("this has the SELECT keyword");

            // ASSERT
            Assert.True(result);
        }

        [Fact]
        public void ContainsSqlInjection_Returns_False()
        {
            // ARRANGE
            var rule = new SQLInjSelectRule();

            // ACTION
            var result = rule.ContainsSqlInjection("nothing to see here please move along");

            // ASSERT
            Assert.False(result);
        }
    }
}
