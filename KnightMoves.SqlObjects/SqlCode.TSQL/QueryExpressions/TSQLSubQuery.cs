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
                                    .Select(c => c as SqlStatement)
                                    .Where(c => c.IsSelect || c.IsFrom || c.IsJoin || c.IsWhereClause).ToList();

        foreach (var subQueryFragment in subQueryFragments)
            sql.Append($"{subQueryFragment}");

        sql.Append($"{IndentString})");

        if (!string.IsNullOrEmpty(Alias))
            sql.Append($" AS [{Alias.SanitizeDelimitedValue()}]{Environment.NewLine}");

        return sql.ToString();
    }
}
