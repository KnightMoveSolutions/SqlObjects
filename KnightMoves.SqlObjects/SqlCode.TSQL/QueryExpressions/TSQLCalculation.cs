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

using KnightMoves.Hierarchical;
using Newtonsoft.Json;

namespace KnightMoves.SqlObjects.SqlCode.TSQL;

/// <summary>
/// <para>
/// This class inherits from the <see cref="ISqlCalculation"/> abstract class.
/// It builds a SQL calculation of the following form.
/// </para>
/// <code>
/// ( [LeftOperand] [Operator] [RightOperand] )
/// </code>
/// </summary>
public class TSQLCalculation : TSQLStatement, ISqlCalculation
{
    /// <summary>
    /// The alias of the SQL expression so it can be referenced easily in other SQL fragments.
    /// </summary>
    public string Alias { get; set; }
    
    /// <summary>
    /// The return data type of the calculation
    /// </summary>
    public SqlDataType DataType { get; set; }

    /// <summary>
    /// A <see cref="SqlArithmeticOperators"/> enum value representing the operator of the SQL calculation
    /// </summary>
    public SqlArithmeticOperators Operator { get; set; }

    /// <summary>
    /// A <see cref="ISqlQueryExpression"/> representing the right operand of the SQL calculation
    /// </summary>
    [JsonConverter(typeof(TreeNodeJsonConverter))]
    public ISqlQueryExpression RightOperand { get; set; }

    /// <summary>
    /// <para>
    /// Returns a SQL calculation of the following form.
    /// </para>
    /// <code>
    /// ( [LeftOperand] [Operator] [RightOperand] )
    /// </code>
    /// </summary>
    public override string SQL()
    {
        string sql = "";

        // Indent this guy only if it isn't part of a CASE WHEN statement.
        if (Parent != null && Parent.GetType() != typeof(TSQLCaseWhen))
            sql = IndentString;

        sql += "(";

        sql += LeftOperand;

        sql += $" {GetOperatorString(Operator)} ";

        sql += RightOperand;

        sql += ")";

        if (!string.IsNullOrEmpty(Alias))
            sql += $" AS [{Alias.SanitizeDelimitedValue()}]";

        return sql;
    }
}
