using System.Data;

namespace KnightMoves.SqlObjects.SqlCode.TSQL
{
    public abstract partial class TSQLStatement : SqlStatement
    {
        /// <summary>
        /// Finish a calculation adding to a column as the right operand
        /// </summary>
        public override SqlStatement Plus(string multipartIdentifier, string column)
        {
            var parent = GetExpressionParent();

            var calc = new TSQLCalculation
            {
                LeftOperand = parent.LeftOperand != null ? parent.LeftOperand : new TSQLColumn { MultiPartIdentifier = parent.LeftMultiPartIdentifier, ColumnName = parent.LeftOperandValue },
                Operator = SqlArithmeticOperators.Plus,
                RightOperand = new TSQLColumn { MultiPartIdentifier = multipartIdentifier, ColumnName = column }
            };

            parent.Children.Add(calc);

            parent.ClearTempValues();

            return calc;
        }

        /// <summary>
        /// Finish a calculation adding to an <see cref="int"/> as the right operand
        /// </summary>
        public override SqlStatement Plus(int operand)
        {
            var parent = GetExpressionParent();

            var calc = new TSQLCalculation
            {
                LeftOperand = parent.LeftOperand != null ? parent.LeftOperand : new TSQLColumn { MultiPartIdentifier = parent.LeftMultiPartIdentifier, ColumnName = parent.LeftOperandValue },
                Operator = SqlArithmeticOperators.Plus,
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
        /// Finish a calculation adding to a <see cref="decimal"/> as the right operand
        /// </summary>
        public override SqlStatement Plus(decimal operand)
        {
            var parent = GetExpressionParent();

            var calc = new TSQLCalculation
            {
                LeftOperand = parent.LeftOperand != null ? parent.LeftOperand : new TSQLColumn { MultiPartIdentifier = parent.LeftMultiPartIdentifier, ColumnName = parent.LeftOperandValue },
                Operator = SqlArithmeticOperators.Plus,
                RightOperand = new TSQLLiteral
                {
                    DataType = new TSQLDataType(SqlDbType.Decimal),
                    Value = operand.ToString()
                }
            };

            parent.Children.Add(calc);

            parent.ClearTempValues();

            parent.ClearTempValues();

            return calc;
        }

        /// <summary>
        /// Finish a calculation adding to a <see cref="long"/> as the right operand
        /// </summary>
        public override SqlStatement Plus(long operand)
        {
            var parent = GetExpressionParent();

            var calc = new TSQLCalculation
            {
                LeftOperand = parent.LeftOperand != null ? parent.LeftOperand : new TSQLColumn { MultiPartIdentifier = parent.LeftMultiPartIdentifier, ColumnName = parent.LeftOperandValue },
                Operator = SqlArithmeticOperators.Plus,
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
