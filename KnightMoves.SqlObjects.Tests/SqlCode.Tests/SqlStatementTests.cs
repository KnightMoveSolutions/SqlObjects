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
using KnightMoves.SqlObjects.SqlCode;
using Moq;
using Xunit;

namespace KnightMoves.SqlObjects.Tests.SqlCode.Tests;

public class SqlStatementTests
{
    [Theory]
    [InlineData(SqlLogicalOperators.AND, "AND")]
    [InlineData(SqlLogicalOperators.OR, "OR")]
    public void GetOperatorString_Returns_LogicalOpEnumString(SqlLogicalOperators op, string opStr)
    {
        // ARRANGE
        var sqlStmt = new Mock<SqlStatement>().Object;

        // ACTION
        var result = sqlStmt.GetOperatorString(op);

        // ASSERT
        Assert.Equal(opStr, result);
    }

    [Theory]
    [InlineData(SqlComparisonOperators.IsEqualTo, "=")]
    [InlineData(SqlComparisonOperators.IsGreaterThan, ">")]
    [InlineData(SqlComparisonOperators.IsGreaterThanOrEqualTo, ">=")]
    [InlineData(SqlComparisonOperators.IsLessThan, "<")]
    [InlineData(SqlComparisonOperators.IsLessThanOrEqualTo, "<=")]
    [InlineData(SqlComparisonOperators.IsNotEqualTo, "<>")]
    public void GetOperatorString_Returns_ComparisonOpEnumString(SqlComparisonOperators op, string opStr)
    {
        // ARRANGE
        var sqlStmt = new Mock<SqlStatement>().Object;

        // ACTION
        var result = sqlStmt.GetOperatorString(op);

        // ASSERT
        Assert.Equal(opStr, result);
    }

    [Theory]
    [InlineData(SqlArithmeticOperators.Divide, "/")]
    [InlineData(SqlArithmeticOperators.Minus, "-")]
    [InlineData(SqlArithmeticOperators.Modulo, "%")]
    [InlineData(SqlArithmeticOperators.Multiply, "*")]
    [InlineData(SqlArithmeticOperators.Plus, "+")]
    public void GetOperatorString_Returns_ArithmeticOpEnumString(SqlArithmeticOperators op, string opStr)
    {
        // ARRANGE
        var sqlStmt = new Mock<SqlStatement>().Object;

        // ACTION
        var result = sqlStmt.GetOperatorString(op);

        // ASSERT
        Assert.Equal(opStr, result);
    }

    [Fact]
    public void ON_Retains_Data()
    {
        // ARRANGE
        var TEST_MULTIPART_ID = "tc";
        var TEST_COLUMN = "TestColumn";

        var sqlStmt = new Mock<TestSqlStatement>().Object;

        // ACTION
        var result = sqlStmt.ON(TEST_MULTIPART_ID, TEST_COLUMN);

        // ASSERT
        Assert.Equal(TEST_MULTIPART_ID, sqlStmt.GetLeftMultipartIdentifier);
        Assert.Equal(TEST_COLUMN, sqlStmt.GetLeftOperandValue);
    }

    [Fact]
    public void AND_RetainsSqlLiteral()
    {
        // ARRANGE
        var moqLiteral = new Mock<ISqlLiteral>();

        moqLiteral
            .Setup(lit => lit.Value)
            .Returns("TEST");

        var mockLiteral = moqLiteral.Object;

        var sqlStmt = new Mock<TestSqlStatement> { CallBase = true }.Object;

        // ACTION
        var result = sqlStmt.AND(mockLiteral);

        // ASSERT
        Assert.Equal(SqlLogicalOperators.AND, result.LogicalOperator);
        Assert.Equal(mockLiteral.Value, (result.LeftOperand as ISqlLiteral).Value);
    }

    [Fact]
    public void AND_Retains_OperandValue()
    {
        // ARRANGE
        var TEST_VALUE = "TEST";

        var sqlStmt = new Mock<TestSqlStatement> { CallBase = true }.Object;

        // ACTION
        var result = sqlStmt.AND(TEST_VALUE);

        // ASSERT
        Assert.Equal(SqlLogicalOperators.AND, result.LogicalOperator);
        Assert.Equal(TEST_VALUE, sqlStmt.GetLeftOperandValue);
    }

    [Fact]
    public void AND_Retains_OperandValue_and_MultiPartIdentifier()
    {
        // ARRANGE
        var TEST_MULTIPART_ID = "t";
        var TEST_VALUE = "TEST";

        var sqlStmt = new Mock<TestSqlStatement> { CallBase = true }.Object;

        // ACTION
        var result = sqlStmt.AND(TEST_MULTIPART_ID, TEST_VALUE);

        // ASSERT
        Assert.Equal(SqlLogicalOperators.AND, result.LogicalOperator);
        Assert.Equal(TEST_MULTIPART_ID, sqlStmt.GetLeftMultipartIdentifier);
        Assert.Equal(TEST_VALUE, sqlStmt.GetLeftOperandValue);
    }

    [Fact]
    public void OR_RetainsSqlLiteral()
    {
        // ARRANGE
        var moqLiteral = new Mock<ISqlLiteral>();

        moqLiteral
            .Setup(lit => lit.Value)
            .Returns("TEST");

        var mockLiteral = moqLiteral.Object;

        var sqlStmt = new Mock<TestSqlStatement>().Object;

        // ACTION
        var result = sqlStmt.OR(mockLiteral);

        // ASSERT
        Assert.Equal(SqlLogicalOperators.OR, result.LogicalOperator);
        Assert.Equal(mockLiteral.Value, (result.LeftOperand as ISqlLiteral).Value);
    }

    [Fact]
    public void OR_Retains_OperandValue()
    {
        // ARRANGE
        var TEST_VALUE = "TEST";

        var sqlStmt = new Mock<TestSqlStatement>().Object;

        // ACTION
        var result = sqlStmt.OR(TEST_VALUE);

        // ASSERT
        Assert.Equal(SqlLogicalOperators.OR, result.LogicalOperator);
        Assert.Equal(TEST_VALUE, sqlStmt.GetLeftOperandValue);
    }

    [Fact]
    public void OR_Retains_OperandValue_and_MultiPartIdentifier()
    {
        // ARRANGE
        var TEST_MULTIPART_ID = "t";
        var TEST_VALUE = "TEST";

        var sqlStmt = new Mock<TestSqlStatement>().Object;

        // ACTION
        var result = sqlStmt.OR(TEST_MULTIPART_ID, TEST_VALUE);

        // ASSERT
        Assert.Equal(SqlLogicalOperators.OR, result.LogicalOperator);
        Assert.Equal(TEST_MULTIPART_ID, sqlStmt.GetLeftMultipartIdentifier);
        Assert.Equal(TEST_VALUE, sqlStmt.GetLeftOperandValue);
    }

    [Fact]
    public void IsEqualTo_String_Invokes_Overload()
    {
        // ARRANGE
        var moqSqlStmt = new Mock<SqlStatement>();

        var sqlStmt = moqSqlStmt.Object;

        // ACTION
        sqlStmt.IsEqualTo(string.Empty);

        // ASSERT
        moqSqlStmt.Verify(s => s.IsEqualTo(It.IsAny<string>(), It.IsAny<string>()));
    }

    [Fact]
    public void IsEqualTo_Int_Invokes_Overload()
    {
        // ARRANGE
        var moqSqlStmt = new Mock<SqlStatement>();

        var sqlStmt = moqSqlStmt.Object;

        // ACTION
        sqlStmt.IsEqualTo(int.MinValue);

        // ASSERT
        moqSqlStmt.Verify(s => s.IsEqualTo(It.IsAny<int>()));
    }

    [Fact]
    public void IsEqualTo_DateTime_Invokes_Overload()
    {
        // ARRANGE
        var moqSqlStmt = new Mock<SqlStatement>();

        var sqlStmt = moqSqlStmt.Object;

        // ACTION
        sqlStmt.IsEqualTo(DateTime.Now);

        // ASSERT
        moqSqlStmt.Verify(s => s.IsEqualTo(It.IsAny<DateTime>()));
    }

    [Fact]
    public void IsEqualTo_bool_Invokes_Overload()
    {
        // ARRANGE
        var moqSqlStmt = new Mock<SqlStatement>();

        var sqlStmt = moqSqlStmt.Object;

        // ACTION
        sqlStmt.IsEqualTo(true);

        // ASSERT
        moqSqlStmt.Verify(s => s.IsEqualTo(It.IsAny<bool>()));
    }

    [Fact]
    public void IsEqualTo_long_Invokes_Overload()
    {
        // ARRANGE
        var moqSqlStmt = new Mock<SqlStatement>();

        var sqlStmt = moqSqlStmt.Object;

        // ACTION
        sqlStmt.IsEqualTo(long.MinValue);

        // ASSERT
        moqSqlStmt.Verify(s => s.IsEqualTo(It.IsAny<long>()));
    }

    [Fact]
    public void IsEqualTo_Guid_Invokes_Overload()
    {
        // ARRANGE
        var moqSqlStmt = new Mock<SqlStatement>();

        var sqlStmt = moqSqlStmt.Object;

        // ACTION
        sqlStmt.IsEqualTo(Guid.Empty);

        // ASSERT
        moqSqlStmt.Verify(s => s.IsEqualTo(It.IsAny<Guid>()));
    }

    [Fact]
    public void IsEqualTo_Char_Invokes_Overload()
    {
        // ARRANGE
        var moqSqlStmt = new Mock<SqlStatement>();

        var sqlStmt = moqSqlStmt.Object;

        // ACTION
        sqlStmt.IsEqualTo('x');

        // ASSERT
        moqSqlStmt.Verify(s => s.IsEqualTo(It.IsAny<char>()));
    }

    [Fact]
    public void IsEqualTo_decimal_Invokes_Overload()
    {
        // ARRANGE
        var moqSqlStmt = new Mock<SqlStatement>();

        var sqlStmt = moqSqlStmt.Object;

        // ACTION
        sqlStmt.IsEqualTo(decimal.MinValue);

        // ASSERT
        moqSqlStmt.Verify(s => s.IsEqualTo(It.IsAny<decimal>()));
    }


    [Fact]
    public void IsNotEqualTo_String_Invokes_Overload()
    {
        // ARRANGE
        var moqSqlStmt = new Mock<SqlStatement>();

        var sqlStmt = moqSqlStmt.Object;

        // ACTION
        sqlStmt.IsNotEqualTo(string.Empty);

        // ASSERT
        moqSqlStmt.Verify(s => s.IsNotEqualTo(It.IsAny<string>(), It.IsAny<string>()));
    }

    [Fact]
    public void IsNotEqualTo_Int_Invokes_Overload()
    {
        // ARRANGE
        var moqSqlStmt = new Mock<SqlStatement>();

        var sqlStmt = moqSqlStmt.Object;

        // ACTION
        sqlStmt.IsNotEqualTo(int.MinValue);

        // ASSERT
        moqSqlStmt.Verify(s => s.IsNotEqualTo(It.IsAny<int>()));
    }

    [Fact]
    public void IsNotEqualTo_DateTime_Invokes_Overload()
    {
        // ARRANGE
        var moqSqlStmt = new Mock<SqlStatement>();

        var sqlStmt = moqSqlStmt.Object;

        // ACTION
        sqlStmt.IsNotEqualTo(DateTime.Now);

        // ASSERT
        moqSqlStmt.Verify(s => s.IsNotEqualTo(It.IsAny<DateTime>()));
    }

    [Fact]
    public void IsNotEqualTo_bool_Invokes_Overload()
    {
        // ARRANGE
        var moqSqlStmt = new Mock<SqlStatement>();

        var sqlStmt = moqSqlStmt.Object;

        // ACTION
        sqlStmt.IsEqualTo(true);

        // ASSERT
        moqSqlStmt.Verify(s => s.IsEqualTo(It.IsAny<bool>()));
    }

    [Fact]
    public void IsNotEqualTo_long_Invokes_Overload()
    {
        // ARRANGE
        var moqSqlStmt = new Mock<SqlStatement>();

        var sqlStmt = moqSqlStmt.Object;

        // ACTION
        sqlStmt.IsNotEqualTo(long.MinValue);

        // ASSERT
        moqSqlStmt.Verify(s => s.IsNotEqualTo(It.IsAny<long>()));
    }

    [Fact]
    public void IsNotEqualTo_Guid_Invokes_Overload()
    {
        // ARRANGE
        var moqSqlStmt = new Mock<SqlStatement>();

        var sqlStmt = moqSqlStmt.Object;

        // ACTION
        sqlStmt.IsNotEqualTo(Guid.Empty);

        // ASSERT
        moqSqlStmt.Verify(s => s.IsNotEqualTo(It.IsAny<Guid>()));
    }

    [Fact]
    public void IsNotEqualTo_Char_Invokes_Overload()
    {
        // ARRANGE
        var moqSqlStmt = new Mock<SqlStatement>();

        var sqlStmt = moqSqlStmt.Object;

        // ACTION
        sqlStmt.IsNotEqualTo('x');

        // ASSERT
        moqSqlStmt.Verify(s => s.IsNotEqualTo(It.IsAny<char>()));
    }

    [Fact]
    public void IsNotEqualTo_decimal_Invokes_Overload()
    {
        // ARRANGE
        var moqSqlStmt = new Mock<SqlStatement>();

        var sqlStmt = moqSqlStmt.Object;

        // ACTION
        sqlStmt.IsNotEqualTo(decimal.MinValue);

        // ASSERT
        moqSqlStmt.Verify(s => s.IsNotEqualTo(It.IsAny<decimal>()));
    }


    [Fact]
    public void IsGreaterThan_String_Invokes_Overload()
    {
        // ARRANGE
        var moqSqlStmt = new Mock<SqlStatement>();

        var sqlStmt = moqSqlStmt.Object;

        // ACTION
        sqlStmt.IsGreaterThan(string.Empty);

        // ASSERT
        moqSqlStmt.Verify(s => s.IsGreaterThan(It.IsAny<string>(), It.IsAny<string>()));
    }

    [Fact]
    public void IsGreaterThan_Int_Invokes_Overload()
    {
        // ARRANGE
        var moqSqlStmt = new Mock<SqlStatement>();

        var sqlStmt = moqSqlStmt.Object;

        // ACTION
        sqlStmt.IsGreaterThan(int.MinValue);

        // ASSERT
        moqSqlStmt.Verify(s => s.IsGreaterThan(It.IsAny<int>()));
    }

    [Fact]
    public void IsGreaterThan_DateTime_Invokes_Overload()
    {
        // ARRANGE
        var moqSqlStmt = new Mock<SqlStatement>();

        var sqlStmt = moqSqlStmt.Object;

        // ACTION
        sqlStmt.IsGreaterThan(DateTime.Now);

        // ASSERT
        moqSqlStmt.Verify(s => s.IsGreaterThan(It.IsAny<DateTime>()));
    }

    [Fact]
    public void IsGreaterThan_bool_Invokes_Overload()
    {
        // ARRANGE
        var moqSqlStmt = new Mock<SqlStatement>();

        var sqlStmt = moqSqlStmt.Object;

        // ACTION
        sqlStmt.IsGreaterThan(true);

        // ASSERT
        moqSqlStmt.Verify(s => s.IsGreaterThan(It.IsAny<bool>()));
    }

    [Fact]
    public void IsGreaterThan_long_Invokes_Overload()
    {
        // ARRANGE
        var moqSqlStmt = new Mock<SqlStatement>();

        var sqlStmt = moqSqlStmt.Object;

        // ACTION
        sqlStmt.IsGreaterThan(long.MinValue);

        // ASSERT
        moqSqlStmt.Verify(s => s.IsGreaterThan(It.IsAny<long>()));
    }

    [Fact]
    public void IsGreaterThan_Guid_Invokes_Overload()
    {
        // ARRANGE
        var moqSqlStmt = new Mock<SqlStatement>();

        var sqlStmt = moqSqlStmt.Object;

        // ACTION
        sqlStmt.IsGreaterThan(Guid.Empty);

        // ASSERT
        moqSqlStmt.Verify(s => s.IsGreaterThan(It.IsAny<Guid>()));
    }

    [Fact]
    public void IsGreaterThan_Char_Invokes_Overload()
    {
        // ARRANGE
        var moqSqlStmt = new Mock<SqlStatement>();

        var sqlStmt = moqSqlStmt.Object;

        // ACTION
        sqlStmt.IsGreaterThan('x');

        // ASSERT
        moqSqlStmt.Verify(s => s.IsGreaterThan( It.IsAny<char>()));
    }

    [Fact]
    public void IsGreaterThan_decimal_Invokes_Overload()
    {
        // ARRANGE
        var moqSqlStmt = new Mock<SqlStatement>();

        var sqlStmt = moqSqlStmt.Object;

        // ACTION
        sqlStmt.IsGreaterThan(decimal.MinValue);

        // ASSERT
        moqSqlStmt.Verify(s => s.IsGreaterThan(It.IsAny<decimal>()));
    }


    [Fact]
    public void IsLessThan_String_Invokes_Overload()
    {
        // ARRANGE
        var moqSqlStmt = new Mock<SqlStatement>();

        var sqlStmt = moqSqlStmt.Object;

        // ACTION
        sqlStmt.IsLessThan(string.Empty);

        // ASSERT
        moqSqlStmt.Verify(s => s.IsLessThan(It.IsAny<string>(), It.IsAny<string>()));
    }

    [Fact]
    public void IsLessThan_Int_Invokes_Overload()
    {
        // ARRANGE
        var moqSqlStmt = new Mock<SqlStatement>();

        var sqlStmt = moqSqlStmt.Object;

        // ACTION
        sqlStmt.IsLessThan(int.MinValue);

        // ASSERT
        moqSqlStmt.Verify(s => s.IsLessThan(It.IsAny<int>()));
    }

    [Fact]
    public void IsLessThan_DateTime_Invokes_Overload()
    {
        // ARRANGE
        var moqSqlStmt = new Mock<SqlStatement>();

        var sqlStmt = moqSqlStmt.Object;

        // ACTION
        sqlStmt.IsLessThan(DateTime.Now);

        // ASSERT
        moqSqlStmt.Verify(s => s.IsLessThan(It.IsAny<DateTime>()));
    }

    [Fact]
    public void IsLessThan_bool_Invokes_Overload()
    {
        // ARRANGE
        var moqSqlStmt = new Mock<SqlStatement>();

        var sqlStmt = moqSqlStmt.Object;

        // ACTION
        sqlStmt.IsLessThan(true);

        // ASSERT
        moqSqlStmt.Verify(s => s.IsLessThan(It.IsAny<bool>()));
    }

    [Fact]
    public void IsLessThan_long_Invokes_Overload()
    {
        // ARRANGE
        var moqSqlStmt = new Mock<SqlStatement>();

        var sqlStmt = moqSqlStmt.Object;

        // ACTION
        sqlStmt.IsLessThan(long.MinValue);

        // ASSERT
        moqSqlStmt.Verify(s => s.IsLessThan(It.IsAny<long>()));
    }

    [Fact]
    public void IsLessThan_Guid_Invokes_Overload()
    {
        // ARRANGE
        var moqSqlStmt = new Mock<SqlStatement>();

        var sqlStmt = moqSqlStmt.Object;

        // ACTION
        sqlStmt.IsLessThan(Guid.Empty);

        // ASSERT
        moqSqlStmt.Verify(s => s.IsLessThan(It.IsAny<Guid>()));
    }

    [Fact]
    public void IsLessThan_Char_Invokes_Overload()
    {
        // ARRANGE
        var moqSqlStmt = new Mock<SqlStatement>();

        var sqlStmt = moqSqlStmt.Object;

        // ACTION
        sqlStmt.IsLessThan('x');

        // ASSERT
        moqSqlStmt.Verify(s => s.IsLessThan(It.IsAny<char>()));
    }

    [Fact]
    public void IsLessThan_decimal_Invokes_Overload()
    {
        // ARRANGE
        var moqSqlStmt = new Mock<SqlStatement>();

        var sqlStmt = moqSqlStmt.Object;

        // ACTION
        sqlStmt.IsLessThan(decimal.MinValue);

        // ASSERT
        moqSqlStmt.Verify(s => s.IsLessThan(It.IsAny<decimal>()));
    }


    [Fact]
    public void IsGreaterThanOrEqualTo_String_Invokes_Overload()
    {
        // ARRANGE
        var moqSqlStmt = new Mock<SqlStatement>();

        var sqlStmt = moqSqlStmt.Object;

        // ACTION
        sqlStmt.IsGreaterThanOrEqualTo(string.Empty);

        // ASSERT
        moqSqlStmt.Verify(s => s.IsGreaterThanOrEqualTo(It.IsAny<string>(), It.IsAny<string>()));
    }

    [Fact]
    public void IsGreaterThanOrEqualTo_Int_Invokes_Overload()
    {
        // ARRANGE
        var moqSqlStmt = new Mock<SqlStatement>();

        var sqlStmt = moqSqlStmt.Object;

        // ACTION
        sqlStmt.IsGreaterThanOrEqualTo(int.MinValue);

        // ASSERT
        moqSqlStmt.Verify(s => s.IsGreaterThanOrEqualTo(It.IsAny<int>()));
    }

    [Fact]
    public void IsGreaterThanOrEqualTo_DateTime_Invokes_Overload()
    {
        // ARRANGE
        var moqSqlStmt = new Mock<SqlStatement>();

        var sqlStmt = moqSqlStmt.Object;

        // ACTION
        sqlStmt.IsGreaterThanOrEqualTo(DateTime.Now);

        // ASSERT
        moqSqlStmt.Verify(s => s.IsGreaterThanOrEqualTo(It.IsAny<DateTime>()));
    }

    [Fact]
    public void IsGreaterThanOrEqualTo_bool_Invokes_Overload()
    {
        // ARRANGE
        var moqSqlStmt = new Mock<SqlStatement>();

        var sqlStmt = moqSqlStmt.Object;

        // ACTION
        sqlStmt.IsGreaterThanOrEqualTo(true);

        // ASSERT
        moqSqlStmt.Verify(s => s.IsGreaterThanOrEqualTo(It.IsAny<bool>()));
    }

    [Fact]
    public void IsGreaterThanOrEqualTo_long_Invokes_Overload()
    {
        // ARRANGE
        var moqSqlStmt = new Mock<SqlStatement>();

        var sqlStmt = moqSqlStmt.Object;

        // ACTION
        sqlStmt.IsGreaterThanOrEqualTo(long.MinValue);

        // ASSERT
        moqSqlStmt.Verify(s => s.IsGreaterThanOrEqualTo(It.IsAny<long>()));
    }

    [Fact]
    public void IsGreaterThanOrEqualTo_Guid_Invokes_Overload()
    {
        // ARRANGE
        var moqSqlStmt = new Mock<SqlStatement>();

        var sqlStmt = moqSqlStmt.Object;

        // ACTION
        sqlStmt.IsGreaterThanOrEqualTo(Guid.Empty);

        // ASSERT
        moqSqlStmt.Verify(s => s.IsGreaterThanOrEqualTo(It.IsAny<Guid>()));
    }

    [Fact]
    public void IsGreaterThanOrEqualTo_Char_Invokes_Overload()
    {
        // ARRANGE
        var moqSqlStmt = new Mock<SqlStatement>();

        var sqlStmt = moqSqlStmt.Object;

        // ACTION
        sqlStmt.IsGreaterThanOrEqualTo('x');

        // ASSERT
        moqSqlStmt.Verify(s => s.IsGreaterThanOrEqualTo(It.IsAny<char>()));
    }

    [Fact]
    public void IsGreaterThanOrEqualTo_decimal_Invokes_Overload()
    {
        // ARRANGE
        var moqSqlStmt = new Mock<SqlStatement>();

        var sqlStmt = moqSqlStmt.Object;

        // ACTION
        sqlStmt.IsGreaterThanOrEqualTo(decimal.MinValue);

        // ASSERT
        moqSqlStmt.Verify(s => s.IsGreaterThanOrEqualTo(It.IsAny<decimal>()));
    }


    [Fact]
    public void IsLessThanOrEqualTo_String_Invokes_Overload()
    {
        // ARRANGE
        var moqSqlStmt = new Mock<SqlStatement>();

        var sqlStmt = moqSqlStmt.Object;

        // ACTION
        sqlStmt.IsLessThanOrEqualTo(string.Empty);

        // ASSERT
        moqSqlStmt.Verify(s => s.IsLessThanOrEqualTo(It.IsAny<string>(), It.IsAny<string>()));
    }

    [Fact]
    public void IsLessThanOrEqualTo_Int_Invokes_Overload()
    {
        // ARRANGE
        var moqSqlStmt = new Mock<SqlStatement>();

        var sqlStmt = moqSqlStmt.Object;

        // ACTION
        sqlStmt.IsLessThanOrEqualTo(int.MinValue);

        // ASSERT
        moqSqlStmt.Verify(s => s.IsLessThanOrEqualTo(It.IsAny<int>()));
    }

    [Fact]
    public void IsLessThanOrEqualTo_DateTime_Invokes_Overload()
    {
        // ARRANGE
        var moqSqlStmt = new Mock<SqlStatement>();

        var sqlStmt = moqSqlStmt.Object;

        // ACTION
        sqlStmt.IsLessThanOrEqualTo(DateTime.Now);

        // ASSERT
        moqSqlStmt.Verify(s => s.IsLessThanOrEqualTo(It.IsAny<DateTime>()));
    }

    [Fact]
    public void IsLessThanOrEqualTo_bool_Invokes_Overload()
    {
        // ARRANGE
        var moqSqlStmt = new Mock<SqlStatement>();

        var sqlStmt = moqSqlStmt.Object;

        // ACTION
        sqlStmt.IsLessThanOrEqualTo(true);

        // ASSERT
        moqSqlStmt.Verify(s => s.IsLessThanOrEqualTo(It.IsAny<bool>()));
    }

    [Fact]
    public void IsLessThanOrEqualTo_long_Invokes_Overload()
    {
        // ARRANGE
        var moqSqlStmt = new Mock<SqlStatement>();

        var sqlStmt = moqSqlStmt.Object;

        // ACTION
        sqlStmt.IsLessThanOrEqualTo(long.MinValue);

        // ASSERT
        moqSqlStmt.Verify(s => s.IsLessThanOrEqualTo(It.IsAny<long>()));
    }

    [Fact]
    public void IsLessThanOrEqualTo_Guid_Invokes_Overload()
    {
        // ARRANGE
        var moqSqlStmt = new Mock<SqlStatement>();

        var sqlStmt = moqSqlStmt.Object;

        // ACTION
        sqlStmt.IsLessThanOrEqualTo(Guid.Empty);

        // ASSERT
        moqSqlStmt.Verify(s => s.IsLessThanOrEqualTo(It.IsAny<Guid>()));
    }

    [Fact]
    public void IsLessThanOrEqualTo_Char_Invokes_Overload()
    {
        // ARRANGE
        var moqSqlStmt = new Mock<SqlStatement>();

        var sqlStmt = moqSqlStmt.Object;

        // ACTION
        sqlStmt.IsLessThanOrEqualTo('x');

        // ASSERT
        moqSqlStmt.Verify(s => s.IsLessThanOrEqualTo(It.IsAny<char>()));
    }

    [Fact]
    public void IsLessThanOrEqualTo_decimal_Invokes_Overload()
    {
        // ARRANGE
        var moqSqlStmt = new Mock<SqlStatement>();

        var sqlStmt = moqSqlStmt.Object;

        // ACTION
        sqlStmt.IsLessThanOrEqualTo(decimal.MinValue);

        // ASSERT
        moqSqlStmt.Verify(s => s.IsLessThanOrEqualTo(It.IsAny<decimal>()));
    }
}

public abstract class TestSqlStatement : SqlStatement
{
    public string GetLeftOperandValue => LeftOperandValue;
    public string GetLeftMultipartIdentifier => LeftMultiPartIdentifier;
}
