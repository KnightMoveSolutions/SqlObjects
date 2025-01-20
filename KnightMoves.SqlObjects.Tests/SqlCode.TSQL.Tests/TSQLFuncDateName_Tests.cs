using System.Data;
using KnightMoves.SqlObjects.SqlCode.TSQL;
using Xunit;

namespace KnightMoves.SqlObjects.Tests.TSql;

public class TSQLFuncDateName_Tests
{
    [Fact]
    public void SQL_Returns_DateName_With_Defaults()
    {
        // ARRANGE
        var func = new TSQLFuncDateName();

        // ACTION
        var sql = func.SQL();

        var expected = "DATENAME(Day, GETDATE())";

        // ASSERT
        Assert.Equal(expected, sql);
        TestHelper.Assert.SerializationWorks(expected, func);
    }

    [Fact]
    public void SQL_Returns_DateName_With_Literals()
    {
        // ARRANGE
        var func = new TSQLFuncDateName();

        var datePart = new TSQLLiteral { DataType = new TSQLDataType(SqlDbType.Variant), Value = "Month" };
        var dateExpression = new TSQLLiteral { DataType = new TSQLDataType(SqlDbType.DateTime), Value = "01/01/2012" };

        func.SetParameterValue(TSQLFuncDateNameParams.datePart, datePart);
        func.SetParameterValue(TSQLFuncDateNameParams.dateExpression, dateExpression);

        // ACTION
        var sql = func.SQL();

        var expected = "DATENAME(Month, '01/01/2012')";

        // ASSERT
        Assert.Equal(expected, sql);
        TestHelper.Assert.SerializationWorks(expected, func);
    }

    [Fact]
    public void SQL_Returns_DateName_With_Columns()
    {
        // ARRANGE
        var func = new TSQLFuncDateName();

        var datePart = new TSQLLiteral { DataType = new TSQLDataType(SqlDbType.Variant), Value = "Month" };
        var dateExpression = new TSQLColumn { DataType = new TSQLDataType(SqlDbType.DateTime), ColumnName = "DateColumnName" };

        func.SetParameterValue(TSQLFuncDateNameParams.datePart, datePart);
        func.SetParameterValue(TSQLFuncDateNameParams.dateExpression, dateExpression);

        // ACTION
        var sql = func.SQL();

        var expected = "DATENAME(Month, [DateColumnName])";

        // ASSERT
        Assert.Equal(expected, sql);
        TestHelper.Assert.SerializationWorks(expected, func);
    }
}
