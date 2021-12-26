using System.Data;
using KnightMoves.SqlObjects.SqlCode.TSQL;
using Xunit;

namespace KnightMoves.SqlObjects.Tests.TSql
{
    public class TSQLFuncDateAdd_Tests
    {
        [Fact]
        public void SQL_Returns_DateAdd_With_Defaults()
        {
            // ARRANGE
            var func = new TSQLFuncDateAdd();

            // ACTION
            var sql = func.SQL();

            var expected = "DATEADD(Day, 0, GETDATE())";

            // ASSERT
            Assert.Equal(expected, sql);
            TestHelper.Assert.SerializationWorks(expected, func);
        }

        [Fact]
        public void SQL_Returns_DateAdd_With_Literals()
        {
            // ARRANGE
            var func = new TSQLFuncDateAdd();

            var datePart = new TSQLLiteral { DataType = new TSQLDataType(SqlDbType.Variant), Value = "Month" };
            var increment = new TSQLLiteral { DataType = new TSQLDataType(SqlDbType.Int), Value = "1" };
            var dateExpression = new TSQLLiteral { DataType = new TSQLDataType(SqlDbType.DateTime), Value = "01/01/2012" };

            func.SetParameterValue(TSQLFuncDateAddParams.datePart, datePart);
            func.SetParameterValue(TSQLFuncDateAddParams.increment, increment);
            func.SetParameterValue(TSQLFuncDateAddParams.dateExpression, dateExpression);

            // ACTION
            var sql = func.SQL();

            var expected = "DATEADD(Month, 1, '01/01/2012')";

            // ASSERT
            Assert.Equal(expected, sql);
            TestHelper.Assert.SerializationWorks(expected, func);
        }

        [Fact]
        public void SQL_Returns_DateAdd_With_Column()
        {
            // ARRANGE
            var func = new TSQLFuncDateAdd();

            var datePart = new TSQLLiteral { DataType = new TSQLDataType(SqlDbType.Variant), Value = "Month" };
            var increment = new TSQLLiteral { DataType = new TSQLDataType(SqlDbType.Int), Value = "1" };
            var dateExpression = new TSQLColumn { DataType = new TSQLDataType(SqlDbType.DateTime), ColumnName = "DateColumnName" };

            func.SetParameterValue(TSQLFuncDateAddParams.datePart, datePart);
            func.SetParameterValue(TSQLFuncDateAddParams.increment, increment);
            func.SetParameterValue(TSQLFuncDateAddParams.dateExpression, dateExpression);

            // ACTION
            var sql = func.SQL();

            var expected = "DATEADD(Month, 1, [DateColumnName])";

            // ASSERT
            Assert.Equal(expected, sql);
            TestHelper.Assert.SerializationWorks(expected, func);
        }
    }
}
