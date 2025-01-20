using System.Data;
using KnightMoves.SqlObjects.SqlCode.TSQL;
using Xunit;

namespace KnightMoves.SqlObjects.Tests.TSql;

public class TSQLFuncDateDiff_Tests
{
    [Fact]
    public void SQL_Returns_DateDiff_With_Defaults()
    {
        // ARRANGE
        var func = new TSQLFuncDateDiff();

        // ACTION
        var sql = func.SQL();

        var expected = "DATEDIFF(Day, GETDATE(), GETDATE())";

        // ASSERT
        Assert.Equal(expected, sql);
        TestHelper.Assert.SerializationWorks(expected, func);
    }

    [Fact]
    public void SQL_Returns_DateDiff_With_Literals()
    {
        // ARRANGE
        var func = new TSQLFuncDateDiff();

        var datePart = new TSQLLiteral { DataType = new TSQLDataType(SqlDbType.Variant), Value = "Month" };
        var startDate = new TSQLLiteral { DataType = new TSQLDataType(SqlDbType.DateTime), Value = "01/01/2012" };
        var endDate = new TSQLLiteral { DataType = new TSQLDataType(SqlDbType.DateTime), Value = "12/31/2012" };

        func.SetParameterValue(TSQLFuncDateDiffParams.datePart, datePart);
        func.SetParameterValue(TSQLFuncDateDiffParams.startDate, startDate);
        func.SetParameterValue(TSQLFuncDateDiffParams.endDate, endDate);

        // ACTION
        var sql = func.SQL();

        var expected = "DATEDIFF(Month, '01/01/2012', '12/31/2012')";

        // ASSERT
        Assert.Equal(expected, sql);
        TestHelper.Assert.SerializationWorks(expected, func);
    }

    [Fact]
    public void SQL_Returns_DateDiff_With_Columns()
    {
        // ARRANGE
        var func = new TSQLFuncDateDiff();

        var datePart = new TSQLLiteral { DataType = new TSQLDataType(SqlDbType.Variant), Value = "Month" };
        var startDate = new TSQLColumn { DataType = new TSQLDataType(SqlDbType.DateTime), ColumnName = "DateColumnA" };
        var endDate = new TSQLColumn { DataType = new TSQLDataType(SqlDbType.DateTime), ColumnName = "DateColumnB" };

        func.SetParameterValue(TSQLFuncDateDiffParams.datePart, datePart);
        func.SetParameterValue(TSQLFuncDateDiffParams.startDate, startDate);
        func.SetParameterValue(TSQLFuncDateDiffParams.endDate, endDate);

        // ACTION
        var sql = func.SQL();

        var expected = "DATEDIFF(Month, [DateColumnA], [DateColumnB])";

        // ASSERT
        Assert.Equal(expected, sql);
        TestHelper.Assert.SerializationWorks(expected, func);
    }
}
