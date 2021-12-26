using KnightMoves.SqlObjects.SqlCode.Security;
using Xunit;

namespace KnightMoves.SqlObjects.Tests.SqlCode.Security
{
    public class SQLValidBooleanRule_Tests
    {
        [Fact]
        public void SanitizeInput_T_Executes_Overloaded_Method()
        {
            // ARRANGE
            var booleanRule = new SQLValidBooleanRule();

            // ACTION
            var result = booleanRule.SanitizeInput<bool>("true");

            // ASSERT
            Assert.Equal("1", result);
        }

        [Fact]
        public void SanitizeInput_Returns_Zero_OnInvalid_Boolean()
        {
            // ARRANGE
            var booleanRule = new SQLValidBooleanRule();

            // ACTION
            var result = booleanRule.SanitizeInput<bool>("X");

            // ASSERT
            Assert.Equal("0", result);
        }

        [Fact]
        public void SanitizeInput_Returns_True_For_True()
        {
            // ARRANGE
            var booleanRule = new SQLValidBooleanRule();

            // ACTION
            var result = booleanRule.SanitizeInput<bool>("True");

            // ASSERT
            Assert.Equal("1", result);
        }

        [Fact]
        public void SanitizeInput_Returns_True_For_One()
        {
            // ARRANGE
            var booleanRule = new SQLValidBooleanRule();

            // ACTION
            var result = booleanRule.SanitizeInput<bool>("1");

            // ASSERT
            Assert.Equal("1", result);
        }

        [Fact]
        public void SanitizeInput_Returns_False_For_False()
        {
            // ARRANGE
            var booleanRule = new SQLValidBooleanRule();

            // ACTION
            var result = booleanRule.SanitizeInput<bool>("false");

            // ASSERT
            Assert.Equal("0", result);
        }

        [Fact]
        public void SanitizeInput_Returns_False_For_Zero()
        {
            // ARRANGE
            var booleanRule = new SQLValidBooleanRule();

            // ACTION
            var result = booleanRule.SanitizeInput<bool>("0");

            // ASSERT
            Assert.Equal("0", result);
        }

        [Fact]
        public void CheckInput_T_Executes_Overloaded_Method()
        {
            // ARRANGE
            var booleanRule = new SQLValidBooleanRule();

            // ACTION
            var (result, errors) = booleanRule.CheckInput<bool>("true");

            // ASSERT
            Assert.True(result);
            Assert.Empty(errors);
        }

        [Fact]
        public void CheckInput_Returns_True()
        {
            // ARRANGE
            var booleanRule = new SQLValidBooleanRule();

            // ACTION
            var (result, errors) = booleanRule.CheckInput(typeof(bool), "true");

            // ASSERT
            Assert.True(result);
            Assert.Empty(errors);
        }

        [Fact]
        public void CheckInput_Returns_False()
        {
            // ARRANGE
            var booleanRule = new SQLValidBooleanRule();

            // ACTION
            var (result, errors) = booleanRule.CheckInput(typeof(bool), "X");

            // ASSERT
            Assert.False(result);
            Assert.NotEmpty(errors);
        }
    }
}
