﻿/*
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

public class TSQLFuncDateDiff_Tests
{
    [Fact]
    public void SQL_Returns_DateDiff_With_Defaults()
    {
        // ARRANGE
        var func = new TSQLFuncDateDiff();

        // ACTION
        var sql = func.SQL();

        var expected = "DATEDIFF(Day, GETDATE(), GETDATE())";

        // ASSERT
        Assert.Equal(expected, sql);
        TestHelper.Assert.SerializationWorks(expected, func);
    }

    [Fact]
    public void SQL_Returns_DateDiff_With_Literals()
    {
        // ARRANGE
        var func = new TSQLFuncDateDiff();

        var datePart = new TSQLLiteral { DataType = new TSQLDataType(SqlDbType.Variant), Value = "Month" };
        var startDate = new TSQLLiteral { DataType = new TSQLDataType(SqlDbType.DateTime), Value = "01/01/2012" };
        var endDate = new TSQLLiteral { DataType = new TSQLDataType(SqlDbType.DateTime), Value = "12/31/2012" };

        func.SetParameterValue(TSQLFuncDateDiffParams.datePart, datePart);
        func.SetParameterValue(TSQLFuncDateDiffParams.startDate, startDate);
        func.SetParameterValue(TSQLFuncDateDiffParams.endDate, endDate);

        // ACTION
        var sql = func.SQL();

        var expected = "DATEDIFF(Month, '01/01/2012', '12/31/2012')";

        // ASSERT
        Assert.Equal(expected, sql);
        TestHelper.Assert.SerializationWorks(expected, func);
    }

    [Fact]
    public void SQL_Returns_DateDiff_With_Columns()
    {
        // ARRANGE
        var func = new TSQLFuncDateDiff();

        var datePart = new TSQLLiteral { DataType = new TSQLDataType(SqlDbType.Variant), Value = "Month" };
        var startDate = new TSQLColumn { DataType = new TSQLDataType(SqlDbType.DateTime), ColumnName = "DateColumnA" };
        var endDate = new TSQLColumn { DataType = new TSQLDataType(SqlDbType.DateTime), ColumnName = "DateColumnB" };

        func.SetParameterValue(TSQLFuncDateDiffParams.datePart, datePart);
        func.SetParameterValue(TSQLFuncDateDiffParams.startDate, startDate);
        func.SetParameterValue(TSQLFuncDateDiffParams.endDate, endDate);

        // ACTION
        var sql = func.SQL();

        var expected = "DATEDIFF(Month, [DateColumnA], [DateColumnB])";

        // ASSERT
        Assert.Equal(expected, sql);
        TestHelper.Assert.SerializationWorks(expected, func);
    }
}
