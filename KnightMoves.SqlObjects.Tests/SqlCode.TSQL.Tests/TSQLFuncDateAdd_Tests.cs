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

public class TSQLFuncDateAdd_Tests
{
    [Fact]
    public void SQL_Returns_DateAdd_With_Defaults()
    {
        // ARRANGE
        var func = new TSQLFuncDateAdd();

        // ACTION
        var sql = func.SQL();

        var expected = "DATEADD(Day, 0, GETDATE())";

        // ASSERT
        Assert.Equal(expected, sql);
        TestHelper.Assert.SerializationWorks(expected, func);
    }

    [Fact]
    public void SQL_Returns_DateAdd_With_Literals()
    {
        // ARRANGE
        var func = new TSQLFuncDateAdd();

        var datePart = new TSQLLiteral { DataType = new TSQLDataType(SqlDbType.Variant), Value = "Month" };
        var increment = new TSQLLiteral { DataType = new TSQLDataType(SqlDbType.Int), Value = "1" };
        var dateExpression = new TSQLLiteral { DataType = new TSQLDataType(SqlDbType.DateTime), Value = "01/01/2012" };

        func.SetParameterValue(TSQLFuncDateAddParams.datePart, datePart);
        func.SetParameterValue(TSQLFuncDateAddParams.increment, increment);
        func.SetParameterValue(TSQLFuncDateAddParams.dateExpression, dateExpression);

        // ACTION
        var sql = func.SQL();

        var expected = "DATEADD(Month, 1, '01/01/2012')";

        // ASSERT
        Assert.Equal(expected, sql);
        TestHelper.Assert.SerializationWorks(expected, func);
    }

    [Fact]
    public void SQL_Returns_DateAdd_With_Column()
    {
        // ARRANGE
        var func = new TSQLFuncDateAdd();

        var datePart = new TSQLLiteral { DataType = new TSQLDataType(SqlDbType.Variant), Value = "Month" };
        var increment = new TSQLLiteral { DataType = new TSQLDataType(SqlDbType.Int), Value = "1" };
        var dateExpression = new TSQLColumn { DataType = new TSQLDataType(SqlDbType.DateTime), ColumnName = "DateColumnName" };

        func.SetParameterValue(TSQLFuncDateAddParams.datePart, datePart);
        func.SetParameterValue(TSQLFuncDateAddParams.increment, increment);
        func.SetParameterValue(TSQLFuncDateAddParams.dateExpression, dateExpression);

        // ACTION
        var sql = func.SQL();

        var expected = "DATEADD(Month, 1, [DateColumnName])";

        // ASSERT
        Assert.Equal(expected, sql);
        TestHelper.Assert.SerializationWorks(expected, func);
    }
}
