using System.Data;
using KnightMoves.SqlObjects.SqlCode.TSQL;
using Xunit;

namespace KnightMoves.SqlObjects.Tests.TSql
{
    public class TSQLFuncCount_Tests
    {
        [Fact]
        public void SQL_Returns_Count_With_Default()
        {
            // ARRANGE
            var func = new TSQLFuncCount();

            // ACTION
            var sql = func.SQL();

            var expected = "COUNT(0)";

            // ASSERT
            Assert.Equal(expected, sql);
            TestHelper.Assert.SerializationWorks(expected, func);
        }

        [Fact]
        public void SQL_Returns_Count_With_Column()
        {
            // ARRANGE
            var func = new TSQLFuncCount();

            var paramValue = new TSQLColumn 
            { 
                DataType = new TSQLDataType(SqlDbType.Variant), 
                ColumnName = "ColumnName" 
            };

            func.SetParameterValue("aggregateExpression", paramValue);

            // ACTION
            var sql = func.SQL();

            var expected = "COUNT([ColumnName])";

            // ASSERT
            Assert.Equal(expected, sql);
            TestHelper.Assert.SerializationWorks(expected, func);
        }
    }
}
