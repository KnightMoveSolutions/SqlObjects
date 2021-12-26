using KnightMoves.SqlObjects.SqlCode;
using Xunit;

namespace KnightMoves.SqlObjects.Tests.TSql.Fluent
{
    public class FluentApi_With_Tests
    {

        [Fact]
        public void Select_Column_WithId_From_Table()
        {
            // ARRANGE / ACTION 
            var sqlObj =
                TSQL
                  .SELECT()
                    .COLUMN("FirstColumn")
                    .COLUMN("MyColumn").WithId("12345")
                  .FROM("Customers");

            // ASSERT
            var lookup = sqlObj.Root.FindById("12345");

            Assert.NotNull(lookup);
            Assert.Equal("12345", lookup.Id);
        }

        [Fact]
        public void Select_Column_WithName_From_Table()
        {
            // ARRANGE / ACTION 
            var sqlObj =
                TSQL
                  .SELECT()
                    .COLUMN("FirstColumn")
                    .COLUMN("MyColumn").WithName("ColSpec")
                  .FROM("Customers");

            // ASSERT
            SqlStatement lookup = null;

            sqlObj.Root.ProcessTree(n =>
            {
                if ((n as SqlStatement).Name == "ColSpec")
                    lookup = n as SqlStatement;

                return true;
            });

            Assert.NotNull(lookup);
            Assert.Equal("ColSpec", lookup.Name);
        }
    }
}
