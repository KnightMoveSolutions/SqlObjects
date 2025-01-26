/*
    THE LICENSED WORK IS PROVIDED UNDER THE TERMS OF THE DEVELOPER TOOL LIMITED 
    LICENSE (“LICENSE”) AS FIRST COMPLETED BY THE ORIGINAL AUTHOR. ANY USE, PUBLIC 
    DISPLAY, PUBLIC PERFORMANCE, REPRODUCTION OR DISTRIBUTION OF, OR PREPARATION OF 
    DERIVATIVE WORKS BASED ON THE LICENSED WORK CONSTITUTES RECIPIENT’S ACCEPTANCE 
    OF THIS LICENSE AND ITS TERMS, WHETHER OR NOT SUCH RECIPIENT READS THE TERMS OF THE 
    LICENSE. “LICENSED WORK” AND “RECIPIENT” ARE DEFINED IN THE LICENSE. A COPY OF THE 
    LICENSE IS LOCATED IN THE TEXT FILE ENTITLED “LICENSE.TXT” ACCOMPANYING THE CONTENTS 
    OF THIS FILE. IF A COPY OF THE LICENSE DOES NOT ACCOMPANY THIS FILE, A COPY OF THE 
    LICENSE MAY ALSO BE OBTAINED AT THE FOLLOWING WEB SITE:  
 
    https://docs.knightmovesolutions.com/sql-objects/license.html
*/

using System;
using System.Data;
using KnightMoves.SqlObjects.SqlCode.TSQL;
using Xunit;

namespace KnightMoves.SqlObjects.Tests.TSql;

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
