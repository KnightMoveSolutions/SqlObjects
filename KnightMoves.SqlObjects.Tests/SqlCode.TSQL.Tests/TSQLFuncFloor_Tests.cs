using System.Data;
using KnightMoves.SqlObjects.SqlCode.TSQL;
using Xunit;

namespace KnightMoves.SqlObjects.Tests.TSql
{
    public class TSQLFuncFloor_Tests
    {
        [Fact]
        public void SQL_Returns_Ceiling_With_Defaults()
        {
            // ARRANGE
            var func = new TSQLFuncFloor();

            // ACTION
            var sql = func.SQL();

            var expected = "FLOOR(0)";

            // ASSERT
            Assert.Equal(expected, sql);
            TestHelper.Assert.SerializationWorks(expected, func);
        }

        [Fact]
        public void SQL_Returns_Ceiling_With_Literal()
        {
            // ARRANGE
            var func = new TSQLFuncFloor();

            var numericExpression = new TSQLLiteral { DataType = new TSQLDataType(SqlDbType.Decimal), Value = "99.9" };

            func.SetParameterValue(TSQLFuncFloorParams.numericExpression, numericExpression);

            // ACTION
            var sql = func.SQL();

            var expected = "FLOOR(99.9)";

            // ASSERT
            Assert.Equal(expected, sql);
            TestHelper.Assert.SerializationWorks(expected, func);
        }

        [Fact]
        public void SQL_Returns_Ceiling_With_Column()
        {
            // ARRANGE
            var func = new TSQLFuncFloor();

            var numericExpression = new TSQLColumn { DataType = new TSQLDataType(SqlDbType.Decimal), ColumnName = "NumericColumnName" };

            func.SetParameterValue(TSQLFuncFloorParams.numericExpression, numericExpression);

            // ACTION
            var sql = func.SQL();

            var expected = "FLOOR([NumericColumnName])";

            // ASSERT
            Assert.Equal(expected, sql);
            TestHelper.Assert.SerializationWorks(expected, func);
        }
    }
}
