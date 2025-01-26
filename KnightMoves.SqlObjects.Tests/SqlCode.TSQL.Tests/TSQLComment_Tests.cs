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

using System;
using KnightMoves.SqlObjects.SqlCode.TSQL;
using Xunit;

namespace KnightMoves.SqlObjects.Tests.TSql;

public class TSQLComment_Tests
{
    private readonly string NL = Environment.NewLine;

    [Fact]
    public void SQL_Returns_Single_Line_Comment()
    {
        // ARRANGE
        var commentText = "This should be a single-line comment";

        var comment = new TSQLComment { CommentText = commentText };

        // ACTION
        var sql = comment.SQL();

        var expected = $" -- {commentText}{NL}";

        // ASSERT
        Assert.Equal(expected, sql);
        TestHelper.Assert.SerializationWorks(expected, comment);
    }

    [Fact]
    public void SQL_Returns_Single_Line_Comment_Trimmed()
    {
        // ARRANGE
        var commentText = $"   {'\t'}   This should be a single-line comment";

        var comment = new TSQLComment { CommentText = commentText };

        // ACTION
        var sql = comment.SQL();

        var expected = $" -- This should be a single-line comment{NL}";

        // ASSERT
        Assert.Equal(expected, sql);
        TestHelper.Assert.SerializationWorks(expected, comment);
    }

    [Fact]
    public void SQL_Returns_Multi_Line_Comment()
    {
        // ARRANGE
        var commentText = $"This should be a multi-line comment{NL}" + 
                          $"The second line of the comment is here{NL}" +
                          $"The third line of the comment is here";

        var comment = new TSQLComment { CommentText = commentText };

        // ACTION
        var sql = comment.SQL();

        var expected = $" /*{NL}" + 
                       $"  * This should be a multi-line comment{NL}" +
                       $"  * The second line of the comment is here{NL}" + 
                       $"  * The third line of the comment is here{NL}" + 
                       $" */{NL}";

        // ASSERT
        Assert.Equal(expected, sql);
        TestHelper.Assert.SerializationWorks(expected, comment);
    }


    [Fact]
    public void SQL_Returns_Multi_Line_Comment_Trimmed()
    {
        // ARRANGE
        var commentText = @"This should be a multi-line comment
                   " + "\t" + @"The second line of the comment is here
                                The third line of the comment is here";

        var comment = new TSQLComment { CommentText = commentText };

        // ACTION
        var sql = comment.SQL();

        var expected = $" /*{NL}" +
                       $"  * This should be a multi-line comment{NL}" +
                       $"  * The second line of the comment is here{NL}" +
                       $"  * The third line of the comment is here{NL}" +
                       $" */{NL}";

        // ASSERT
        Assert.Equal(expected, sql);
        TestHelper.Assert.SerializationWorks(expected, comment);
    }

    [Fact]
    public void SQL_Returns_Many_Single_Line_Comments()
    {
        // ARRANGE
        var commentText = $"This is the first line of the comment{NL}" +
                          $"The second line of the comment is here{NL}" +
                          $"The third line of the comment is here";

        var comment = new TSQLComment { CommentText = commentText, SingleLineCommentsOnly = true };

        // ACTION
        var sql = comment.SQL();

        var expected = $" -- This is the first line of the comment{NL}" +
                       $" -- The second line of the comment is here{NL}" +
                       $" -- The third line of the comment is here{NL}";

        // ASSERT
        Assert.Equal(expected, sql);
        TestHelper.Assert.SerializationWorks(expected, comment);
    }

    [Fact]
    public void SQL_Returns_Many_Single_Line_Comments_Trimmed()
    {
        // ARRANGE
        var commentText = @"This is the first line of the comment
                   " + "\t" + @"The second line of the comment is here
                                The third line of the comment is here";

        var comment = new TSQLComment { CommentText = commentText, SingleLineCommentsOnly = true };

        // ACTION
        var sql = comment.SQL();

        var expected = $" -- This is the first line of the comment{NL}" +
                       $" -- The second line of the comment is here{NL}" +
                       $" -- The third line of the comment is here{NL}";

        // ASSERT
        Assert.Equal(expected, sql);
        TestHelper.Assert.SerializationWorks(expected, comment);
    }
}
