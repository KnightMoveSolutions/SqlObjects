using KnightMoves.SqlObjects.SqlCode.TSQL;
using Xunit;

namespace KnightMoves.SqlObjects.Tests.TSql
{
    public class TSQL_Tests
    {
        [Fact]
        public void SELECT_Returns_TSQLSelect_Object()
        {
            // ARRANGE / ACTION
            var select = TSQL.SELECT();

            // ASSERT
            Assert.IsType<TSQLSelect>(select);
        }

        [Fact]
        public void SELECT_Build_Returns_Select_Star()
        {
            // ARRANGE 
            var select = TSQL.SELECT();

            // ACTION
            var sql = select.Build();

            var expected = "  SELECT * ";

            // ASSERT
            Assert.Equal(expected, sql);
            TestHelper.Assert.SerializationWorks(expected, select);
        }
    }
}

