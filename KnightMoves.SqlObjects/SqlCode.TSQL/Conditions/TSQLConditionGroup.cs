using System;
using System.Linq;
using System.Text;

namespace KnightMoves.SqlObjects.SqlCode.TSQL;

/// <summary>
/// This class places all <see cref="ISqlCondition"/> objects from the Conditions collection 
/// within a scoped SQL block (i.e. within parentheses)
/// </summary>
public class TSQLConditionGroup : TSQLStatement, ISqlConditionGroup
{
    /// <summary>
    /// Returns the <see cref="ISqlCondition"/> objects from the Conditions collection within a 4
    /// scoped SQL block (i.e. within parentheses)
    /// </summary>
    public override string SQL()
    {
        var sql = new StringBuilder();

        sql.Append($"{IndentString}({Environment.NewLine}");

        var conditions = Children.Where(c => (c as SqlStatement).IsCondition).ToList();

        foreach (var condition in conditions)
        {
            sql.Append($"{condition.IndentString}{condition}");

            if (conditions.IndexOf(condition) < conditions.Count - 1)
                sql.Append($" {(condition as ISqlCondition).LogicalOperator}");

            sql.Append(Environment.NewLine);
        }

        sql.Append($"{IndentString})");

        return sql.ToString();
    }
}
