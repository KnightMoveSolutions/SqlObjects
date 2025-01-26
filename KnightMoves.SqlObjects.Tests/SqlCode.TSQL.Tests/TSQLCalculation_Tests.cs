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
using KnightMoves.SqlObjects.SqlCode;
using KnightMoves.SqlObjects.SqlCode.TSQL;
using Xunit;

namespace KnightMoves.SqlObjects.Tests.TSql;

public class TSQLCalculation_Tests
{
    [Fact]
    public void SQL_Method_Returns_Calculation()
    {
        // ARRANGE
        var calc = new TSQLCalculation
        {
            LeftOperand = new TSQLLiteral { DataType = new TSQLDataType(SqlDbType.Int), Value = "9999" },
            Operator = SqlArithmeticOperators.Plus,
            RightOperand = new TSQLLiteral { DataType = new TSQLDataType(SqlDbType.Int), Value = "1234" }
        };

        // ACTION
        var sql = calc.SQL();

        var expected = "(9999 + 1234)";

        // ASSERT
        Assert.Equal(expected, sql);
        TestHelper.Assert.SerializationWorks(expected, calc);
    }

    [Fact]
    public void SQL_Method_Returns_Nested_Calculation()
    {
        // ARRANGE
        var subCalc = new TSQLCalculation
        {
            LeftOperand = new TSQLLiteral { DataType = new TSQLDataType(SqlDbType.Int), Value = "9999" },
            Operator = SqlArithmeticOperators.Multiply,
            RightOperand = new TSQLLiteral { DataType = new TSQLDataType(SqlDbType.Int), Value = "1234" }
        };

        var calc = new TSQLCalculation
        {
            LeftOperand = new TSQLLiteral { DataType = new TSQLDataType(SqlDbType.Int), Value = "9999" },
            Operator = SqlArithmeticOperators.Plus,
            RightOperand = subCalc
        };

        // ACTION
        var sql = calc.SQL();

        var expected = "(9999 + (9999 * 1234))";

        // ASSERT
        Assert.Equal(expected, sql);
        TestHelper.Assert.SerializationWorks(expected, calc);
    }
}
