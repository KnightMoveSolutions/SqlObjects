using KnightMoves.SqlObjects.SqlCode;
using Xunit;

namespace KnightMoves.SqlObjects.Tests.SqlCode.Tests.Extensions
{
    public class StringExtensions_Tests
    {
        [Fact]
        public void IsParameter_Returns_False()
        {
            string nullString = null;
            string emptyString = string.Empty;

            Assert.False(nullString.IsParameter());
            Assert.False(emptyString.IsParameter());
            Assert.False("SomeString".IsParameter());
        }

        [Fact]
        public void IsParameter_Returns_True()
        {
            Assert.True("@someParam".IsParameter());
        }

        [Fact]
        public void IsDelimited_Returns_True_on_BasicFormat()
        {
            Assert.True("[SomeColumn]".IsDelimited());
        }

        [Fact]
        public void IsDelimited_Returns_True_on_Escaped_EndBracket()
        {
            Assert.True("[Some]]Column]".IsDelimited());
        }

        [Fact]
        public void IsDelimited_Returns_False_on_NullOrEmpty()
        {
            string nullString = null;
            Assert.False(nullString.IsDelimited());
            Assert.False(string.Empty.IsDelimited());
        }

        [Fact]
        public void IsDelimited_Returns_False_on_Missing_StartOrEnd_Brackets()
        {
            Assert.False("SomeColumn]".IsDelimited());
            Assert.False("[SomeColumn".IsDelimited());
        }

        [Fact]
        public void IsDelimited_Returns_False_on_Unescaped_EndBracket()
        {
            Assert.False("[Some]Column]".IsDelimited());
        }

        [Fact]
        public void SanitizeDelimitedValue_Returns_Null_for_NullString()
        {
            string nullString = null;
            Assert.Null(nullString.SanitizeDelimitedValue());
        }

        [Fact]
        public void SanitizeDelimitedValue_Escapes_Unterminated_Bracket_in_DelimitedString()
        {
            Assert.Equal("[Some]]Column]", "[Some]Column]".SanitizeDelimitedValue());
        }

        [Fact]
        public void SanitizeDelimitedValue_Escapes_Unterminated_Bracket_in_RegularString()
        {
            Assert.Equal("Some]]Column", "Some]Column".SanitizeDelimitedValue());
        }
    }
}
