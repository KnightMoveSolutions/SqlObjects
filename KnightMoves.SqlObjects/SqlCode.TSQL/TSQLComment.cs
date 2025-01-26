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
using System.Linq;
using System.Text;

namespace KnightMoves.SqlObjects.SqlCode.TSQL;

/// <summary>
/// This class implements <see cref="ISqlComment"/> and builds comment blocks.
/// </summary>
public class TSQLComment : TSQLStatement, ISqlComment
{
    /// <summary>
    /// If true, it will use the "--" prefix for multi-line comments. If false (default) 
    /// it will use the /* ... */ style for multi-line comments.
    /// </summary>
    public bool SingleLineCommentsOnly { get; set; } = false;

    /// <summary>
    /// The comment text. 
    /// </summary>
    public string CommentText { get; set; }

    /// <summary>
    /// Builds the SQL comment block. If any <see cref="System.Environment.NewLine"/>s are found in the 
    /// <see cref="CommentText"/>, it will automatically create a multi-line comment block out of them.
    /// If <see cref="SingleLineCommentsOnly"/> is true, then it will use the "--" prefix for multi-line 
    /// comments. If false (default) it will use the /* ... */ style for multi-line comments.
    /// </summary>
    public override string SQL()
    {
        if (string.IsNullOrEmpty(CommentText))
            return string.Empty;

        if (!CommentText.Contains(Environment.NewLine))
            return $"{IndentString}-- {CommentText.TrimStart().TrimEnd()}{Environment.NewLine}";

        var sql = new StringBuilder();

        if (!SingleLineCommentsOnly)
            sql.Append($"{IndentString}/*{Environment.NewLine}");

        CommentText
            .Split(Environment.NewLine)
            .ToList()
            .ForEach(commentText =>
            {
                commentText = commentText.TrimStart(Environment.NewLine.ToCharArray()).Trim();
                var commentStart = SingleLineCommentsOnly ? "--" : " *";
                sql.Append($"{IndentString}{commentStart} {commentText}{Environment.NewLine}");
            });

        if (!SingleLineCommentsOnly)
            sql.Append($"{IndentString}*/{Environment.NewLine}");

        return sql.ToString();
    }
}
