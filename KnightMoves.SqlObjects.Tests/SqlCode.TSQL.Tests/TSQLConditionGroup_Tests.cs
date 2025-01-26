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
using KnightMoves.SqlObjects.SqlCode;
using KnightMoves.SqlObjects.SqlCode.TSQL;
using Xunit;

namespace KnightMoves.SqlObjects.Tests.TSql;

public class TSQLConditionGroup_Tests
{
    [Fact]
    public void SQL_Returns_Condition_Group()
    {
        // ARRANGE
        var condGrp = new TSQLConditionGroup();

        TSQLBasicCondition cond;

        cond = new TSQLBasicCondition 
        { 
            Operator = SqlComparisonOperators.IsGreaterThan, 
            LogicalOperator = SqlLogicalOperators.AND 
        };

        cond.Children.Add(
            new TSQLColumn { DataType = new TSQLDataType(SqlDbType.Int), ColumnName = "ColumnA" }
        );

        cond.Children.Add(
            new TSQLLiteral { DataType = new TSQLDataType(SqlDbType.Int), Value = "99" }
        );

        condGrp.Children.Add(cond);

        cond = new TSQLBasicCondition
        {
            Operator = SqlComparisonOperators.IsGreaterThan,
            LogicalOperator = SqlLogicalOperators.AND
        };

        cond.Children.Add(
            new TSQLColumn 
            { 
                DataType = new TSQLDataType(SqlDbType.Int), 
                ColumnName = "ColumnB" 
            }
        );

        cond.Children.Add(
            new TSQLLiteral 
            { 
                DataType = new TSQLDataType(SqlDbType.Int),
                Value = "99" 
            }
        );

        condGrp.Children.Add(cond);

        cond = new TSQLBasicCondition
        {
            Operator = SqlComparisonOperators.IsGreaterThan,
            LogicalOperator = SqlLogicalOperators.AND
        };

        cond.Children.Add(
            new TSQLColumn 
            { 
                DataType = new TSQLDataType(SqlDbType.Int), 
                ColumnName = "ColumnC" 
            }
        );

        cond.Children.Add(
            new TSQLLiteral 
            { 
                DataType = new TSQLDataType(SqlDbType.Int), 
                Value = "99" 
            }
        );

        condGrp.Children.Add(cond);

        cond = new TSQLBasicCondition
        {
            Operator = SqlComparisonOperators.IsGreaterThan,
            LogicalOperator = SqlLogicalOperators.AND
        };

        cond.Children.Add(
            new TSQLColumn 
            { 
                DataType = new TSQLDataType(SqlDbType.Int), 
                ColumnName = "ColumnD" 
            }
        );

        cond.Children.Add(
            new TSQLLiteral 
            { 
                DataType = new TSQLDataType(SqlDbType.Int), 
                Value = "99" 
            }
        );

        condGrp.Children.Add(cond);

        // ACTION
        var sql = condGrp.SQL();

        var expected = " (" + Environment.NewLine +
                        "  [ColumnA] > 99 AND" + Environment.NewLine +
                        "  [ColumnB] > 99 AND" + Environment.NewLine +
                        "  [ColumnC] > 99 AND" + Environment.NewLine +
                        "  [ColumnD] > 99" + Environment.NewLine +
                        " )";

        // ASSERT
        Assert.Equal(expected, sql);
        TestHelper.Assert.SerializationWorks(expected, condGrp);
    }
}
