using System;
using System.Linq;
using System.Text;

namespace KnightMoves.SqlObjects.SqlCode.TSQL
{
    /// <summary>
    /// This class impelements <see cref="ISqlWhereClause"/> and builds a SQL WHERE clause
    /// using all <see cref="ISqlCondition"/> objects added to it for the filtering conditions
    /// </summary>
    public class TSQLWhereClause : TSQLStatement, ISqlWhereClause
    {
        /// <summary>
        /// Builds a SQL WHERE clause using the <see cref="ISqlCondition"/> objects for the 
        /// conditions of the WHERE clause. It includes 1=1 as the first condition by default so 
        /// if no <see cref="ISqlCondition"/> objects exist then it will still be a valid WHERE clause.
        /// </summary>
        public override string SQL()
        {
            var sql = new StringBuilder();

            var children = Children.Select(c => c as SqlStatement);

            sql.Append($"{IndentString}WHERE 1=1{Environment.NewLine}");

            foreach (var sqlObject in children.Where(s => s.IsCondition || s.IsComment))
            {
                if (sqlObject.IsComment)
                {
                    sql.Append($"{sqlObject}");
                    continue;
                }

                sql.Append($"{sqlObject.IndentString}{sqlObject.LogicalOperator}");

                if (sqlObject.IsConditionGroup)
                    sql.Append(Environment.NewLine);
                else
                    sql.Append(" ");

                var conditionSql = sqlObject.IsConditionGroup ? sqlObject.ToString() : sqlObject.ToString().Trim();

                sql.Append($"{conditionSql}{Environment.NewLine}");
            }

            return sql.ToString();
        }
    }
}
