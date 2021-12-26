using System.Data;
using KnightMoves.SqlObjects.SqlCode.TSQL;
using Xunit;

namespace KnightMoves.SqlObjects.Tests.TSql
{
    public class TSQLFuncMin_Tests
    {
        [Fact]
        public void SQL_Returns_Min_With_Default()
        {
            // ARRANGE
            var func = new TSQLFuncMin();

            // ACTION
            var sql = func.SQL();

            var expected = "MIN(0)";

            // ASSERT
            Assert.Equal(expected, sql);
            TestHelper.Assert.SerializationWorks(expected, func);
        }

        [Fact]
        public void SQL_Returns_Min_With_Column()
        {
            // ARRANGE
            var func = new TSQLFuncMin();

            var aggregateExpression = new TSQLColumn { DataType = new TSQLDataType(SqlDbType.Variant), ColumnName = "ColumnName" };

            func.SetParameterValue(TSQLFuncMinParams.aggregateExpression, aggregateExpression);

            // ACTION
            var sql = func.SQL();

            var expected = "MIN([ColumnName])";

            // ASSERT
            Assert.Equal(expected, sql);
            TestHelper.Assert.SerializationWorks(expected, func);
        }
    }
}
