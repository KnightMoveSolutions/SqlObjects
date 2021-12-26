using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KnightMoves.SqlObjects.SqlCode.TSQL
{
    /// <summary>
    /// This class implements <see cref="ISqlOrderBy"/> and builds a SQL ORDER BY clause using 
    /// any <see cref="ISqlOrderByExpression"/>s that were added to it as the sorting spec
    /// </summary>
    public class TSQLOrderBy : TSQLStatement, ISqlOrderBy
    {
        /// <summary>
        /// Builds a SQL ORDER BY clause using any <see cref="ISqlOrderByExpression"/>s that 
        /// were added to it as the sorting spec
        /// <code>ORDER BY</code>
        /// <code>-- OrderBy expression list here</code>
        /// </summary>
        public override string SQL()
        {
            var sql = new StringBuilder();

            sql.Append($"{IndentString}ORDER BY{Environment.NewLine}");

            IEnumerable<SqlStatement> orderByExpressions = null;

            var children = Children.Select(c => c as SqlStatement);

            // First we check to see if any items were added to the group by object as children
            if (children.Any(c => c.IsOrderByExpression))
            {
                orderByExpressions = children.Where(c => c.IsOrderByExpression || c.IsComment);
            }

            if (orderByExpressions == null || !orderByExpressions.Any())
                return $"{IndentString}-- No ORDER BY expressions found";

            foreach (var sqlObject in orderByExpressions)
            {
                if (sqlObject.IsComment)
                {
                    sql.Append($"{sqlObject}");
                    continue;
                }

                sql.Append($"{sqlObject},{Environment.NewLine}");
            }

            if (orderByExpressions.LastOrDefault(x => x.IsOrderByExpression) != null)
                sql.Remove(sql.Length - 3, 1); // Remove trailing comma

            return sql.ToString();
        }
    }
}
