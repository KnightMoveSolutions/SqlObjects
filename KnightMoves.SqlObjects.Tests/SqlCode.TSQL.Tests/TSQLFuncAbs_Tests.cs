using System.Data;
using KnightMoves.SqlObjects.SqlCode.TSQL;
using Xunit;

namespace KnightMoves.SqlObjects.Tests.TSql
{
    public class TSQLFuncAbs_Tests
    {
        [Fact]
        public void SQL_Returns_ABS_Function_With_Zero_Default_Arg()
        {
            // ARRANGE
            TSQLFuncAbs func = new TSQLFuncAbs();

            // ACTION
            var sql = func.SQL();

            var expected = "ABS(0)";

            // ASSERT
            Assert.Equal(expected, sql);
        }

        [Fact]
        public void SQL_Returns_ABS_Function_With_Literal_Parameter()
        {
            // ARRANGE
            var func = new TSQLFuncAbs();

            var paramValue = new TSQLLiteral 
            { 
                DataType = new TSQLDataType(SqlDbType.Decimal), 
                Value = "99.9" 
            };

            func.SetParameterValue(TSQLFuncAbsParams.numericExpression, paramValue);

            // ACTION
            var sql = func.SQL();

            var expected = "ABS(99.9)";

            // ASSERT
            Assert.Equal(expected, sql);
            TestHelper.Assert.SerializationWorks(expected, func);
        }

        [Fact]
        public void SQL_Returns_ABS_Function_With_Column_Parameter()
        {
            TSQLFuncAbs func = new TSQLFuncAbs();

            func.SetParameterValue(TSQLFuncAbsParams.numericExpression, new TSQLColumn { DataType = new TSQLDataType(SqlDbType.Decimal), ColumnName = "NumericColumnName" });

            Assert.Equal("ABS([NumericColumnName])", func.SQL());
        }
    }
}
