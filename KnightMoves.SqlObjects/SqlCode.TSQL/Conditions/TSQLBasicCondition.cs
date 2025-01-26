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

using System.Linq;
using System.Text;

namespace KnightMoves.SqlObjects.SqlCode.TSQL;

/// <summary>
/// This class implements <see cref="ISqlBasicCondition"/> and builds a basic condition 
/// </summary>
public class TSQLBasicCondition : TSQLStatement, ISqlBasicCondition
{
    /// <summary>
    /// A <see cref="SqlComparisonOperators"/> enum value that represents the operator between the 
    /// left and right operands of a condition. Comparison operators are =, &lt;=, &gt;=, &lt;&gt;, and %
    /// </summary>
    public SqlComparisonOperators Operator { get; set; }

    /// <summary>
    /// <para>
    /// Returns a basic condition for use in a T-SQL WHERE clause of the following form of 
    /// </para>
    /// <para>
    /// [LeftOperand] [ComparisonOperator] [RightOperand]
    /// </para>
    /// </summary>
    public override string SQL()
    {
        var sql = new StringBuilder();

        var leftOperand = Children.FirstOrDefault(c => c is ISqlQueryExpression);
        var rightOperand = Children.LastOrDefault(c => c is ISqlQueryExpression);

        sql.Append(leftOperand.ToString().Trim());
        sql.Append($" {GetOperatorString(Operator)} ");
        sql.Append(rightOperand.ToString().Trim());

        return sql.ToString();
    }
}
