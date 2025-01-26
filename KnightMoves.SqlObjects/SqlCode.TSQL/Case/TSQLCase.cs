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
/// This class implements <see cref="ITSQLCaseExpression"/>. It builds a T-SQL CASE statement 
/// using the <see cref="TSQLCaseWhen"/> objects and a <see cref="TSQLCaseElse"/> object 
/// (if one is found) in the Children collection.
/// </summary>
public class TSQLCase : TSQLStatement, ITSQLCase
{
    /// <summary>
    /// The alias of the SQL expression so it can be referenced easily in other SQL fragments.
    /// </summary>
    public string Alias { get; set; }

    /// <summary>
    /// A <see cref="SqlDataType"/> object representing the data type returned by the CASE statement.
    /// </summary>
    public SqlDataType DataType { get; set; }

    /// <summary>
    /// This class implements <see cref="ITSQLCaseExpression"/>. It builds a T-SQL CASE statement using the 
    /// <see cref="TSQLCaseWhen"/> objects and a <see cref="TSQLCaseElse"/> object (if one is found) 
    /// in the Children collection.
    /// </summary>
    public override string SQL()
    {
        if(!Children.Any(c => c.GetType() == typeof(TSQLCaseWhen)))
            throw new InvalidOperationException("Cannot produce valid SQL because the CASE statement has no conditions. SQL Fragment Name='" + Name + "', SQL Fragment ID='" + Id + "'");

        StringBuilder sql = new StringBuilder();

        sql.Append($"{(Parent == null ? string.Empty : IndentString)}CASE{Environment.NewLine}");

        ITSQLCaseExpression expression;

        SqlStatement elseClause = null;

        var indentStr = string.Empty;

        // We do this to indent further when the CASE statement is the left operand of a condition
        if (Parent == null) 
            indentStr = $"{new string(IndentCharacter, (DepthFromRoot * 3) + LogicalOperator.ToString().Length)}";

        foreach (SqlStatement sqlStmt in Children)
        {
            if ((sqlStmt is ITSQLCaseExpression) == false)
                continue;

            if (sqlStmt is TSQLCaseElse)
            {
                elseClause = sqlStmt;
                continue;
            }

            expression = sqlStmt as ITSQLCaseExpression;

            if (expression.DataType != DataType)
                throw new InvalidOperationException("The DataType of the CASE WHEN condition does not match the DataType of the CASE clause. SQL Fragment Name='" + Name + "', SQL Fragment ID='" + Id + "'");

            sql.Append($"{indentStr}{sqlStmt}{Environment.NewLine}");
        }

        if (elseClause != null)
            sql.Append($"{indentStr}{elseClause}{Environment.NewLine}");

        sql.Append($"{indentStr}{IndentString}END");

        if (!string.IsNullOrEmpty(Alias))
            sql.Append($" AS [{Alias.SanitizeDelimitedValue()}]");

        return sql.ToString();
    }
}
