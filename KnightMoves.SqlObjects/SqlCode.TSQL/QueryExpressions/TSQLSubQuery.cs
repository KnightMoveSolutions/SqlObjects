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
/// This class implements <see cref="ISqlSubQuery"/> and builds a query wrapped in a scope 
/// denoted by open and closing parentheses
/// </summary>
public class TSQLSubQuery : TSQLStatement, ISqlSubQuery
{
    public string Alias { get; set; }
    public SqlDataType DataType { get; set; }

    /// <summary>
    /// This class implements <see cref="ISqlSubQuery"/> and builds a query wrapped in a scope 
    /// denoted by open and closing parentheses
    /// <code>
    /// (
    /// </code>
    /// <code>
    ///    /* sub-query here */
    /// </code>
    /// <code>
    /// )
    /// </code>
    /// </summary>
    public override string SQL()
    {
        StringBuilder sql = new StringBuilder();

        sql.Append($"{IndentString}({Environment.NewLine}");

        var subQueryFragments = Children
                                    .Where(c => c.IsSelect || c.IsFrom || c.IsJoin || c.IsWhereClause).ToList();

        foreach (var subQueryFragment in subQueryFragments)
            sql.Append($"{subQueryFragment}");

        sql.Append($"{IndentString})");

        if (!string.IsNullOrEmpty(Alias))
            sql.Append($" AS [{Alias.SanitizeDelimitedValue()}]{Environment.NewLine}");

        return sql.ToString();
    }
}
