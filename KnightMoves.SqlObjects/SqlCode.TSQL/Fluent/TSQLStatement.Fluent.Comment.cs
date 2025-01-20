using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KnightMoves.SqlObjects.SqlCode.TSQL;

public abstract partial class TSQLStatement : SqlStatement
{
    /// <summary>
    /// Creates a SQL Comment using the "--" comment prefix. If <paramref name="commentText"/> contains any 
    /// <see cref="Environment.NewLine"/>s then it will create a multi-line comment using /* ... */. If 
    /// <paramref name="singleLineOnly"/> is set to true then it will split the comments on the new lines 
    /// using the "--" comment prefix for each line.
    /// </summary>
    public override SqlStatement Comment(string commentText, bool singleLineOnly = false)
    {
        AddComment(new TSQLComment { CommentText = commentText, SingleLineCommentsOnly = singleLineOnly });
        return this;
    }

    /// <summary>
    /// Creates a SQL multi-line comment using the /* ... */ comment block syntax where each string in the 
    /// collection will be on its own line. If <paramref name="singleLineOnly"/> is set to true then it will 
    /// write each string in the collection on its own line using the "--" comment prefix.
    /// </summary>
    public override SqlStatement Comment(IEnumerable<string> commentText, bool singleLineOnly = false)
    {
        var sb = new StringBuilder();

        commentText.ToList().ForEach(commentText =>
        {
            sb.Append(
                commentText.Trim(Environment.NewLine.ToCharArray())
                           .Trim() + Environment.NewLine
            );
        });

        sb.Remove(sb.Length - 2, 2);

        AddComment(new TSQLComment { CommentText = sb.ToString(), SingleLineCommentsOnly = singleLineOnly });

        return this;
    }

    private void AddComment(SqlStatement sqlComment)
    {
        var parent = GetExpressionParent();

        if (parent != null)
        {
            parent.Children.Add(sqlComment);
            return;
        }

        parent = GetParentScope();

        if (parent != null)
        {
            parent.Children.Add(sqlComment);
            return;
        }

        if (Children.Any())
            Children.Add(sqlComment);
    }
}
