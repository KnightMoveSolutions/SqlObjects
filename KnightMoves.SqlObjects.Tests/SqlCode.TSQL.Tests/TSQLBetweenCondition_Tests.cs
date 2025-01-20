using System.Data;
using KnightMoves.SqlObjects.SqlCode.TSQL;
using Xunit;

namespace KnightMoves.SqlObjects.Tests.TSql;

public class TSQLBetweenCondition_Tests
{
    [Fact]
    public void SQL_Method_Returns_Expected_SQL()
    {
        // ARRANGE
        var between = new TSQLBetweenCondition
        {
            LeftOperand = new TSQLColumn { DataType = new TSQLDataType(SqlDbType.Int), ColumnName = "ColumnName" },
            StartVal = new TSQLLiteral { DataType = new TSQLDataType(SqlDbType.Int), Value = "1" },
            EndVal = new TSQLLiteral { DataType = new TSQLDataType(SqlDbType.Int), Value = "100" }
        };

        // ACTION
        var sql = between.SQL();

        var expected = "[ColumnName] BETWEEN 1 AND 100";

        // ASSERT
        Assert.Equal(expected, sql);
        TestHelper.Assert.SerializationWorks(expected, between);
    }
}
