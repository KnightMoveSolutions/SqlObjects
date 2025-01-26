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

using KnightMoves.SqlObjects.SqlCode;
using KnightMoves.SqlObjects.SqlCode.TSQL;
using System;
using System.Data;
using Xunit;

namespace KnightMoves.SqlObjects.Tests.TSql;

public class TSQLSelect_Tests
{
    [Fact]
    public void SQL_Returns_Select_With_Star_As_Default()
    {
        // ARRANGE
        var select = new TSQLSelect();

        // ACTION
        var sql = select.SQL();

        var expected = " SELECT * ";

        // ASSERT
        Assert.Equal(expected, sql);
        TestHelper.Assert.SerializationWorks(expected, select);
    }

    [Fact]
    public void SQL_Returns_Select_With_Columns_and_Star()
    {
        // ARRANGE
        var select = new TSQLSelect();

        select.Children.Add(new TSQLColumn { DataType = new TSQLDataType(SqlDbType.VarChar), ColumnName = "ColumnA" });
        select.Children.Add(new TSQLColumn { DataType = new TSQLDataType(SqlDbType.VarChar), ColumnName = "ColumnB" });
        select.Children.Add(new TSQLColumn { DataType = new TSQLDataType(SqlDbType.VarChar), ColumnName = "ColumnC" });
        select.Children.Add(new TSQLLiteral { DataType = new TSQLDataType(SqlDbType.VarChar), Value = "*" });

        // ACTION
        var sql = select.SQL();

        var expected = " SELECT" + Environment.NewLine +
                       "  [ColumnA]," + Environment.NewLine +
                       "  [ColumnB]," + Environment.NewLine +
                       "  [ColumnC]," + Environment.NewLine +
                       "  *" + Environment.NewLine;

        // ASSERT
        Assert.Equal(expected, sql);
        TestHelper.Assert.SerializationWorks(expected, select);
    }

    [Fact]
    public void SQL_Returns_Select_With_Aliases()
    {
        // ARRANGE
        var select = new TSQLSelect();

        select.Children.Add(new TSQLColumn { DataType = new TSQLDataType(SqlDbType.VarChar), ColumnName = "ColumnA", Alias = "A" });
        select.Children.Add(new TSQLColumn { DataType = new TSQLDataType(SqlDbType.VarChar), ColumnName = "ColumnB", Alias = "B" });
        select.Children.Add(new TSQLColumn { DataType = new TSQLDataType(SqlDbType.VarChar), ColumnName = "ColumnC", Alias = "C" });
        select.Children.Add(new TSQLLiteral { DataType = new TSQLDataType(SqlDbType.VarChar), Value = "*" });

        // ACTION
        var sql = select.SQL();

        var expected = " SELECT" + Environment.NewLine +
                       "  [ColumnA] AS [A]," + Environment.NewLine +
                       "  [ColumnB] AS [B]," + Environment.NewLine +
                       "  [ColumnC] AS [C]," + Environment.NewLine +
                       "  *" + Environment.NewLine;

        // ASSERT
        Assert.Equal(expected, sql);
        TestHelper.Assert.SerializationWorks(expected, select);
    }

    [Fact]
    public void SQL_Returns_Select_With_Case()
    {
        // ARRANGE
        var select = new TSQLSelect();

        var caseObj = new TSQLCase { DataType = new TSQLDataType(SqlDbType.VarChar), Alias = "CaseResult" };

        TSQLCaseWhen when;

        when = new TSQLCaseWhen { DataType = new TSQLDataType(SqlDbType.VarChar) };

        var cond1 = new TSQLBasicCondition
        {
            Operator = SqlComparisonOperators.IsEqualTo
        };

        cond1.Children.Add(
            new TSQLColumn { DataType = new TSQLDataType(SqlDbType.VarChar), ColumnName = "ColumnA" }
        );

        cond1.Children.Add(
            new TSQLLiteral { DataType = new TSQLDataType(SqlDbType.VarChar), Value = "XYZ" }
        );

        when.Children.Add(cond1);

        when.Children.Add(new TSQLLiteral { DataType = new TSQLDataType(SqlDbType.VarChar), Value = "1" });

        caseObj.Children.Add(when);

        when = new TSQLCaseWhen { DataType = new TSQLDataType(SqlDbType.VarChar) };

        var cond2 = new TSQLBasicCondition
        {
            Operator = SqlComparisonOperators.IsEqualTo
        };

        cond2.Children.Add(
            new TSQLColumn 
            { 
                DataType = new TSQLDataType(SqlDbType.VarChar), 
                ColumnName = "ColumnB" 
            }
        );

        cond2.Children.Add(
            new TSQLLiteral 
            { 
                DataType = new TSQLDataType(SqlDbType.VarChar), 
                Value = "ABC" 
            }
        );

        when.Children.Add(cond2);

        when.Children.Add(new TSQLLiteral { DataType = new TSQLDataType(SqlDbType.VarChar), Value = "2" });

        var elseObj = new TSQLCaseElse { Result = new TSQLLiteral { DataType = new TSQLDataType(SqlDbType.VarChar), Value = "0" } };

        caseObj.Children.Add(when);

        caseObj.Children.Add(elseObj);

        select.Children.Add(caseObj);

        select.Children.Add(new TSQLColumn { DataType = new TSQLDataType(SqlDbType.VarChar), ColumnName = "ColumnA", Alias = "A" });
        select.Children.Add(new TSQLColumn { DataType = new TSQLDataType(SqlDbType.VarChar), ColumnName = "ColumnB", Alias = "B" });
        select.Children.Add(new TSQLColumn { DataType = new TSQLDataType(SqlDbType.VarChar), ColumnName = "ColumnC", Alias = "C" });
        select.Children.Add(new TSQLLiteral { DataType = new TSQLDataType(SqlDbType.VarChar), Value = "*" });

        // ACTION
        var sql = select.SQL();

        var expected = " SELECT" + Environment.NewLine +
                       "  CASE" + Environment.NewLine +
                       "   WHEN [ColumnA] = 'XYZ' THEN '1'" + Environment.NewLine +
                       "   WHEN [ColumnB] = 'ABC' THEN '2'" + Environment.NewLine +
                       "   ELSE '0'" + Environment.NewLine +
                       "  END AS [CaseResult]," + Environment.NewLine +
                       "  [ColumnA] AS [A]," + Environment.NewLine +
                       "  [ColumnB] AS [B]," + Environment.NewLine +
                       "  [ColumnC] AS [C]," + Environment.NewLine +
                       "  *" + Environment.NewLine;

        // ASSERT
        Assert.Equal(expected, sql);
        TestHelper.Assert.SerializationWorks(expected, select);
    }

    [Fact]
    public void SQL_Returns_Select_With_Functions()
    {
        // ARRANGE
        var avg = new TSQLFuncAvg();

        avg.DataType = new TSQLDataType(SqlDbType.Variant);
        avg.SetParameterValue("aggregateExpression", new TSQLColumn { DataType = new TSQLDataType(SqlDbType.Variant), ColumnName = "ColumnA" });
        avg.Alias = "AvgA";

        var sum = new TSQLFuncSum();

        sum.DataType = new TSQLDataType(SqlDbType.Variant);
        sum.SetParameterValue("aggregateExpression", new TSQLColumn { DataType = new TSQLDataType(SqlDbType.Variant), ColumnName = "ColumnB" });
        sum.Alias = "SumB";

        var select = new TSQLSelect();

        select.Children.Add(avg);
        select.Children.Add(sum);
        select.Children.Add(new TSQLColumn { DataType = new TSQLDataType(SqlDbType.VarChar), ColumnName = "ColumnA", Alias = "A" });
        select.Children.Add(new TSQLColumn { DataType = new TSQLDataType(SqlDbType.VarChar), ColumnName = "ColumnB", Alias = "B" });
        select.Children.Add(new TSQLColumn { DataType = new TSQLDataType(SqlDbType.VarChar), ColumnName = "ColumnC", Alias = "C" });
        select.Children.Add(new TSQLLiteral { DataType = new TSQLDataType(SqlDbType.VarChar), Value = "*" });

        // ACTION
        var sql = select.SQL();

        var expected = " SELECT" + Environment.NewLine +
                       "  AVG([ColumnA]) AS [AvgA]," + Environment.NewLine +
                       "  SUM([ColumnB]) AS [SumB]," + Environment.NewLine +
                       "  [ColumnA] AS [A]," + Environment.NewLine +
                       "  [ColumnB] AS [B]," + Environment.NewLine +
                       "  [ColumnC] AS [C]," + Environment.NewLine +
                       "  *" + Environment.NewLine;

        // ASSERT
        Assert.Equal(expected, sql);
        TestHelper.Assert.SerializationWorks(expected, select);
    }

    [Fact]
    public void SQL_Returns_Select_With_Comment()
    {
        // ARRANGE
        var select = new TSQLSelect();

        select.Children.Add(new TSQLColumn { DataType = new TSQLDataType(SqlDbType.VarChar), ColumnName = "ColumnA" });
        select.Children.Add(new TSQLComment { CommentText = "Comment about ColumnB below" });
        select.Children.Add(new TSQLColumn { DataType = new TSQLDataType(SqlDbType.VarChar), ColumnName = "ColumnB" });
        select.Children.Add(new TSQLColumn { DataType = new TSQLDataType(SqlDbType.VarChar), ColumnName = "ColumnC" });
        select.Children.Add(new TSQLLiteral { DataType = new TSQLDataType(SqlDbType.VarChar), Value = "*" });

        // ACTION
        var sql = select.SQL();

        var expected = " SELECT" + Environment.NewLine +
                      "  [ColumnA]," + Environment.NewLine +
                      "  -- Comment about ColumnB below" + Environment.NewLine +
                      "  [ColumnB]," + Environment.NewLine +
                      "  [ColumnC]," + Environment.NewLine +
                      "  *" + Environment.NewLine;

        // ASSERT
        Assert.Equal(expected, sql);
        TestHelper.Assert.SerializationWorks(expected, select);
    }
}
