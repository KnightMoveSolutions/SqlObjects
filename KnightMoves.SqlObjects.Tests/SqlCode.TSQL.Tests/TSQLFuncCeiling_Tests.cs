using System.Data;
using KnightMoves.SqlObjects.SqlCode.TSQL;
using Xunit;

namespace KnightMoves.SqlObjects.Tests.TSql
{
    public class TSQLFuncCeiling_Tests
    {
        [Fact]
        public void SQL_Returns_Ceiling_With_Default()
        {
            // ARRANGE
            var func = new TSQLFuncCeiling();

            // ACTION
            var sql = func.SQL();

            var expected = "CEILING(0)";

            // ASSERT
            Assert.Equal(expected, sql);
            TestHelper.Assert.SerializationWorks(expected, func);
        }

        [Fact]
        public void SQL_Returns_Ceiling_With_Literal()
        {
            // ARRANGE
            var func = new TSQLFuncCeiling();

            var paramValue = new TSQLLiteral 
            { 
                DataType = new TSQLDataType(SqlDbType.Decimal), 
                Value = "99.9" 
            };

            func.SetParameterValue(TSQLFuncCeilingParams.numericExpression, paramValue);

            // ACTION 
            var sql = func.SQL();

            var expected = "CEILING(99.9)";

            // ASSERT
            Assert.Equal(expected, sql);
            TestHelper.Assert.SerializationWorks(expected, func);
        }

        [Fact]
        public void SQL_Returns_Ceiling_With_Column()
        {
            // ASSERT
            var func = new TSQLFuncCeiling();

            var paramValue = new TSQLColumn 
            { 
                DataType = new TSQLDataType(SqlDbType.Decimal), 
                ColumnName = "NumericColumnName" 
            };

            func.SetParameterValue(TSQLFuncCeilingParams.numericExpression, paramValue);

            // ACTION
            var sql = func.SQL();

            var expected = "CEILING([NumericColumnName])";

            // ASSERT
            Assert.Equal(expected, sql);
            TestHelper.Assert.SerializationWorks(expected, func);
        }
    }
}
