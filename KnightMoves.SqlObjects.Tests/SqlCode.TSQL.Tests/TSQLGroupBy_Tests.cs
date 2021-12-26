using System;
using System.Data;
using KnightMoves.SqlObjects.SqlCode.TSQL;
using Xunit;

namespace KnightMoves.SqlObjects.Tests.TSql
{
    public class TSQLGroupBy_Tests
    {
        [Fact]
        public void SQL_Returns_GroupBy_From_Select_List()
        {
            // ARRANGE
            var select = new TSQLSelect();

            select.Children.Add(new TSQLColumn { DataType = new TSQLDataType(SqlDbType.VarChar), ColumnName = "ColumnA" });
            select.Children.Add(new TSQLColumn { DataType = new TSQLDataType(SqlDbType.VarChar), ColumnName = "ColumnB" });
            select.Children.Add(new TSQLColumn { DataType = new TSQLDataType(SqlDbType.VarChar), ColumnName = "ColumnC" });

            var groupBy = new TSQLGroupBy();

            var script = new TSQLScript();

            script.Children.Add(select);
            script.Children.Add(groupBy);

            // ACTION
            var sql = groupBy.SQL();

            var expected = "  GROUP BY" + Environment.NewLine +
                           "   [ColumnA]," + Environment.NewLine +
                           "   [ColumnB]," + Environment.NewLine +
                           "   [ColumnC]" + Environment.NewLine;

            // ASSERT
            Assert.Equal(expected, sql);
        }

        [Fact]
        public void SQL_Returns_GroupBy_With_Aggregate_Funcs()
        {
            var avg = new TSQLFuncAvg { DataType = new TSQLDataType(SqlDbType.Variant) };
            avg.SetParameterValue("aggregateExpression", new TSQLColumn { DataType = new TSQLDataType(SqlDbType.Variant), ColumnName = "ColumnA" });

            var sum = new TSQLFuncSum { DataType = new TSQLDataType(SqlDbType.Variant) };
            sum.SetParameterValue("aggregateExpression", new TSQLColumn { DataType = new TSQLDataType(SqlDbType.Variant), ColumnName = "ColumnB" });

            var select = new TSQLSelect();

            select.Children.Add(avg);
            select.Children.Add(sum);
            select.Children.Add(new TSQLColumn { DataType = new TSQLDataType(SqlDbType.VarChar), ColumnName = "ColumnA" });
            select.Children.Add(new TSQLColumn { DataType = new TSQLDataType(SqlDbType.VarChar), ColumnName = "ColumnB" });
            select.Children.Add(new TSQLColumn { DataType = new TSQLDataType(SqlDbType.VarChar), ColumnName = "ColumnC" });

            var groupBy = new TSQLGroupBy();

            var script = new TSQLScript();

            script.Children.Add(select);
            script.Children.Add(groupBy);

            var sql = groupBy.SQL();

            var expected = "  GROUP BY" + Environment.NewLine +
                           "   [ColumnA]," + Environment.NewLine +
                           "   [ColumnB]," + Environment.NewLine +
                           "   [ColumnC]" + Environment.NewLine;

            Assert.Equal(expected, sql);
        }

        [Fact]
        public void SQL_Returns_GroupBy_With_Columns()
        {
            var groupBy = new TSQLGroupBy();

            groupBy.Children.Add(new TSQLColumn { DataType = new TSQLDataType(SqlDbType.VarChar), ColumnName = "ColumnA" });
            groupBy.Children.Add(new TSQLColumn { DataType = new TSQLDataType(SqlDbType.VarChar), ColumnName = "ColumnB" });
            groupBy.Children.Add(new TSQLColumn { DataType = new TSQLDataType(SqlDbType.VarChar), ColumnName = "ColumnC" });

            var script = new TSQLScript();

            script.Children.Add(groupBy);

            var sql = groupBy.SQL();

            var expected = "  GROUP BY" + Environment.NewLine +
                           "   [ColumnA]," + Environment.NewLine +
                           "   [ColumnB]," + Environment.NewLine +
                           "   [ColumnC]" + Environment.NewLine;

            Assert.Equal(expected, sql);
        }

        [Fact]
        public void SQL_Returns_Missing_Select_List_Comment()
        {
            var script = new TSQLScript();
            var groupBy = new TSQLGroupBy();

            script.Children.Add(new TSQLSelect());
            script.Children.Add(groupBy);

            var sql = groupBy.SQL();

            Assert.Contains("-- No", sql);
        }

        [Fact]
        public void SQL_Returns_Missing_Columns_Comment()
        {
            var script = new TSQLScript();
            var groupBy = new TSQLGroupBy();

            script.Children.Add(groupBy);

            var sql = groupBy.SQL();

            Assert.Contains("-- No", sql);
        }


        [Fact]
        public void SQL_Returns_GroupBy_With_Comment()
        {
            var groupBy = new TSQLGroupBy();

            groupBy.Children.Add(new TSQLColumn { DataType = new TSQLDataType(SqlDbType.VarChar), ColumnName = "ColumnA" });
            groupBy.Children.Add(new TSQLComment { CommentText = "Comment about ColumnB below" });
            groupBy.Children.Add(new TSQLColumn { DataType = new TSQLDataType(SqlDbType.VarChar), ColumnName = "ColumnB" });
            groupBy.Children.Add(new TSQLColumn { DataType = new TSQLDataType(SqlDbType.VarChar), ColumnName = "ColumnC" });

            var script = new TSQLScript();

            script.Children.Add(groupBy);

            var sql = groupBy.SQL();

            var expected = "  GROUP BY" + Environment.NewLine +
                           "   [ColumnA]," + Environment.NewLine +
                           "   -- Comment about ColumnB below" + Environment.NewLine +
                           "   [ColumnB]," + Environment.NewLine +
                           "   [ColumnC]" + Environment.NewLine;

            Assert.Equal(expected, sql);
        }
    }
}
