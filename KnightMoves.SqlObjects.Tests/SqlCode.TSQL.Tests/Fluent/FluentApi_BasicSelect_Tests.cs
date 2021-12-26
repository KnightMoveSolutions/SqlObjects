using System;
using Xunit;

namespace KnightMoves.SqlObjects.Tests.TSql.Fluent
{
    public class FluentApi_BasicSelect_Tests
    {
        private readonly string NL = Environment.NewLine;

        [Fact]
        public void Select_Star_From_Table()
        {
            // ARRANGE 
            var sqlObj =
                TSQL
                  .SELECT()
                    .STAR()
                  .FROM("Customers");

            // ACTION
            var sql = sqlObj.Build();

            var expected = $"  SELECT{NL}" +
                           $"   *{NL}" +
                           $"  FROM [Customers]{NL}";

            // ASSERT
            Assert.Equal(expected, sql);
            TestHelper.Assert.SerializationWorks(expected, sqlObj);
        }
    }
}
