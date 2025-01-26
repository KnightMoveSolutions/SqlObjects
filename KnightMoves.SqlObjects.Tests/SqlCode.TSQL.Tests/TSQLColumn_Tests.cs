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

using KnightMoves.SqlObjects.SqlCode.TSQL;
using System.Data;
using Xunit;

namespace KnightMoves.SqlObjects.Tests.TSql;

public class TSQLColumn_Tests
{
    [Fact]
    public void SQL_Method_Returns_ColumnName()
    {
        // ARRANGE
        var col = new TSQLColumn
        {
            DataType = new TSQLDataType(SqlDbType.Int),
            ColumnName = "ColumnName"
        };

        // ACTION 
        var sql = col.SQL();

        var expected = "[ColumnName]";

        // ASSERT
        Assert.Equal(expected, sql);
        TestHelper.Assert.SerializationWorks(expected, col);
    }

    [Fact]
    public void SQL_Method_Returns_Fully_Qualified_ColumnName()
    {
        // ARRANGE
        var col = new TSQLColumn
        {
            DataType = new TSQLDataType(SqlDbType.Int),
            Schema = "dbo",
            TableName = "TableName",
            ColumnName = "ColumnName"
        };

        // ACTION
        var sql = col.SQL();

        var expected = "[dbo].[TableName].[ColumnName]";

        // ASSERT
        Assert.Equal(expected, sql);
        TestHelper.Assert.SerializationWorks(expected, col);
    }
}

