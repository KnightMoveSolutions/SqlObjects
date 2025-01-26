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

public class TSQLUpdate_Tests
{
    private readonly string NL = Environment.NewLine;

    [Fact]
    public void SQL_Returns_Basic_Update()
    {
        // ARRANGE
        var updateStmt = new TSQLUpdate
        {
            Schema = "dbo",
            Table = "Customers"
        };

        var update1 = new TSQLBasicCondition { Operator = SqlComparisonOperators.IsEqualTo };

        update1.Children.Add(
            new TSQLColumn 
            { 
                DataType = new TSQLDataType(SqlDbType.VarChar), 
                ColumnName = "FirstName" 
            }
        );

        update1.Children.Add(
            new TSQLLiteral 
            { 
                DataType = new TSQLDataType(SqlDbType.VarChar), 
                Value = "John" 
            }
        );

        var update2 = new TSQLBasicCondition { Operator = SqlComparisonOperators.IsEqualTo };

        update2.Children.Add(
            new TSQLColumn 
            { 
                DataType = new TSQLDataType(SqlDbType.VarChar), 
                ColumnName = "MiddleInitial" 
            }
        );

        update2.Children.Add(
            new TSQLLiteral 
            { 
                DataType = new TSQLDataType(SqlDbType.VarChar), 
                Value = "Q" 
            }
        );

        var update3 = new TSQLBasicCondition { Operator = SqlComparisonOperators.IsEqualTo };

        update3.Children.Add(
            new TSQLColumn 
            { 
                DataType = new TSQLDataType(SqlDbType.VarChar), 
                ColumnName = "LastName" 
            }
        );

        update3.Children.Add(
            new TSQLLiteral 
            { 
                DataType = new TSQLDataType(SqlDbType.VarChar), 
                Value = "Doe" 
            }
        );

        updateStmt.Children.Add(update1);
        updateStmt.Children.Add(update2);
        updateStmt.Children.Add(update3);

        // ACTION
        var sql = updateStmt.SQL();

        var expected = $" UPDATE [dbo].[Customers] SET{NL}" +
                       $"  [FirstName] = 'John',{NL}" +
                       $"  [MiddleInitial] = 'Q',{NL}" +
                       $"  [LastName] = 'Doe'{NL}";

        // ASSERT
        Assert.Equal(expected, sql);
        TestHelper.Assert.SerializationWorks(expected, updateStmt);
    }

    [Fact]
    public void SQL_Returns_Update_With_Comment()
    {
        // ARRANGE
        var updateStmt = new TSQLUpdate
        {
            Schema = "dbo",
            Table = "Customers"
        };

        var update1 = new TSQLBasicCondition { Operator = SqlComparisonOperators.IsEqualTo };

        update1.Children.Add(
            new TSQLColumn 
            { 
                DataType = new TSQLDataType(SqlDbType.VarChar), 
                ColumnName = "FirstName" 
            }
        );

        update1.Children.Add(
            new TSQLLiteral 
            { 
                DataType = new TSQLDataType(SqlDbType.VarChar), 
                Value = "John" 
            }
        );

        var update2 = new TSQLBasicCondition { Operator = SqlComparisonOperators.IsEqualTo };

        update2.Children.Add(
            new TSQLColumn 
            { 
                DataType = new TSQLDataType(SqlDbType.VarChar), 
                ColumnName = "MiddleInitial" 
            }
        );

        update2.Children.Add(
            new TSQLLiteral 
            { 
                DataType = new TSQLDataType(SqlDbType.VarChar), 
                Value = "Q" 
            }
        );

        var update3 = new TSQLBasicCondition { Operator = SqlComparisonOperators.IsEqualTo };

        update3.Children.Add(
            new TSQLColumn 
            { 
                DataType = new TSQLDataType(SqlDbType.VarChar), 
                ColumnName = "LastName" 
            }
        );

        update3.Children.Add(
            new TSQLLiteral 
            { 
                DataType = new TSQLDataType(SqlDbType.VarChar), 
                Value = "Doe" 
            }
        );

        updateStmt.Children.Add(update1);
        updateStmt.Children.Add(update2);
        updateStmt.Children.Add(new TSQLComment { CommentText = "Comment about LastName column below" });
        updateStmt.Children.Add(update3);

        // ACTION
        var sql = updateStmt.SQL();

        var expected = $" UPDATE [dbo].[Customers] SET{NL}" +
                       $"  [FirstName] = 'John',{NL}" +
                       $"  [MiddleInitial] = 'Q',{NL}" +
                       $"  -- Comment about LastName column below{NL}" +
                       $"  [LastName] = 'Doe'{NL}";

        // ASSERT
        Assert.Equal(expected, sql);
        TestHelper.Assert.SerializationWorks(expected, updateStmt);
    }
}
