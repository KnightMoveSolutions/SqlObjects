using System.Data;

namespace KnightMoves.SqlObjects.SqlCode.TSQL;

public abstract partial class TSQLStatement : SqlStatement
{
    /// <summary>
    /// Finish a calculation subtracting from a column as the right operand
    /// </summary>
    public override SqlStatement Minus(string multipartIdentifier, string column)
    {
        var parent = GetExpressionParent();

        var calc = new TSQLCalculation
        {
            LeftOperand = parent.LeftOperand != null ? parent.LeftOperand : new TSQLColumn { MultiPartIdentifier = parent.LeftMultiPartIdentifier, ColumnName = parent.LeftOperandValue },
            Operator = SqlArithmeticOperators.Minus,
            RightOperand = new TSQLColumn { MultiPartIdentifier = multipartIdentifier, ColumnName = column }
        };

        parent.Children.Add(calc);

        parent.ClearTempValues();

        return calc;
    }

    /// <summary>
    /// Finish a calculation subtracting from an <see cref="int"/> as the right operand
    /// </summary>
    public override SqlStatement Minus(int operand)
    {
        var parent = GetExpressionParent();

        var calc = new TSQLCalculation
        {
            LeftOperand = parent.LeftOperand != null ? parent.LeftOperand : new TSQLColumn { MultiPartIdentifier = parent.LeftMultiPartIdentifier, ColumnName = parent.LeftOperandValue },
            Operator = SqlArithmeticOperators.Minus,
            RightOperand = new TSQLLiteral
            {
                DataType = new TSQLDataType(SqlDbType.Int),
                Value = operand.ToString()
            }
        };

        parent.Children.Add(calc);

        parent.ClearTempValues();

        return calc;
    }

    /// <summary>
    /// Finish a calculation subtracting from a <see cref="decimal"/> as the right operand
    /// </summary>
    public override SqlStatement Minus(decimal operand)
    {
        var parent = GetExpressionParent();

        var calc = new TSQLCalculation
        {
            LeftOperand = parent.LeftOperand != null ? parent.LeftOperand : new TSQLColumn { MultiPartIdentifier = parent.LeftMultiPartIdentifier, ColumnName = parent.LeftOperandValue },
            Operator = SqlArithmeticOperators.Minus,
            RightOperand = new TSQLLiteral
            {
                DataType = new TSQLDataType(SqlDbType.Decimal),
                Value = operand.ToString()
            }
        };

        parent.Children.Add(calc);

        parent.ClearTempValues();

        return calc;
    }

    /// <summary>
    /// Finish a calculation subtracting from a <see cref="long"/> as the right operand
    /// </summary>
    public override SqlStatement Minus(long operand)
    {
        var parent = GetExpressionParent();

        var calc = new TSQLCalculation
        {
            LeftOperand = parent.LeftOperand != null ? parent.LeftOperand : new TSQLColumn { MultiPartIdentifier = parent.LeftMultiPartIdentifier, ColumnName = parent.LeftOperandValue },
            Operator = SqlArithmeticOperators.Minus,
            RightOperand = new TSQLLiteral
            {
                DataType = new TSQLDataType(SqlDbType.BigInt),
                Value = operand.ToString()
            }
        };

        parent.Children.Add(calc);

        parent.ClearTempValues();

        return calc;
    }

}
