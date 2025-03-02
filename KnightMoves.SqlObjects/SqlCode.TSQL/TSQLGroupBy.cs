﻿/*
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
/// This class implements <see cref="ISqlGroupBy"/> and builds a SQL GROUP BY clause
/// </summary>
public class TSQLGroupBy : TSQLStatement, ISqlGroupBy
{
    /// <summary>
    /// Returns a GROUP BY SQL statement with the non-aggregate elements of the 
    /// <see cref="SqlCode.SelectList"/> object passed in the constructor.
    /// </summary>
    public override string SQL()
    {
        var sql = new StringBuilder();

        sql.Append($"{IndentString}GROUP BY{Environment.NewLine}");

        IEnumerable<SqlStatement> groupItems = null;

        // First we check to see if any items were added to the group by object as children
        if (Children.Any(c => c is ISqlQueryExpression))
        {
            groupItems = Children.Where(c => c.IsQueryExpression || c.IsComment);
        }

        // If not then we look for a SELECT clause in the sibling collection to auto-build the items
        if (groupItems == null)
        {
            var selectList = Siblings.SingleOrDefault(s => s.IsSelect);

            if (selectList == null)
                return $"{IndentString}-- No Group items or SELECT clause found to build GROUP BY clause from";

            groupItems = selectList.Children.Where(c => c.IsQueryExpression);
        }

        if (!groupItems.Any())
            return $"{IndentString}-- No grouping items or SELECT clause found";

        foreach (SqlStatement sqlObject in groupItems)
        {
            if (sqlObject.IsFunction && ((ISqlFunction)sqlObject).IsAggregate == true)
                continue;

            if (sqlObject.IsComment)
            {
                sql.Append($"{sqlObject}");
                continue;
            }

            var qryExp = sqlObject as ISqlQueryExpression;

            var alias = qryExp.Alias;

            qryExp.Alias = string.Empty;

            sql.Append($"{sqlObject},{Environment.NewLine}");

            qryExp.Alias = alias;
        }

        if (groupItems.LastOrDefault(x => x.IsQueryExpression) != null)
            sql.Remove(sql.Length - 3, 1); // Remove trailing comma

        return sql.ToString();
    }
}
