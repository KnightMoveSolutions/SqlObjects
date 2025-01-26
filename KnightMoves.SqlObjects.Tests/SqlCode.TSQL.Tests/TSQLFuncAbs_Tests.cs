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

public class TSQLFuncAbs_Tests
{
    [Fact]
    public void SQL_Returns_ABS_Function_With_Zero_Default_Arg()
    {
        // ARRANGE
        TSQLFuncAbs func = new TSQLFuncAbs();

        // ACTION
        var sql = func.SQL();

        var expected = "ABS(0)";

        // ASSERT
        Assert.Equal(expected, sql);
    }

    [Fact]
    public void SQL_Returns_ABS_Function_With_Literal_Parameter()
    {
        // ARRANGE
        var func = new TSQLFuncAbs();

        var paramValue = new TSQLLiteral 
        { 
            DataType = new TSQLDataType(SqlDbType.Decimal), 
            Value = "99.9" 
        };

        func.SetParameterValue(TSQLFuncAbsParams.numericExpression, paramValue);

        // ACTION
        var sql = func.SQL();

        var expected = "ABS(99.9)";

        // ASSERT
        Assert.Equal(expected, sql);
        TestHelper.Assert.SerializationWorks(expected, func);
    }

    [Fact]
    public void SQL_Returns_ABS_Function_With_Column_Parameter()
    {
        TSQLFuncAbs func = new TSQLFuncAbs();

        func.SetParameterValue(TSQLFuncAbsParams.numericExpression, new TSQLColumn { DataType = new TSQLDataType(SqlDbType.Decimal), ColumnName = "NumericColumnName" });

        Assert.Equal("ABS([NumericColumnName])", func.SQL());
    }
}
