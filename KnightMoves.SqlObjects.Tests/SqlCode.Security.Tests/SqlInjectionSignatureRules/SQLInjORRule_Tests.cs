using KnightMoves.SqlObjects.SqlCode.Security;
using Xunit;

namespace KnightMoves.SqlObjects.Tests.SqlCode.Security
{
    public class SQLInjORRule_Tests
    {
        [Fact]
        public void ContainsSqlInjection_Returns_True()
        {
            // ARRANGE
            var rule = new SQLInjORRule();

            // ACTION
            var result1 = rule.ContainsSqlInjection("This has OR in it");
            var result2 = rule.ContainsSqlInjection("This ends in the keyword Or");
            var result3 = rule.ContainsSqlInjection("oR is at the start of this one");

            // ASSERT
            Assert.True(result1);
            Assert.True(result2);
            Assert.True(result3);
        }

        [Fact]
        public void ContainsSqlInjection_Returns_False()
        {
            // ARRANGE
            var rule = new SQLInjORRule();

            // ACTION
            var result1 = rule.ContainsSqlInjection("nothing to see here please move along");
            var result2 = rule.ContainsSqlInjection("do not confuse it with order border and odor okay");

            // ASSERT
            Assert.False(result1);
            Assert.False(result2);
        }
    }
}
