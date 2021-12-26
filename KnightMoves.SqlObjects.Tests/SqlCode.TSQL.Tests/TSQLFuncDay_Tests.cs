using System.Data;
using KnightMoves.SqlObjects.SqlCode.TSQL;
using Xunit;

namespace KnightMoves.SqlObjects.Tests.TSql
{
    public class TSQLFuncDay_Tests
    {
        [Fact]
        public void SQL_Returns_Day_With_Defaults()
        {
            // ARRANGE
            var func = new TSQLFuncDay();

            // ACTION
            var sql = func.SQL();

            var expected = "DAY(GETDATE())";

            // ASSERT
            Assert.Equal(expected, sql);
            TestHelper.Assert.SerializationWorks(expected, func);
        }

        [Fact]
        public void SQL_Returns_Day_With_Literal()
        {
            // ARRANGE
            var func = new TSQLFuncDay();

            var dateExpression = new TSQLLiteral { DataType = new TSQLDataType(SqlDbType.DateTime), Value = "01/01/2012" };

            func.SetParameterValue(TSQLFuncDayParams.dateExpression, dateExpression);

            // ACTION
            var sql = func.SQL();

            var expected = "DAY('01/01/2012')";

            // ASSERT
            Assert.Equal(expected, sql);
            TestHelper.Assert.SerializationWorks(expected, func);
        }

        [Fact]
        public void SQL_Returns_Day_With_Column()
        {
            // ARRANGE
            var func = new TSQLFuncDay();

            var dateExpression = new TSQLColumn { DataType = new TSQLDataType(SqlDbType.DateTime), ColumnName = "DateColumnName" };

            func.SetParameterValue(TSQLFuncDayParams.dateExpression, dateExpression);

            // ACTION
            var sql = func.SQL();

            var expected = "DAY([DateColumnName])";

            // ASSERT
            Assert.Equal(expected, sql);
            TestHelper.Assert.SerializationWorks(expected, func);
        }
    }
}
