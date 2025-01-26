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

public class TSQLCase_Tests
{
    [Fact]
    public void SQL_Throws_Expection_On_Missing_When_Clauses()
    {
        var caseObj = new TSQLCase();
        
        // should throw exception because no WHEN clauses have been added
        Assert.Throws<InvalidOperationException>(() => { caseObj.SQL(); });
    }

    [Fact]
    public void SQL_Throws_Exception_On_Mismatching_DataTypes_In_When_Clause()
    {
        var caseObj = new TSQLCase();
        var when = new TSQLCaseWhen();

        caseObj.Children.Add(when);
        
        // should throw exception due to data type mismatch between the parent case object and the child when object.
        Assert.Throws<InvalidOperationException>(() => { caseObj.SQL(); });
    }

    [Fact]
    public void SQL_Throws_Exception_On_Mismatching_DataTypes_In_Else_Clause()
    {
        var caseObj = new TSQLCase { DataType = new TSQLDataType(SqlDbType.Int) };

        var elseObj = new TSQLCaseElse();

        elseObj.Result = new TSQLLiteral(); // DataType not specified here

        caseObj.Children.Add(elseObj);

        //Should throw an exception because data types don't match
        Assert.Throws<InvalidOperationException>(() => { elseObj.SQL(); });
    }

    [Fact]
    public void SQL_Returns_Case_When_Else()
    {
        // ARRANGE
        var script = new TSQLScript();
        var caseObj = new TSQLCase { DataType = new TSQLDataType(SqlDbType.Int) };
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

        caseObj.Children.Add(when);

        var elseObj = new TSQLCaseElse { DataType = new TSQLDataType(SqlDbType.Int), Result = new TSQLLiteral { DataType = new TSQLDataType(SqlDbType.Int), Value = "0" } };

        caseObj.Children.Add(elseObj);

        script.Children.Add(caseObj);

        // ACTION
        var sql = script.SQL();

        var expected = "  CASE" + Environment.NewLine +
                       "   WHEN [ColumnName] < 9999 THEN 1" + Environment.NewLine +
                       "   ELSE 0" + Environment.NewLine +
                       "  END";
        // ASSERT
        Assert.Equal(expected, sql);
        TestHelper.Assert.SerializationWorks(expected, script);
    }

    [Fact]
    public void SQL_Returns_Case_WhenWithTwoConditions_Else()
    {
        // ARRANGE
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

        var elseObj = new TSQLCaseElse 
        { 
            DataType = new TSQLDataType(SqlDbType.Int), 
            Result = new TSQLLiteral { DataType = new TSQLDataType(SqlDbType.Int), Value = "0" } 
        };

        caseObj.Children.Add(elseObj);

        script.Children.Add(caseObj);

        // ACTION
        var sql = script.SQL();

        var expected = "  CASE" + Environment.NewLine +
                       "   WHEN [ColumnName] > 999 AND [AnotherColumn] < 9999 THEN 1" + Environment.NewLine +
                       "   ELSE 0" + Environment.NewLine +
                       "  END";

        // ASSERT
        Assert.Equal(expected, sql);
        TestHelper.Assert.SerializationWorks(expected, script);
    }

    [Fact]
    public void SQL_Returns_Case_When_When_Else()
    {
        // ARRANGE
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

        var result = new TSQLLiteral { DataType = new TSQLDataType(SqlDbType.Int), Value = "1" };

        when.Children.Add(result);

        caseObj.Children.Add(when);

        when = new TSQLCaseWhen { DataType = new TSQLDataType(SqlDbType.Int) };

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

        result = new TSQLLiteral { DataType = new TSQLDataType(SqlDbType.Int), Value = "2" };

        when.Children.Add(result);

        caseObj.Children.Add(when);

        var elseObj = new TSQLCaseElse 
        { 
            DataType = new TSQLDataType(SqlDbType.Int), 
            Result = new TSQLLiteral { DataType = new TSQLDataType(SqlDbType.Int), Value = "0" } 
        };

        caseObj.Children.Add(elseObj);

        script.Children.Add(caseObj);

        // ACTION
        var sql = script.SQL();

        var expected = "  CASE" + Environment.NewLine +
                       "   WHEN [ColumnName] > 999 THEN 1" + Environment.NewLine +
                       "   WHEN [AnotherColumn] < 9999 THEN 2" + Environment.NewLine +
                       "   ELSE 0" + Environment.NewLine +
                       "  END";

        // ASSERT
        Assert.Equal(expected, sql);
        TestHelper.Assert.SerializationWorks(expected, script);
    }
}
