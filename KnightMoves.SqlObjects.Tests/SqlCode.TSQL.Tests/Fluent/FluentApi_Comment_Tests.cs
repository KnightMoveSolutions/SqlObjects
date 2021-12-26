using System;
using System.Collections.Generic;
using Xunit;

namespace KnightMoves.SqlObjects.Tests.TSql.Fluent
{
    public class FluentApi_Comment_Tests
    {
        private readonly string NL = Environment.NewLine;

        [Fact]
        public void SingleLine_Comment_Select_Column_From()
        {
            // ARRANGE
            var sqlObj =
                TSQL
                  .Comment("Here is my single-line comment")
                  .SELECT()
                    .COLUMN("FirstName")
                  .FROM("Customers");

            // ACTION
            var sql = sqlObj.Build();

            var expected = $"  -- Here is my single-line comment{NL}" +
                           $"  SELECT{NL}" +
                           $"   [FirstName]{NL}" +
                           $"  FROM [Customers]{NL}";

            Assert.Equal(expected, sql);
            TestHelper.Assert.SerializationWorks(expected, sqlObj);
        }

        [Fact]
        public void MultiLine_Comment_Select_Column_From()
        {
            // ARRANGE
            var sqlObj =
                TSQL
                  .Comment
                  (
                    @"Here is my single-line comment
                      With some more commentary and 
                      blah blah blah watch this thing 
                      line up my multi-line comment nicely"
                  )
                  .SELECT()
                    .COLUMN("FirstName")
                  .FROM("Customers");

            // ACTION
            var sql = sqlObj.Build();

            var expected = $"  /*{NL}" +
                           $"   * Here is my single-line comment{NL}" +
                           $"   * With some more commentary and{NL}" +
                           $"   * blah blah blah watch this thing{NL}" +
                           $"   * line up my multi-line comment nicely{NL}" +
                           $"  */{NL}" +
                           $"  SELECT{NL}" +
                           $"   [FirstName]{NL}" +
                           $"  FROM [Customers]{NL}";

            Assert.Equal(expected, sql);
            TestHelper.Assert.SerializationWorks(expected, sqlObj);
        }

        [Fact]
        public void Many_SingleLine_Comment_Select_Column_From()
        {
            // ARRANGE
            var sqlObj =
                TSQL
                  .Comment
                  (
                    commentText:
                    @"Here is my single-line comment
                      With some more commentary and 
                      blah blah blah watch this thing 
                      line up my multi-line comment nicely",
                    singleLineOnly: 
                    true
                  )
                  .SELECT()
                    .COLUMN("FirstName")
                  .FROM("Customers");

            // ACTION
            var sql = sqlObj.Build();

            var expected = $"  -- Here is my single-line comment{NL}" +
                           $"  -- With some more commentary and{NL}" +
                           $"  -- blah blah blah watch this thing{NL}" +
                           $"  -- line up my multi-line comment nicely{NL}" +
                           $"  SELECT{NL}" +
                           $"   [FirstName]{NL}" +
                           $"  FROM [Customers]{NL}";

            Assert.Equal(expected, sql);
            TestHelper.Assert.SerializationWorks(expected, sqlObj);
        }

        [Fact]
        public void CommentCollection_Select_Column_From()
        {
            // ARRANGE
            var commentText = new List<string>
            {
                "Here is my single-line comment",
                "With some more commentary and",
                "blah blah blah watch this thing",
                "line up my multi-line comment nicely"
            };

            var sqlObj =
                TSQL
                  .Comment(commentText)
                  .SELECT()
                    .COLUMN("FirstName")
                  .FROM("Customers");

            // ACTION
            var sql = sqlObj.Build();

            var expected = $"  /*{NL}" +
                           $"   * Here is my single-line comment{NL}" +
                           $"   * With some more commentary and{NL}" +
                           $"   * blah blah blah watch this thing{NL}" +
                           $"   * line up my multi-line comment nicely{NL}" +
                           $"  */{NL}" +
                           $"  SELECT{NL}" +
                           $"   [FirstName]{NL}" +
                           $"  FROM [Customers]{NL}";

            Assert.Equal(expected, sql);
            TestHelper.Assert.SerializationWorks(expected, sqlObj);
        }

        [Fact]
        public void Collection_to_ManySingleLine_Comment_Select_Column_From()
        {
            // ARRANGE
            var commentText = new List<string>
            {
                "Here is my single-line comment",
                "With some more commentary and",
                "blah blah blah watch this thing",
                "line up my multi-line comment nicely"
            };

            var sqlObj =
                TSQL
                  .Comment(commentText, true)
                  .SELECT()
                    .COLUMN("FirstName")
                  .FROM("Customers");

            // ACTION
            var sql = sqlObj.Build();

            var expected = $"  -- Here is my single-line comment{NL}" +
                           $"  -- With some more commentary and{NL}" +
                           $"  -- blah blah blah watch this thing{NL}" +
                           $"  -- line up my multi-line comment nicely{NL}" +
                           $"  SELECT{NL}" +
                           $"   [FirstName]{NL}" +
                           $"  FROM [Customers]{NL}";

            Assert.Equal(expected, sql);
            TestHelper.Assert.SerializationWorks(expected, sqlObj);
        }

        [Fact]
        public void Select_SingleLine_Comment_Column_From()
        {
            // ARRANGE
            var sqlObj =
                TSQL
                  .SELECT()
                    .Comment("Here is my single-line comment")
                    .COLUMN("FirstName")
                  .FROM("Customers");

            // ACTION
            var sql = sqlObj.Build();

            var expected = $"  SELECT{NL}" +
                           $"   -- Here is my single-line comment{NL}" +
                           $"   [FirstName]{NL}" +
                           $"  FROM [Customers]{NL}";

            Assert.Equal(expected, sql);
            TestHelper.Assert.SerializationWorks(expected, sqlObj);
        }

        [Fact]
        public void Select_MultiLine_Comment_Column_From()
        {
            // ARRANGE
            var sqlObj =
                TSQL
                  .SELECT()
                  .Comment
                  (
                    @"Here is my single-line comment
                      With some more commentary and 
                      blah blah blah watch this thing 
                      line up my multi-line comment nicely"
                  )
                    .COLUMN("FirstName")
                  .FROM("Customers");

            // ACTION
            var sql = sqlObj.Build();

            var expected = $"  SELECT{NL}" +
                           $"   /*{NL}" +
                           $"    * Here is my single-line comment{NL}" +
                           $"    * With some more commentary and{NL}" +
                           $"    * blah blah blah watch this thing{NL}" +
                           $"    * line up my multi-line comment nicely{NL}" +
                           $"   */{NL}" +
                           $"   [FirstName]{NL}" +
                           $"  FROM [Customers]{NL}";

            Assert.Equal(expected, sql);
            TestHelper.Assert.SerializationWorks(expected, sqlObj);
        }

        [Fact]
        public void Select_Many_SingleLine_Comment_Column_From()
        {
            // ARRANGE
            var sqlObj =
                TSQL
                  .SELECT()
                  .Comment
                  (
                    commentText:
                    @"Here is my single-line comment
                      With some more commentary and 
                      blah blah blah watch this thing 
                      line up my multi-line comment nicely",
                    singleLineOnly:
                    true
                  )
                    .COLUMN("FirstName")
                  .FROM("Customers");

            // ACTION
            var sql = sqlObj.Build();

            var expected = $"  SELECT{NL}" +
                           $"   -- Here is my single-line comment{NL}" +
                           $"   -- With some more commentary and{NL}" +
                           $"   -- blah blah blah watch this thing{NL}" +
                           $"   -- line up my multi-line comment nicely{NL}" +
                           $"   [FirstName]{NL}" +
                           $"  FROM [Customers]{NL}";

            Assert.Equal(expected, sql);
            TestHelper.Assert.SerializationWorks(expected, sqlObj);
        }

        [Fact]
        public void Select_CommentCollection_Column_From()
        {
            // ARRANGE
            var commentText = new List<string>
            {
                "Here is my single-line comment",
                "With some more commentary and",
                "blah blah blah watch this thing",
                "line up my multi-line comment nicely"
            };

            var sqlObj =
                TSQL
                  .SELECT()
                  .Comment(commentText)
                    .COLUMN("FirstName")
                  .FROM("Customers");

            // ACTION
            var sql = sqlObj.Build();

            var expected = $"  SELECT{NL}" +
                           $"   /*{NL}" +
                           $"    * Here is my single-line comment{NL}" +
                           $"    * With some more commentary and{NL}" +
                           $"    * blah blah blah watch this thing{NL}" +
                           $"    * line up my multi-line comment nicely{NL}" +
                           $"   */{NL}" +
                           $"   [FirstName]{NL}" +
                           $"  FROM [Customers]{NL}";

            Assert.Equal(expected, sql);
            TestHelper.Assert.SerializationWorks(expected, sqlObj);
        }

        [Fact]
        public void Select_Collection_to_ManySingleLine_Comment_Column_From()
        {
            // ARRANGE
            var commentText = new List<string>
            {
                "Here is my single-line comment",
                "With some more commentary and",
                "blah blah blah watch this thing",
                "line up my multi-line comment nicely"
            };

            var sqlObj =
                TSQL
                  .SELECT()
                  .Comment(commentText, true)
                    .COLUMN("FirstName")
                  .FROM("Customers");

            // ACTION
            var sql = sqlObj.Build();

            var expected = $"  SELECT{NL}" +
                           $"   -- Here is my single-line comment{NL}" +
                           $"   -- With some more commentary and{NL}" +
                           $"   -- blah blah blah watch this thing{NL}" +
                           $"   -- line up my multi-line comment nicely{NL}" +
                           $"   [FirstName]{NL}" +
                           $"  FROM [Customers]{NL}";

            Assert.Equal(expected, sql);
            TestHelper.Assert.SerializationWorks(expected, sqlObj);
        }
    }
}
