using System;
using KnightMoves.SqlObjects.SqlCode.TSQL;
using Xunit;

namespace KnightMoves.SqlObjects.Tests.TSql
{
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
}
