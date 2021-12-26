using System.Data;
using KnightMoves.SqlObjects.SqlCode.TSQL;
using Xunit;

namespace KnightMoves.SqlObjects.Tests.TSql
{
    public class TSQLFuncDatePart_Tests
    {
        [Fact]
        public void SQL_Returns_DatePart_With_Defaults()
        {
            // ARRANGE
            var func = new TSQLFuncDatePart();

            // ACTION
            var sql = func.SQL();

            var expected = "DATEPART(Day, GETDATE())";

            // ASSERT
            Assert.Equal(expected, sql);
            TestHelper.Assert.SerializationWorks(expected, func);
        }

        [Fact]
        public void SQL_Returns_DatePart_With_Literals()
        {
            // ARRANGE
            var func = new TSQLFuncDatePart();

            var datePart = new TSQLLiteral { DataType = new TSQLDataType(SqlDbType.Variant), Value = "Month" };
            var dateExpression = new TSQLLiteral { DataType = new TSQLDataType(SqlDbType.DateTime), Value = "01/01/2012" };

            func.SetParameterValue(TSQLFuncDatePartParams.datePart, datePart);
            func.SetParameterValue(TSQLFuncDatePartParams.dateExpression, dateExpression);

            // ACTION
            var sql = func.SQL();

            var expected = "DATEPART(Month, '01/01/2012')";

            // ASSERT
            Assert.Equal(expected, sql);
            TestHelper.Assert.SerializationWorks(expected, func);
        }

        [Fact]
        public void SQL_Returns_DatePart_With_Columns()
        {
            // ARRANGE
            var func = new TSQLFuncDatePart();

            var datePart = new TSQLLiteral { DataType = new TSQLDataType(SqlDbType.Variant), Value = "Month" };
            var dateExpression = new TSQLColumn { DataType = new TSQLDataType(SqlDbType.DateTime), ColumnName = "DateColumnName" };

            func.SetParameterValue(TSQLFuncDatePartParams.datePart, datePart);
            func.SetParameterValue(TSQLFuncDatePartParams.dateExpression, dateExpression);

            // ACTION
            var sql = func.SQL();

            var expected = "DATEPART(Month, [DateColumnName])";

            // ASSERT
            Assert.Equal(expected, sql);
            TestHelper.Assert.SerializationWorks(expected, func);
        }
    }
}
