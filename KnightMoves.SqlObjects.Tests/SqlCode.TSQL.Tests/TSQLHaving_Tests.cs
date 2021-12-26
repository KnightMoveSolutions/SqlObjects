using System;
using System.Data;
using KnightMoves.SqlObjects.SqlCode;
using KnightMoves.SqlObjects.SqlCode.TSQL;
using Xunit;

namespace KnightMoves.SqlObjects.Tests.TSql
{
    public class TSQLHaving_Tests
    {
        [Fact]
        public void SQL_Returns_Having_With_Default()
        {
            // ARRANGE
            var having = new TSQLHaving();

            having.Children.Clear();

            // ACTION
            var sql = having.SQL();

            var expected = " HAVING 1=1" + Environment.NewLine;

            // ASSERT
            Assert.Equal(expected, sql);
            TestHelper.Assert.SerializationWorks(expected, having);
        }

        [Fact]
        public void SQL_Returns_Having_With_BasicCondition()
        {
            // ARRANGE
            var having = new TSQLHaving();

            having.Children.Clear();

            var cond = new TSQLBasicCondition
            {
                Operator = SqlComparisonOperators.IsEqualTo,
                LogicalOperator = SqlLogicalOperators.AND
            };

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
                    Value = "99" 
                }
            );

            having.Children.Add(cond);

            // ACTION
            var sql = having.SQL();

            var expected = " HAVING 1=1" + Environment.NewLine +
                           "  AND [ColumnName] = 99" + Environment.NewLine;

            // ASSERT
            Assert.Equal(expected, sql);
            TestHelper.Assert.SerializationWorks(expected, having);
        }

        [Fact]
        public void SQL_Returns_Having_With_All_Condition_Types()
        {
            // ARRANGE
            var having = new TSQLHaving();

            having.Children.Clear();

            var basicCond = new TSQLBasicCondition
            {
                Operator = SqlComparisonOperators.IsEqualTo,
                LogicalOperator = SqlLogicalOperators.AND
            };

            basicCond.Children.Add(
                new TSQLColumn 
                { 
                    DataType = new TSQLDataType(SqlDbType.Int), 
                    ColumnName = "ColumnNameA" 
                }
            );

            basicCond.Children.Add(
                new TSQLLiteral 
                { 
                    DataType = new TSQLDataType(SqlDbType.Int), 
                    Value = "99" 
                }
            );

            having.Children.Add(basicCond);

            var between = new TSQLBetweenCondition
            {
                LeftOperand = new TSQLColumn { DataType = new TSQLDataType(SqlDbType.Int), ColumnName = "ColumnNameB" },
                StartVal = new TSQLLiteral { DataType = new TSQLDataType(SqlDbType.Int), Value = "1" },
                EndVal = new TSQLLiteral { DataType = new TSQLDataType(SqlDbType.Int), Value = "100" },
                LogicalOperator = SqlLogicalOperators.AND
            };

            having.Children.Add(between);

            var inList = new TSQLInListCondition();

            inList.Children.Add(
                new TSQLColumn 
                { 
                    DataType = new TSQLDataType(SqlDbType.Int), 
                    ColumnName = "ColumnNameC" 
                }
            );

            inList.InList.Add(new TSQLLiteral { DataType = new TSQLDataType(SqlDbType.Int), Value = "1" });
            inList.InList.Add(new TSQLLiteral { DataType = new TSQLDataType(SqlDbType.Int), Value = "2" });
            inList.InList.Add(new TSQLLiteral { DataType = new TSQLDataType(SqlDbType.Int), Value = "3" });
            inList.InList.Add(new TSQLLiteral { DataType = new TSQLDataType(SqlDbType.Int), Value = "4" });
            inList.InList.Add(new TSQLLiteral { DataType = new TSQLDataType(SqlDbType.Int), Value = "5" });

            inList.LogicalOperator = SqlLogicalOperators.AND;

            having.Children.Add(inList);

            var like = new TSQLLikeCondition
            {
                Pattern = "%pattern%",
                LogicalOperator = SqlLogicalOperators.AND
            };

            like.Children.Add(
                new TSQLColumn 
                { 
                    DataType = new TSQLDataType(SqlDbType.Int), 
                    ColumnName = "ColumnNameD" 
                }
            );

            having.Children.Add(like);

            // ACTION
            var sql = having.SQL();

            var expected = " HAVING 1=1" + Environment.NewLine +
                           "  AND [ColumnNameA] = 99" + Environment.NewLine +
                           "  AND [ColumnNameB] BETWEEN 1 AND 100" + Environment.NewLine +
                           "  AND [ColumnNameC] IN (1,2,3,4,5)" + Environment.NewLine +
                           "  AND [ColumnNameD] LIKE '%pattern%'" + Environment.NewLine;

            // ASSERT
            Assert.Equal(expected, sql);
            TestHelper.Assert.SerializationWorks(expected, having);
        }

        [Fact]
        public void SQL_Returns_Having_With_Condition_Group()
        {
            // ARRANGE
            var having = new TSQLHaving();

            having.Children.Clear();

            var condGrp = new TSQLConditionGroup();

            var basicCond = new TSQLBasicCondition
            {
                Operator = SqlComparisonOperators.IsEqualTo,
                LogicalOperator = SqlLogicalOperators.AND
            };

            basicCond.Children.Add(
                new TSQLColumn 
                { 
                    DataType = new TSQLDataType(SqlDbType.Int), 
                    ColumnName = "ColumnNameA" 
                }
            );

            basicCond.Children.Add(
                new TSQLLiteral 
                { 
                    DataType = new TSQLDataType(SqlDbType.Int), 
                    Value = "99" 
                }
            );

            condGrp.Children.Add(basicCond);

            var between = new TSQLBetweenCondition
            {
                LeftOperand = new TSQLColumn { DataType = new TSQLDataType(SqlDbType.Int), ColumnName = "ColumnNameB" },
                StartVal = new TSQLLiteral { DataType = new TSQLDataType(SqlDbType.Int), Value = "1" },
                EndVal = new TSQLLiteral { DataType = new TSQLDataType(SqlDbType.Int), Value = "100" },
                LogicalOperator = SqlLogicalOperators.AND
            };

            condGrp.Children.Add(between);

            var inList = new TSQLInListCondition();

            inList.Children.Add(
                new TSQLColumn 
                { 
                    DataType = new TSQLDataType(SqlDbType.Int), 
                    ColumnName = "ColumnNameC" 
                }
            );

            inList.InList.Add(new TSQLLiteral { DataType = new TSQLDataType(SqlDbType.Int), Value = "1" });
            inList.InList.Add(new TSQLLiteral { DataType = new TSQLDataType(SqlDbType.Int), Value = "2" });
            inList.InList.Add(new TSQLLiteral { DataType = new TSQLDataType(SqlDbType.Int), Value = "3" });
            inList.InList.Add(new TSQLLiteral { DataType = new TSQLDataType(SqlDbType.Int), Value = "4" });
            inList.InList.Add(new TSQLLiteral { DataType = new TSQLDataType(SqlDbType.Int), Value = "5" });

            inList.LogicalOperator = SqlLogicalOperators.AND;

            condGrp.Children.Add(inList);

            var like = new TSQLLikeCondition
            {
                Pattern = "%pattern%",
                LogicalOperator = SqlLogicalOperators.AND
            };

            like.Children.Add(
                new TSQLColumn 
                { 
                    DataType = new TSQLDataType(SqlDbType.Int), 
                    ColumnName = "ColumnNameD" 
                }
            );

            condGrp.Children.Add(like);

            having.Children.Add(condGrp);

            // ACTION
            var sql = having.SQL();

            var expected = " HAVING 1=1" + Environment.NewLine +
                           "  AND" + Environment.NewLine +
                           "  (" + Environment.NewLine +
                           "   [ColumnNameA] = 99 AND" + Environment.NewLine +
                           "   [ColumnNameB] BETWEEN 1 AND 100 AND" + Environment.NewLine +
                           "   [ColumnNameC] IN (1,2,3,4,5) AND" + Environment.NewLine +
                           "   [ColumnNameD] LIKE '%pattern%'" + Environment.NewLine +
                           "  )" + Environment.NewLine;

            // ASSERT
            Assert.Equal(expected, sql);
            TestHelper.Assert.SerializationWorks(expected, having);
        }

        [Fact]
        public void SQL_Returns_Having_With_BasicCondition_and_Comment()
        {
            // ARRANGE
            var having = new TSQLHaving();

            having.Children.Clear();

            having.Children.Add(new TSQLComment { CommentText = "Comment about condition below" });

            var cond = new TSQLBasicCondition
            {
                Operator = SqlComparisonOperators.IsEqualTo,
                LogicalOperator = SqlLogicalOperators.AND
            };

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
                    Value = "99" 
                }
            );

            having.Children.Add(cond);

            // ACTION
            var sql = having.SQL();

            var expected = " HAVING 1=1" + Environment.NewLine +
                           "  -- Comment about condition below" + Environment.NewLine +
                           "  AND [ColumnName] = 99" + Environment.NewLine;

            // ASSERT
            Assert.Equal(expected, sql);
            TestHelper.Assert.SerializationWorks(expected, having);
        }
    }
}
