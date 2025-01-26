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

using System.Linq;
using System.Text;

namespace KnightMoves.SqlObjects.SqlCode.TSQL;

/// <summary>
/// This class implements <see cref="ISqlOrderByExpression"/> and builds a T-SQL
/// ORDER BY expression that includes the sort direction (either ASC or DESC)
/// <code>
/// [orderByExpression] ASC
/// </code>
/// <code>
/// [orderByExpression] DESC
/// </code>
/// </summary>
public class TSQLOrderByExpression : TSQLStatement, ISqlOrderByExpression
{
    /// <summary>
    /// The ORDER BY expression
    /// </summary>
    //[JsonConverter(typeof(TreeNodeJsonConverter))]
    //public ISqlQueryExpression Expression { get; set; }

    /// <summary>
    /// The sort order direction (either ASC or DESC)
    /// </summary>
    public SqlOrderByDirections Direction { get; set; } = SqlOrderByDirections.ASC;

    /// <summary>
    /// <para>
    /// Returns an expression suitable for use in an ORDER BY clause that includes the 
    /// expression and its specified sort direction (either ASC or DESC)
    /// </para>
    /// <code>
    /// [orderByExpression] ASC
    /// </code>
    /// <code>
    /// [orderByExpression] DESC
    /// </code>
    /// </summary>
    public override string SQL()
    {
        var sql = new StringBuilder();

        var orderByExpression = Children.FirstOrDefault(c => c.IsQueryExpression) as ISqlQueryExpression;

        var orderByExpressionStr = string.Empty;

        if (!string.IsNullOrEmpty(orderByExpression.Alias))
        {
            orderByExpressionStr = $"[{orderByExpression.Alias.SanitizeDelimitedValue()}]";
        } 
        else
        {
            orderByExpressionStr = orderByExpression.ToString().Trim();
        }

        if ((orderByExpression as SqlStatement).IsSubQuery)
            orderByExpressionStr = $"{IndentString}{orderByExpressionStr}";

        sql.Append($"{IndentString}{orderByExpressionStr} {Direction}");

        return sql.ToString();
    }
}
