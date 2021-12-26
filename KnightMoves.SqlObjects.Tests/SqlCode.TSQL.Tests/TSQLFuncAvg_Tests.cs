using System.Data;
using KnightMoves.SqlObjects.SqlCode.TSQL;
using Xunit;

namespace KnightMoves.SqlObjects.Tests.TSql
{
    public class TSQLFuncAvg_Tests
    {
        [Fact]
        public void SQL_Returns_AVG_Function_With_Zero_Default_Arg()
        {
            // ARRANGE
            var func = new TSQLFuncAvg();

            // ACTION
            var sql = func.SQL();

            var expected = "AVG(0)";

            // ASSERT
            Assert.Equal(expected, sql);
            TestHelper.Assert.SerializationWorks(expected, func);
        }

        [Fact]
        public void SQL_Returns_AVG_Function_With_Column_Parameter()
        {
            // ARRANGE
            var func = new TSQLFuncAvg();

            var paramValue = new TSQLColumn
            {
                DataType = new TSQLDataType(SqlDbType.Decimal),
                ColumnName = "ColumnName"
            };

            func.SetParameterValue(TSQLFuncAvgParams.aggregateExpression, paramValue);

            // ACTION
            var sql = func.SQL();

            var expected = "AVG([ColumnName])";

            // ASSERT
            Assert.Equal(expected, sql);
            TestHelper.Assert.SerializationWorks(expected, func);
        }
    }
}
