using System.Data;
using KnightMoves.SqlObjects.SqlCode;
using KnightMoves.SqlObjects.SqlCode.TSQL;
using Xunit;

namespace KnightMoves.SqlObjects.Tests.TSql
{
    public class TSQLCalculation_Tests
    {
        [Fact]
        public void SQL_Method_Returns_Calculation()
        {
            // ARRANGE
            var calc = new TSQLCalculation
            {
                LeftOperand = new TSQLLiteral { DataType = new TSQLDataType(SqlDbType.Int), Value = "9999" },
                Operator = SqlArithmeticOperators.Plus,
                RightOperand = new TSQLLiteral { DataType = new TSQLDataType(SqlDbType.Int), Value = "1234" }
            };

            // ACTION
            var sql = calc.SQL();

            var expected = "(9999 + 1234)";

            // ASSERT
            Assert.Equal(expected, sql);
            TestHelper.Assert.SerializationWorks(expected, calc);
        }

        [Fact]
        public void SQL_Method_Returns_Nested_Calculation()
        {
            // ARRANGE
            var subCalc = new TSQLCalculation
            {
                LeftOperand = new TSQLLiteral { DataType = new TSQLDataType(SqlDbType.Int), Value = "9999" },
                Operator = SqlArithmeticOperators.Multiply,
                RightOperand = new TSQLLiteral { DataType = new TSQLDataType(SqlDbType.Int), Value = "1234" }
            };

            var calc = new TSQLCalculation
            {
                LeftOperand = new TSQLLiteral { DataType = new TSQLDataType(SqlDbType.Int), Value = "9999" },
                Operator = SqlArithmeticOperators.Plus,
                RightOperand = subCalc
            };

            // ACTION
            var sql = calc.SQL();

            var expected = "(9999 + (9999 * 1234))";

            // ASSERT
            Assert.Equal(expected, sql);
            TestHelper.Assert.SerializationWorks(expected, calc);
        }
    }
}
