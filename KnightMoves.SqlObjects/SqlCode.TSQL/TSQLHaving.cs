using System;
using System.Linq;
using System.Text;

namespace KnightMoves.SqlObjects.SqlCode.TSQL
{
    /// <summary>
    /// This class implements <see cref="ISqlHaving"/> and builds a SQL HAVING clause to 
    /// be used in conjunction with a GROUP BY clause
    /// </summary>
    public class TSQLHaving : TSQLStatement, ISqlHaving
    {
        /// <summary>
        /// Builds a SQL HAVING clause for filtering the results of a GROUP BY clause. It 
        /// includes 1=1 as the first condition in case no conditions have been added so 
        /// that it still produces a valid HAVING clause.
        /// </summary>
        public override string SQL()
        {
            var sql = new StringBuilder();

            sql.Append($"{IndentString}HAVING 1=1{Environment.NewLine}");

            var children = Children.Select(c => c as SqlStatement);

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
