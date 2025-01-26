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
/// This class implements <see cref="ITSQLCaseExpression"/>. It builds a WHEN...THEN clause for a T-SQL CASE statement.
/// </summary>
public class TSQLCaseWhen : TSQLStatement, ITSQLCaseExpression
{
    /// <summary>
    /// The alias of the SQL expression so it can be referenced easily in other SQL fragments.
    /// </summary>
    public string Alias { get; set; }

    /// <summary>
    /// The data type of the value returned by the WHEN condition result
    /// </summary>
    public SqlDataType DataType { get; set; }

    /// <summary>
    /// Returns a WHEN...THEN clause for a T-SQL CASE statement. 
    /// </summary>
    public override string SQL()
    {
        var sql = new StringBuilder();

        sql.Append($"{IndentString}WHEN ");

        // The last ISqlCondition is the result for the THEN clause so skip it
        var conditionList = Children
                                .Where(c => (c as SqlStatement).IsCondition)
                                .Take(Children.Count() - 1);

        var result = Children.Where(c => (c as SqlStatement).IsQueryExpression);

        if (conditionList == null || conditionList.Count() == 0 || result == null || result.Count() == 0)
            throw new InvalidOperationException("Cannot produce valid SQL. The CASE WHEN clause requires at least one condition and a result.  SQL Fragment Name='" + Name + "', SQL Fragment ID='" + Id + "'");

        var resultExp = result.First() as ISqlQueryExpression;

        if (Parent != null && resultExp.DataType != ((TSQLCase)Parent).DataType)
            throw new InvalidOperationException("The DataType of the result of the WHEN clause does not match the DataType of the CASE statement.  SQL Fragment Name='" + Name + "', SQL Fragment ID='" + Id + "'");

        foreach (SqlStatement condition in conditionList)
        {
            sql.Append(condition);

            if (Children.IndexOf(condition) < conditionList.Count() - 1)
                sql.Append($" {condition.LogicalOperator}");

            sql.Append(" ");
        }

        sql.Append($"THEN {resultExp}");

        return sql.ToString();
    }
}
