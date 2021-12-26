using System.Data;

namespace KnightMoves.SqlObjects.SqlCode.TSQL
{
    public abstract partial class TSQLStatement : SqlStatement
    {
        /// <summary>
        /// Finish a calculation multiplying by a column as the right operand
        /// </summary>
        public override SqlStatement Times(string multipartIdentifier, string column)
        {
            var parent = GetExpressionParent();

            var calc = new TSQLCalculation
            {
                LeftOperand = parent.LeftOperand != null ? parent.LeftOperand : new TSQLColumn { MultiPartIdentifier = parent.LeftMultiPartIdentifier, ColumnName = parent.LeftOperandValue },
                Operator = SqlArithmeticOperators.Multiply,
                RightOperand = new TSQLColumn { MultiPartIdentifier = multipartIdentifier, ColumnName = column }
            };

            parent.Children.Add(calc);

            parent.ClearTempValues();

            return calc;
        }

        /// <summary>
        /// Finish a calculation multiplying by an <see cref="int"/> as the right operand
        /// </summary>
        public override SqlStatement Times(int operand)
        {
            var parent = GetExpressionParent();

            var calc = new TSQLCalculation
            {
                LeftOperand = parent.LeftOperand != null ? parent.LeftOperand : new TSQLColumn { MultiPartIdentifier = parent.LeftMultiPartIdentifier, ColumnName = parent.LeftOperandValue },
                Operator = SqlArithmeticOperators.Multiply,
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
        /// Finish a calculation multiplying by a <see cref="decimal"/> as the right operand
        /// </summary>
        public override SqlStatement Times(decimal operand)
        {
            var parent = GetExpressionParent();

            var calc = new TSQLCalculation
            {
                LeftOperand = parent.LeftOperand != null ? parent.LeftOperand : new TSQLColumn { MultiPartIdentifier = parent.LeftMultiPartIdentifier, ColumnName = parent.LeftOperandValue },
                Operator = SqlArithmeticOperators.Multiply,
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
        /// Finish a calculation multiplying by a <see cref="long"/> as the right operand
        /// </summary>
        public override SqlStatement Times(long operand)
        {
            var parent = GetExpressionParent();

            var calc = new TSQLCalculation
            {
                LeftOperand = parent.LeftOperand != null ? parent.LeftOperand : new TSQLColumn { MultiPartIdentifier = parent.LeftMultiPartIdentifier, ColumnName = parent.LeftOperandValue },
                Operator = SqlArithmeticOperators.Multiply,
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
}
