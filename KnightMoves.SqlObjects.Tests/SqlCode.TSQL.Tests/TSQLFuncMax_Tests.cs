using System.Data;
using KnightMoves.SqlObjects.SqlCode.TSQL;
using Xunit;

namespace KnightMoves.SqlObjects.Tests.TSql;

public class TSQLFuncMax_Tests
{
    [Fact]
    public void SQL_Returns_Max_With_Default()
    {
        // ARRANGE
        var func = new TSQLFuncMax();

        // ACTION
        var sql = func.SQL();

        var expected = "MAX(0)";

        // ASSERT
        Assert.Equal(expected, sql);
        TestHelper.Assert.SerializationWorks(expected, func);
    }

    [Fact]
    public void SQL_Returns_Max_With_Column()
    {
        // ARRANGE
        var func = new TSQLFuncMax();

        var aggregateExpression = new TSQLColumn { DataType = new TSQLDataType(SqlDbType.Variant), ColumnName = "ColumnName" };

        func.SetParameterValue(TSQLFuncMaxParams.aggregateExpression, aggregateExpression);

        // ACTION
        var sql = func.SQL();

        var expected = "MAX([ColumnName])";

        // ASSERT
        Assert.Equal(expected, sql);
        TestHelper.Assert.SerializationWorks(expected, func);
    }
}
