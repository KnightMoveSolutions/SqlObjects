using System.Data;
using KnightMoves.SqlObjects.SqlCode;
using KnightMoves.SqlObjects.SqlCode.TSQL;
using Xunit;

namespace KnightMoves.SqlObjects.Tests.TSql;

public class TSQLBasicCondition_Tests
{
    [Fact]
    public void SQL_Method_Returns_Expected_SQL()
    {
        // ARRANGE
        var basicCond = new TSQLBasicCondition { Operator = SqlComparisonOperators.IsLessThan };

        basicCond.Children.Add(
            new TSQLColumn
            {
                DataType = new TSQLDataType(SqlDbType.Int),
                ColumnName = "ColumnName"
            }
        );

        basicCond.Children.Add(
            new TSQLLiteral 
            { 
                DataType = new TSQLDataType(SqlDbType.Int), 
                Value = "9999" 
            }
        );

        // ACTION
        var sql = basicCond.SQL();

        var expected = "[ColumnName] < 9999";

        // ASSERT
        Assert.Equal(expected, sql);
        TestHelper.Assert.SerializationWorks(expected, basicCond);
    }
}
