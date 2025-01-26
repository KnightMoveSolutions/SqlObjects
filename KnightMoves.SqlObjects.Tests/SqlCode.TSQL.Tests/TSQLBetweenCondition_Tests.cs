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

using System.Data;
using KnightMoves.SqlObjects.SqlCode.TSQL;
using Xunit;

namespace KnightMoves.SqlObjects.Tests.TSql;

public class TSQLBetweenCondition_Tests
{
    [Fact]
    public void SQL_Method_Returns_Expected_SQL()
    {
        // ARRANGE
        var between = new TSQLBetweenCondition
        {
            LeftOperand = new TSQLColumn { DataType = new TSQLDataType(SqlDbType.Int), ColumnName = "ColumnName" },
            StartVal = new TSQLLiteral { DataType = new TSQLDataType(SqlDbType.Int), Value = "1" },
            EndVal = new TSQLLiteral { DataType = new TSQLDataType(SqlDbType.Int), Value = "100" }
        };

        // ACTION
        var sql = between.SQL();

        var expected = "[ColumnName] BETWEEN 1 AND 100";

        // ASSERT
        Assert.Equal(expected, sql);
        TestHelper.Assert.SerializationWorks(expected, between);
    }
}
