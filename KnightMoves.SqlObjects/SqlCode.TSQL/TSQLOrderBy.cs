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

        // First we check to see if any items were added to the group by object as children
        if (Children.Any(c => c.IsOrderByExpression))
        {
            orderByExpressions = Children.Where(c => c.IsOrderByExpression || c.IsComment);
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
