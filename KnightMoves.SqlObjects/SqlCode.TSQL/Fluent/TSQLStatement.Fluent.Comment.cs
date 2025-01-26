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
