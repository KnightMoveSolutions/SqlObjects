using System;
using System.Data;
using KnightMoves.SqlObjects.SqlCode.TSQL;
using Xunit;

namespace KnightMoves.SqlObjects.Tests.TSql
{
    public class TSQLInListCondition_Tests
    {
        [Fact]
        public void SQL_Throws_Exception_On_Missing_InList_Values()
        {
            var inList = new TSQLInListCondition();

            // Should throw an exception because nothing has been added to the InList collection.
            Assert.Throws<InvalidOperationException>(() => { inList.SQL(); });
        }

        [Fact]
        public void SQL_Returns_InList_With_Unquoted_Values()
        {
            // ARRANGE
            var inList = new TSQLInListCondition();

            inList.Children.Add(
                new TSQLColumn 
                { 
                    DataType = new TSQLDataType(SqlDbType.Int), 
                    ColumnName = "ColumnName" 
                }
            );

            inList.InList.Add(new TSQLLiteral { DataType = new TSQLDataType(SqlDbType.Int), Value = "1" });
            inList.InList.Add(new TSQLLiteral { DataType = new TSQLDataType(SqlDbType.Int), Value = "2" });
            inList.InList.Add(new TSQLLiteral { DataType = new TSQLDataType(SqlDbType.Int), Value = "3" });
            inList.InList.Add(new TSQLLiteral { DataType = new TSQLDataType(SqlDbType.Int), Value = "4" });
            inList.InList.Add(new TSQLLiteral { DataType = new TSQLDataType(SqlDbType.Int), Value = "5" });

            // ACTION
            var sql = inList.SQL();

            var expected = "[ColumnName] IN (1,2,3,4,5)";

            // ASSERT
            Assert.Equal(expected, sql);
            TestHelper.Assert.SerializationWorks(expected, inList);
        }

        [Fact]
        public void SQL_Returns_InList_With_Quoted_Values()
        {
            // ARRANGE
            var inList = new TSQLInListCondition();

            inList.Children.Add(
                new TSQLColumn 
                { 
                    DataType = new TSQLDataType(SqlDbType.VarChar), 
                    ColumnName = "ColumnName" 
                }
            );

            inList.InList.Add(new TSQLLiteral { DataType = new TSQLDataType(SqlDbType.VarChar), Value = "A" });
            inList.InList.Add(new TSQLLiteral { DataType = new TSQLDataType(SqlDbType.VarChar), Value = "B" });
            inList.InList.Add(new TSQLLiteral { DataType = new TSQLDataType(SqlDbType.VarChar), Value = "C" });
            inList.InList.Add(new TSQLLiteral { DataType = new TSQLDataType(SqlDbType.VarChar), Value = "D" });
            inList.InList.Add(new TSQLLiteral { DataType = new TSQLDataType(SqlDbType.VarChar), Value = "E" });

            // ACTION
            var sql = inList.SQL();

            var expected = "[ColumnName] IN ('A','B','C','D','E')";

            // ASSERT
            Assert.Equal(expected, sql);
            TestHelper.Assert.SerializationWorks(expected, inList);
        }
    }
}
