using System;
using System.Data;
using KnightMoves.SqlObjects.SqlCode;
using KnightMoves.SqlObjects.SqlCode.TSQL;
using Xunit;

namespace KnightMoves.SqlObjects.Tests.TSql
{
    public class TSQLParameter_Tests
    {
        [Fact]
        public void SQL_Returns_Literal_Value()
        {
            // ARRANGE
            var p = new TSQLParameter 
            { 
                ParameterDataType = new TSQLDataType(SqlDbType.Int),
                Value = new TSQLLiteral { DataType = new TSQLDataType(SqlDbType.Int), Value = "9999" }
            };
            
            // ACTION
            var sql = p.SQL();

            var expected = "9999";

            // ASSERT
            Assert.Equal(expected, sql);
            TestHelper.Assert.SerializationWorks(expected, p);
        }

        [Fact]
        public void SQL_Returns_Column_Value()
        {
            // ARRANGE
            var p = new TSQLParameter 
            { 
                ParameterDataType = new TSQLDataType(SqlDbType.Int),
                Value = new TSQLColumn { DataType = new TSQLDataType(SqlDbType.Int), ColumnName = "ColumnName" }
            };

            // ACTION
            var sql = p.SQL();

            var expected = "[ColumnName]";

            // ASSERT
            Assert.Equal(expected, sql);
            TestHelper.Assert.SerializationWorks(expected, p);
        }

        [Fact]
        public void SQL_Returns_Fully_Qualified_Column_Value()
        {
            // ARRANGE
            var p = new TSQLParameter 
            { 
                ParameterDataType = new TSQLDataType(SqlDbType.Int),
                Value = new TSQLColumn 
                { 
                    DataType = new TSQLDataType(SqlDbType.Int), 
                    Schema = "dbo", 
                    TableName = "TableName", 
                    ColumnName = "ColumnName" 
                }
            };

            // ACTION
            var sql = p.SQL();

            var expected = "[dbo].[TableName].[ColumnName]";

            // ASSERT
            Assert.Equal(expected, sql);
            TestHelper.Assert.SerializationWorks(expected, p);
        }

        [Fact]
        public void SQL_Returns_Calculation_Value()
        {
            // ARRANGE
            var p = new TSQLParameter 
            { 
                ParameterDataType = new TSQLDataType(SqlDbType.Int),
                Value = new TSQLCalculation
                {
                    DataType = new TSQLDataType(SqlDbType.Int),
                    LeftOperand = new TSQLLiteral { DataType = new TSQLDataType(SqlDbType.Int), Value = "9999" },
                    Operator = SqlArithmeticOperators.Plus,
                    RightOperand = new TSQLLiteral { DataType = new TSQLDataType(SqlDbType.Int), Value = "1234" }
                }
            };

            // ACTION
            var sql = p.SQL();

            var expected = "(9999 + 1234)";

            // ASSERT
            Assert.Equal(expected, sql);
            TestHelper.Assert.SerializationWorks(expected, p);
        }

        [Fact]
        public void Value_Throws_Exception_On_Missing_DataType()
        {
            var p = new TSQLParameter();

            Assert.Throws<InvalidOperationException>(() => { p.Value = new TSQLLiteral(); });
        }

        [Fact]
        public void ParameterDataType_Throws_Exception_On_Mismatched_DataType()
        {
            var p = new TSQLParameter { ParameterDataType = new TSQLDataType(SqlDbType.Int) };

            // This test should throw an exception because the DataType of the SqlQueryExpression object being set 
            // for the Value property of the parameter does not match the ParameterDataType property of the parameter.
            Assert.Throws<InvalidOperationException>(() => { p.Value = new TSQLLiteral { DataType = new TSQLDataType(SqlDbType.Text), Value = "9999" }; });
        }

        [Fact]
        public void EnumType_Throws_Exception_On_NonEnumType_Value()
        {
            var p = new TSQLParameter();

            // This test should throw an exception because a non-enum type name was provided for the EnumType property.
            Assert.Throws<ArgumentException>(() => { p.EnumType = "ThisShouldBreak"; });
        }

        [Fact]
        public void EnumType_Set_Successful()
        {
            var p = new TSQLParameter();
            p.EnumType = typeof(DateParts).AssemblyQualifiedName;
        }

        [Fact]
        public void Value_Set_Throws_Exception_On_Non_Literal_Value()
        {
            var p = new TSQLParameter { ParameterDataType = new TSQLDataType(SqlDbType.VarChar), EnumType = typeof(DateParts).AssemblyQualifiedName };

            // Should throw exception because it's not a SqlLiteral object
            Assert.Throws<InvalidOperationException>(() => { p.Value = new TSQLColumn { DataType = new TSQLDataType(SqlDbType.VarChar), ColumnName = "ColumnName" }; });
        }

        [Fact]
        public void Value_Set_Throws_Exception_On_Invalid_Enum_Value()
        {
            var p = new TSQLParameter { ParameterDataType = new TSQLDataType(SqlDbType.VarChar), EnumType = typeof(DateParts).AssemblyQualifiedName };

            // Should throw exception because the value is not a valid DateParts enum option
            Assert.Throws<InvalidOperationException>(() => { p.Value = new TSQLLiteral { DataType = new TSQLDataType(SqlDbType.VarChar), Value = "blah" }; });
        }

        [Fact]
        public void Value_Set_Successful()
        {
            // ARRANGE
            var p = new TSQLParameter 
            { 
                ParameterDataType = new TSQLDataType(SqlDbType.VarChar), 
                EnumType = typeof(DateParts).AssemblyQualifiedName,
                Value = new TSQLLiteral { DataType = new TSQLDataType(SqlDbType.VarChar), Value = "Day" }
            };

            // ACTION
            var value = p.Value.ToString();

            var expected = "'Day'";

            // ASSERT
            Assert.Equal(expected, value);
        }
    }
}
