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

        sql.Append($"{IndentString}WHERE 1=1{Environment.NewLine}");

        foreach (var sqlObject in Children.Where(s => s.IsCondition || s.IsComment))
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
