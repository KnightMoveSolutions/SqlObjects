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

public class TSQLFuncYear_Tests
{
    [Fact]
    public void SQL_Returns_Year_With_Default()
    {
        // ARRANGE
        var func = new TSQLFuncYear();

        // ACTION
        var sql = func.SQL();

        var expected = "YEAR(GETDATE())";

        // ASSERT
        Assert.Equal(expected, sql);
        TestHelper.Assert.SerializationWorks(expected, func);
    }

    [Fact]
    public void SQL_Returns_Year_With_Literal()
    {
        // ARRANGE
        var func = new TSQLFuncYear();

        var dateExpression = new TSQLLiteral { DataType = new TSQLDataType(SqlDbType.DateTime), Value = "01/15/2012" };

        func.SetParameterValue(TSQLFuncYearParams.dateExpression, dateExpression);

        // ACTION
        var sql = func.SQL();

        var expected = "YEAR('01/15/2012')";

        // ASSERT
        Assert.Equal(expected, sql);
        TestHelper.Assert.SerializationWorks(expected, func);
    }

    [Fact]
    public void SQL_Returns_Year_With_Column()
    {
        // ARRANGE
        var func = new TSQLFuncYear();

        var dateExpression = new TSQLColumn { DataType = new TSQLDataType(SqlDbType.DateTime), ColumnName = "ColumnName" };

        func.SetParameterValue(TSQLFuncYearParams.dateExpression, dateExpression);

        // ACTION
        var sql = func.SQL();

        var expected = "YEAR([ColumnName])";

        // ASSERT
        Assert.Equal(expected, sql);
        TestHelper.Assert.SerializationWorks(expected, func);
    }

    [Fact]
    public void SQL_Returns_Year_With_Schema_And_Column()
    {
        // ARRANGE
        var func = new TSQLFuncYear();

        var dateExpression = new TSQLColumn { DataType = new TSQLDataType(SqlDbType.DateTime), Schema = "dbo", TableName = "TableName", ColumnName = "ColumnName" };

        func.SetParameterValue(TSQLFuncYearParams.dateExpression, dateExpression);

        // ACTION
        var sql = func.SQL();

        var expected = "YEAR([dbo].[TableName].[ColumnName])";

        // ASSERT
        Assert.Equal(expected, sql);
        TestHelper.Assert.SerializationWorks(expected, func);
    }
}
