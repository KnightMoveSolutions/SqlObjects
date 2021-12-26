using System.Data;
using KnightMoves.SqlObjects.SqlCode.TSQL;
using Xunit;

namespace KnightMoves.SqlObjects.Tests.TSql
{
    public class TSQLFuncSum_Tests
    {
        [Fact]
        public void SQL_Returns_Sum_With_Default()
        {
            // ARRANGE
            var func = new TSQLFuncSum();

            // ACTION
            var sql = func.SQL();

            var expected = "SUM(0)";

            // ASSERT
            Assert.Equal(expected, sql);
            TestHelper.Assert.SerializationWorks(expected, func);
        }

        [Fact]
        public void SQL_Returns_Sum_With_Column()
        {
            // ARRANGE
            var func = new TSQLFuncSum();

            var aggregateExpression = new TSQLColumn { DataType = new TSQLDataType(SqlDbType.Variant), ColumnName = "ColumnName" };

            func.SetParameterValue(TSQLFuncSumParams.aggregateExpression, aggregateExpression);

            // ACTION
            var sql = func.SQL();

            var expected = "SUM([ColumnName])";

            // ASSERT
            Assert.Equal(expected, sql);
            TestHelper.Assert.SerializationWorks(expected, func);
        }
    }
}
