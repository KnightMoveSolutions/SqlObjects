using System.Data;
using KnightMoves.SqlObjects.SqlCode.TSQL;
using Xunit;

namespace KnightMoves.SqlObjects.Tests.TSql
{
    public class TSQLLikeCondition_Tests
    {
        [Fact]
        public void SQL_Returns_Like_With_Specified_Pattern()
        {
            // ARRANGE
            var like = new TSQLLikeCondition
            {
                Pattern = "%pattern%"
            };

            like.Children.Add(
                new TSQLColumn 
                { 
                    DataType = new TSQLDataType(SqlDbType.Int), 
                    ColumnName = "ColumnName" 
                }
            );

            // ACTION
            var sql = like.SQL();

            var expected = "[ColumnName] LIKE '%pattern%'";

            // ASSERT
            Assert.Equal(expected, sql);
            TestHelper.Assert.SerializationWorks(expected, like);
        }
    }
}
