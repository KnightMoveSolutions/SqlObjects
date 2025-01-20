using System.Data;
using KnightMoves.SqlObjects.SqlCode.TSQL;
using Xunit;

namespace KnightMoves.SqlObjects.Tests.TSql;

public class TSQLFuncYear_Tests
{
    [Fact]
    public void SQL_Returns_Year_With_Default()
    {
        // ARRANGE
        var func = new TSQLFuncYear();

        // ACTION
        var sql = func.SQL();

        var expected = "YEAR(GETDATE())";

        // ASSERT
        Assert.Equal(expected, sql);
        TestHelper.Assert.SerializationWorks(expected, func);
    }

    [Fact]
    public void SQL_Returns_Year_With_Literal()
    {
        // ARRANGE
        var func = new TSQLFuncYear();

        var dateExpression = new TSQLLiteral { DataType = new TSQLDataType(SqlDbType.DateTime), Value = "01/15/2012" };

        func.SetParameterValue(TSQLFuncYearParams.dateExpression, dateExpression);

        // ACTION
        var sql = func.SQL();

        var expected = "YEAR('01/15/2012')";

        // ASSERT
        Assert.Equal(expected, sql);
        TestHelper.Assert.SerializationWorks(expected, func);
    }

    [Fact]
    public void SQL_Returns_Year_With_Column()
    {
        // ARRANGE
        var func = new TSQLFuncYear();

        var dateExpression = new TSQLColumn { DataType = new TSQLDataType(SqlDbType.DateTime), ColumnName = "ColumnName" };

        func.SetParameterValue(TSQLFuncYearParams.dateExpression, dateExpression);

        // ACTION
        var sql = func.SQL();

        var expected = "YEAR([ColumnName])";

        // ASSERT
        Assert.Equal(expected, sql);
        TestHelper.Assert.SerializationWorks(expected, func);
    }

    [Fact]
    public void SQL_Returns_Year_With_Schema_And_Column()
    {
        // ARRANGE
        var func = new TSQLFuncYear();

        var dateExpression = new TSQLColumn { DataType = new TSQLDataType(SqlDbType.DateTime), Schema = "dbo", TableName = "TableName", ColumnName = "ColumnName" };

        func.SetParameterValue(TSQLFuncYearParams.dateExpression, dateExpression);

        // ACTION
        var sql = func.SQL();

        var expected = "YEAR([dbo].[TableName].[ColumnName])";

        // ASSERT
        Assert.Equal(expected, sql);
        TestHelper.Assert.SerializationWorks(expected, func);
    }
}
