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
/// This class implements <see cref="ISqlSelect"/> and builds a SELECT statement using the 
/// <see cref="ISqlQueryExpression"/> objects added to it for the SELECT list.
/// </summary>
public class TSQLSelect : TSQLStatement, ISqlSelect
{
    /// <summary>
    /// Builds a SQL SELECT statement with all child statements in the Children 
    /// collection as the SELECT list.
    /// <code>SELECT</code>
    /// <code>-- Query Expression list here</code>
    /// </summary>
    public override string SQL()
    {
        var sql = new StringBuilder();

        sql.Append($"{IndentString}SELECT");

        if(!Children.Any())
        {
            sql.Append(" * ");

            return sql.ToString();
        }

        sql.Append(Environment.NewLine);

        ISqlQueryExpression expression;

        foreach (var sqlObject in Children.Where(c => c.IsQueryExpression || c.IsComment))
        {
            if (sqlObject.IsComment)
            {
                sql.Append(sqlObject);
                continue;
            }

            expression = sqlObject as ISqlQueryExpression;

            sql.Append(expression);

            if (!string.IsNullOrEmpty(expression.Alias))
            {
                if (sql.ToString().EndsWith(Environment.NewLine))
                    sql.Remove(sql.Length - 2, 2);
            }

            sql.Append("," + Environment.NewLine);
        }

        sql.Remove(sql.Length - 3, 1); // Remove trailing comma

        return sql.ToString();
    }
}
