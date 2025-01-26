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

using System.Data;

namespace KnightMoves.SqlObjects.SqlCode.TSQL;

public abstract partial class TSQLStatement : SqlStatement
{
    /// <summary>
    /// Start a calculation with a column as the left operand (e.g. [dbo].[MyColumn])
    /// </summary>
    public override SqlStatement Calculate(string multipartIdentifier, string column)
    {
        var parent = GetExpressionParent();
        parent.LeftMultiPartIdentifier = multipartIdentifier;
        parent.LeftOperandValue = column;
        return this;
    }

    /// <summary>
    /// Start a calculation with an integer as the left operand
    /// </summary>
    public override SqlStatement Calculate(int operand)
    {
        var parent = GetExpressionParent();

        parent.LeftOperand = new TSQLLiteral
        {
            DataType = new TSQLDataType(SqlDbType.Int),
            Value = operand.ToString()
        };

        return this;
    }

    /// <summary>
    /// Start a calculation with a decimal as the left operand
    /// </summary>
    public override SqlStatement Calculate(decimal operand)
    {
        var parent = GetExpressionParent();

        parent.LeftOperand = new TSQLLiteral
        {
            DataType = new TSQLDataType(SqlDbType.Decimal),
            Value = operand.ToString()
        };

        return this;
    }

    /// <summary>
    /// Start a calculation with a long as the left operand
    /// </summary>
    public override SqlStatement Calculate(long operand)
    {
        var parent = GetExpressionParent();

        parent.LeftOperand = new TSQLLiteral
        {
            DataType = new TSQLDataType(SqlDbType.BigInt),
            Value = operand.ToString()
        };

        return this;
    }

}
