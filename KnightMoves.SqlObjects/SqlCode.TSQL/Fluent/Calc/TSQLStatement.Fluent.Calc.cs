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
