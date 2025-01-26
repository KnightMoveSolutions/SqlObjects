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
