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
