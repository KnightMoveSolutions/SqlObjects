using System;
using Xunit;

namespace KnightMoves.SqlObjects.Tests.TSql.Fluent
{
    public class FluentApi_Like_Tests
    {
        private readonly string NL = Environment.NewLine;

        [Fact]
        public void Select_Star_From_Where_Column_Like_Pattern()
        {
            // ARRANGE
            var sqlObj =
                TSQL
                  .SELECT()
                    .STAR()
                  .FROM("MyTable")
                  .WHERE("t", "MyColumn").LIKE("%some?pattern%");

            // ACTION
            var sql = sqlObj.Build();

            var expected = $"  SELECT{NL}" +
                           $"   *{NL}" +
                           $"  FROM [MyTable]{NL}" +
                           $"  WHERE 1=1{NL}" +
                           $"   AND [t].[MyColumn] LIKE '%some?pattern%'{NL}";

            // ASSERT
            Assert.Equal(expected, sql);
            TestHelper.Assert.SerializationWorks(expected, sqlObj);
        }

        [Fact]
        public void Select_Star_From_Where_Column_Like_Parameter()
        {
            // ARRANGE
            var sqlObj =
                TSQL
                  .SELECT()
                    .STAR()
                  .FROM("MyTable")
                  .WHERE("t", "MyColumn").LIKE("@parameter");

            // ACTION
            var sql = sqlObj.Build();

            var expected = $"  SELECT{NL}" +
                           $"   *{NL}" +
                           $"  FROM [MyTable]{NL}" +
                           $"  WHERE 1=1{NL}" +
                           $"   AND [t].[MyColumn] LIKE @parameter{NL}";

            // ASSERT
            Assert.Equal(expected, sql);
            TestHelper.Assert.SerializationWorks(expected, sqlObj);
        }

        [Fact]
        public void Select_Star_From_Where_Column_Like_Parameter_And_Basic_Condition()
        {
            // ARRANGE
            var sqlObj =
                TSQL
                  .SELECT()
                    .STAR()
                  .FROM("MyTable")
                  .WHERE("t", "MyColumn").LIKE("@parameter").AND()
                    .COLUMN("AnotherColumn").IsGreaterThan(100);

            // ACTION
            var sql = sqlObj.Build();

            var expected = $"  SELECT{NL}" +
                           $"   *{NL}" +
                           $"  FROM [MyTable]{NL}" +
                           $"  WHERE 1=1{NL}" +
                           $"   AND [t].[MyColumn] LIKE @parameter{NL}" +
                           $"   AND [AnotherColumn] > 100{NL}";

            // ASSERT
            Assert.Equal(expected, sql);
            TestHelper.Assert.SerializationWorks(expected, sqlObj);
        }
    }
}
