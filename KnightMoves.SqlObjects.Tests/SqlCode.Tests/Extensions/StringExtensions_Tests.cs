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

using KnightMoves.SqlObjects.SqlCode;
using Xunit;

namespace KnightMoves.SqlObjects.Tests.SqlCode.Tests.Extensions;

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
