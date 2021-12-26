using System;
using System.Data;
using KnightMoves.SqlObjects.SqlCode;
using KnightMoves.SqlObjects.SqlCode.TSQL;
using Xunit;

namespace KnightMoves.SqlObjects.Tests.TSql
{
    public class TSQLCaseWhen_Tests
    {
        [Fact]
        public void SQL_Throws_Exception_On_Missing_Conditions()
        {
            var when = new TSQLCaseWhen();
            
            // should throw an exception because there are no conditions
            Assert.Throws<InvalidOperationException>(() => { when.SQL(); });
        }

        [Fact]
        public void SQL_Throws_Exception_On_Mismatching_DataTypes()
        {
            var when = new TSQLCaseWhen { DataType = new TSQLDataType(SqlDbType.Text) };

            var cond = new TSQLBasicCondition { Operator = SqlComparisonOperators.IsLessThan };

            cond.Children.Add(
                new TSQLColumn { DataType = new TSQLDataType(SqlDbType.Int), ColumnName = "ColumnName" }
            );

            cond.Children.Add(
                new TSQLLiteral { DataType = new TSQLDataType(SqlDbType.Int), Value = "9999" }
            );

            when.Children.Add(cond);

            // Should throw an exception because the DataType of the caseObj does not match the DataType of the when obj
            Assert.Throws<InvalidOperationException>(() => { when.SQL(); });
        }

        [Fact]
        public void SQL_Throws_Exception_On_Missing_Result_Expression()
        {
            var when = new TSQLCaseWhen { DataType = new TSQLDataType(SqlDbType.Int) };

            var cond = new TSQLBasicCondition { Operator = SqlComparisonOperators.IsLessThan };

            cond.Children.Add(
                new TSQLColumn 
                { 
                    DataType = new TSQLDataType(SqlDbType.Int), 
                    ColumnName = "ColumnName" 
                }
            );

            cond.Children.Add(
                new TSQLLiteral 
                { 
                    DataType = new TSQLDataType(SqlDbType.Int), 
                    Value = "9999" 
                }
            );

            when.Children.Add(cond);

            // This test should throw an exception because there is no SQLQueryExpression added for the resulting value of the THEN clause but no exception was thrown so the Unit Test failed.
            Assert.Throws<InvalidOperationException>(() => { when.SQL(); });
        }

        [Fact]
        public void SQL_Returns_When_Clause()
        {
            // ARRANGE
            var when = new TSQLCaseWhen { DataType = new TSQLDataType(SqlDbType.Int) };

            var cond = new TSQLBasicCondition { Operator = SqlComparisonOperators.IsLessThan };

            cond.Children.Add(
                 new TSQLColumn 
                 { 
                     DataType = new TSQLDataType(SqlDbType.Int), 
                     ColumnName = "ColumnName" 
                 }
            );

            cond.Children.Add(
                new TSQLLiteral 
                { 
                    DataType = new TSQLDataType(SqlDbType.Int), 
                    Value = "9999" 
                }
            );

            when.Children.Add(cond);

            var result = new TSQLLiteral { DataType = new TSQLDataType(SqlDbType.Int), Value = "1" };

            when.Children.Add(result);

            // ACTION
            var sql = when.SQL();

            var expected = " WHEN [ColumnName] < 9999 THEN 1";

            // ASSERT
            Assert.Equal(expected, sql);
            TestHelper.Assert.SerializationWorks(expected, when);
        }

        [Fact]
        public void Test_TSQLCaseWhenWithTwoConditionsSQL()
        {
            var script = new TSQLScript();
            var caseObj = new TSQLCase { DataType = new TSQLDataType(SqlDbType.Int) };
            var when = new TSQLCaseWhen { DataType = new TSQLDataType(SqlDbType.Int) };

            var cond = new TSQLBasicCondition { Operator = SqlComparisonOperators.IsGreaterThan };

            cond.Children.Add(
                new TSQLColumn 
                { 
                    DataType = new TSQLDataType(SqlDbType.Int), 
                    ColumnName = "ColumnName" 
                }
            );

            cond.Children.Add(
                new TSQLLiteral 
                { 
                    DataType = new TSQLDataType(SqlDbType.Int), 
                    Value = "999" 
                }
            );

            when.Children.Add(cond);

            cond = new TSQLBasicCondition { Operator = SqlComparisonOperators.IsLessThan };

            cond.Children.Add(
                new TSQLColumn 
                { 
                    DataType = new TSQLDataType(SqlDbType.Int), 
                    ColumnName = "AnotherColumn" 
                }
            );

            cond.Children.Add(
                new TSQLLiteral 
                { 
                    DataType = new TSQLDataType(SqlDbType.Int), 
                    Value = "9999" 
                }
            );

            when.Children.Add(cond);

            var result = new TSQLLiteral { DataType = new TSQLDataType(SqlDbType.Int), Value = "1" };

            when.Children.Add(result);

            caseObj.Children.Add(when);

            script.Children.Add(caseObj);

            var expected = "  CASE" + Environment.NewLine +
                           "   WHEN [ColumnName] > 999 AND [AnotherColumn] < 9999 THEN 1" + Environment.NewLine +
                           "  END";

            var sql = script.SQL();

            Assert.Equal(expected, sql);
        }
    }
}
