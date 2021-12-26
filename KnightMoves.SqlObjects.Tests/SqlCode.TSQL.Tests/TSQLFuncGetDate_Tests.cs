using KnightMoves.SqlObjects.SqlCode.TSQL;
using Xunit;

namespace KnightMoves.SqlObjects.Tests.TSql
{
    public class TSQLFuncGetDate_Tests
    {
        [Fact]
        public void SQL_Returns_GetDate()
        {
            // ARRANGE
            var func = new TSQLFuncGetDate();

            // ACTION
            var sql = func.SQL();

            var expected = "GETDATE()";

            Assert.Equal(expected, sql);
            TestHelper.Assert.SerializationWorks(expected, func);
        }
    }
}
