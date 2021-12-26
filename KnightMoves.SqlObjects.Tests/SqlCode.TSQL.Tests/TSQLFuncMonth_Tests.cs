using System.Data;
using KnightMoves.SqlObjects.SqlCode.TSQL;
using Xunit;

namespace KnightMoves.SqlObjects.Tests.TSql
{
    public class TSQLFuncMonth_Tests
    {
        [Fact]
        public void SQL_Returns_Month_With_Defaults()
        {
            // ARRANGE
            var func = new TSQLFuncMonth();

            // ACTION
            var sql = func.SQL();

            var expected = "MONTH(GETDATE())";

            // ASSERT
            Assert.Equal(expected, sql);
            TestHelper.Assert.SerializationWorks(expected, func);
        }

        [Fact]
        public void SQL_Returns_Month_With_Literal()
        {
            // ARRANGE
            var func = new TSQLFuncMonth();

            var dateExpression = new TSQLLiteral { DataType = new TSQLDataType(SqlDbType.DateTime), Value = "01/01/2012" };

            func.SetParameterValue(TSQLFuncMonthParams.dateExpression, dateExpression);

            // ACTION
            var sql = func.SQL();

            var expected = "MONTH('01/01/2012')";

            // ASSERT
            Assert.Equal(expected, sql);
            TestHelper.Assert.SerializationWorks(expected, func);
        }

        [Fact]
        public void SQL_Returns_Month_With_Column()
        {
            // ARRANGE
            var func = new TSQLFuncMonth();

            var dateExpression = new TSQLColumn { DataType = new TSQLDataType(SqlDbType.DateTime), ColumnName = "DateColumnName" };

            func.SetParameterValue(TSQLFuncMonthParams.dateExpression, dateExpression);

            // ACTION
            var sql = func.SQL();

            var expected = "MONTH([DateColumnName])";

            // ASSERT
            Assert.Equal(expected, sql);
            TestHelper.Assert.SerializationWorks(expected, func);
        }
    }
}
