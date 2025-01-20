using KnightMoves.SqlObjects.SqlCode.TSQL;
using System.Data;
using Xunit;

namespace KnightMoves.SqlObjects.Tests.TSql;

public class TSQLColumn_Tests
{
    [Fact]
    public void SQL_Method_Returns_ColumnName()
    {
        // ARRANGE
        var col = new TSQLColumn
        {
            DataType = new TSQLDataType(SqlDbType.Int),
            ColumnName = "ColumnName"
        };

        // ACTION 
        var sql = col.SQL();

        var expected = "[ColumnName]";

        // ASSERT
        Assert.Equal(expected, sql);
        TestHelper.Assert.SerializationWorks(expected, col);
    }

    [Fact]
    public void SQL_Method_Returns_Fully_Qualified_ColumnName()
    {
        // ARRANGE
        var col = new TSQLColumn
        {
            DataType = new TSQLDataType(SqlDbType.Int),
            Schema = "dbo",
            TableName = "TableName",
            ColumnName = "ColumnName"
        };

        // ACTION
        var sql = col.SQL();

        var expected = "[dbo].[TableName].[ColumnName]";

        // ASSERT
        Assert.Equal(expected, sql);
        TestHelper.Assert.SerializationWorks(expected, col);
    }
}

