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
