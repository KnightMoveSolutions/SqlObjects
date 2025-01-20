using System;
using System.Data;
using KnightMoves.SqlObjects.SqlCode;
using KnightMoves.SqlObjects.SqlCode.TSQL;
using Xunit;

namespace KnightMoves.SqlObjects.Tests.TSql;

public class TSQLFuncIsNull_Tests
{
    [Fact]
    public void SQL_Returns_IsNull_With_String_Type()
    {
        // ARRANGE
        var func = new TSQLFuncIsNull(SqlDbType.VarChar);

        func.SetParameterValue(TSQLFuncIsNull.TSQLFuncIsNullParams.checkExpression, new TSQLColumn { DataType = new TSQLDataType(SqlDbType.Variant), ColumnName = "ColumnA" });
        func.SetParameterValue(TSQLFuncIsNull.TSQLFuncIsNullParams.replacementValue, new TSQLLiteral { DataType = new TSQLDataType(SqlDbType.VarChar), Value = "X" });

        // ACTION
        var sql = func.SQL();

        var expected = "ISNULL([ColumnA], 'X')";

        // ASSERT
        Assert.Equal(expected, sql);
        TestHelper.Assert.SerializationWorks(expected, func);
    }

    [Fact]
    public void SQL_Returns_IsNull_With_Numeric_Type()
    {
        // ARRANGE
        var func = new TSQLFuncIsNull(SqlDbType.Int);

        func.SetParameterValue(TSQLFuncIsNull.TSQLFuncIsNullParams.checkExpression, new TSQLColumn { DataType = new TSQLDataType(SqlDbType.Variant), ColumnName = "ColumnA" });
        func.SetParameterValue(TSQLFuncIsNull.TSQLFuncIsNullParams.replacementValue, new TSQLLiteral { DataType = new TSQLDataType(SqlDbType.Int), Value = "99" });

        // ACTION
        var sql = func.SQL();

        var expected = "ISNULL([ColumnA], 99)";

        // ASSERT
        Assert.Equal(expected, sql);
        TestHelper.Assert.SerializationWorks(expected, func);
    }

    [Fact]
    public void SQL_Returns_IsNull_With_Bool_Type()
    {
        // ARRANGE
        var func = new TSQLFuncIsNull(SqlDbType.Bit);

        func.SetParameterValue(TSQLFuncIsNull.TSQLFuncIsNullParams.checkExpression, new TSQLColumn { DataType = new TSQLDataType(SqlDbType.Variant), ColumnName = "ColumnA" });
        func.SetParameterValue(TSQLFuncIsNull.TSQLFuncIsNullParams.replacementValue, new TSQLLiteral { DataType = new TSQLDataType(SqlDbType.Bit), Value = "false" });

        // ACTION
        var sql = func.SQL();

        var expected = "ISNULL([ColumnA], 0)";

        // ASSERT
        Assert.Equal(expected, sql);
        TestHelper.Assert.SerializationWorks(expected, func);
    }

    [Fact]
    public void SQL_Returns_IsNull_With_Date_Type()
    {
        // ARRANGE
        var func = new TSQLFuncIsNull(SqlDbType.DateTime);

        func.SetParameterValue(TSQLFuncIsNull.TSQLFuncIsNullParams.checkExpression, new TSQLColumn { DataType = new TSQLDataType(SqlDbType.Variant), ColumnName = "ColumnA" });
        func.SetParameterValue(TSQLFuncIsNull.TSQLFuncIsNullParams.replacementValue, new TSQLLiteral { DataType = new TSQLDataType(SqlDbType.DateTime), Value = "2021-01-01 00:00:00" });

        // ACTION
        var sql = func.SQL();

        var expected = "ISNULL([ColumnA], '2021-01-01 00:00:00')";

        // ASSERT
        Assert.Equal(expected, sql);
        TestHelper.Assert.SerializationWorks(expected, func);
    }

    [Fact]
    public void SQL_Returns_IsNull_With_Guid_Type()
    {
        // ARRANGE
        var func = new TSQLFuncIsNull(SqlDbType.UniqueIdentifier);

        func.SetParameterValue(TSQLFuncIsNull.TSQLFuncIsNullParams.checkExpression, new TSQLColumn { DataType = new TSQLDataType(SqlDbType.Variant), ColumnName = "ColumnA" });
        func.SetParameterValue(TSQLFuncIsNull.TSQLFuncIsNullParams.replacementValue, new TSQLLiteral { DataType = new TSQLDataType(SqlDbType.UniqueIdentifier), Value = Guid.Empty.ToString() });

        // ACTION
        var sql = func.SQL();

        var expected = $"ISNULL([ColumnA], '{Guid.Empty.ToString()}')";

        // ASSERT
        Assert.Equal(expected, sql);
        TestHelper.Assert.SerializationWorks(expected, func);
    }

    [Fact]
    public void SQL_Returns_IsNull_With_Char_Type()
    {
        // ARRANGE
        var func = new TSQLFuncIsNull(SqlDbType.Char);

        func.SetParameterValue(TSQLFuncIsNull.TSQLFuncIsNullParams.checkExpression, new TSQLColumn { DataType = new TSQLDataType(SqlDbType.Variant), ColumnName = "ColumnA" });
        func.SetParameterValue(TSQLFuncIsNull.TSQLFuncIsNullParams.replacementValue, new TSQLLiteral { DataType = new TSQLDataType(SqlDbType.Char), Value = "X" });

        // ACTION
        var sql = func.SQL();

        var expected = $"ISNULL([ColumnA], 'X')";

        // ASSERT
        Assert.Equal(expected, sql);
        TestHelper.Assert.SerializationWorks(expected, func);
    }

    [Fact]
    public void SQL_Returns_IsNull_With_Function()
    {
        // ARRANGE
        var func = new TSQLFuncIsNull(SqlDbType.DateTime);

        func.SetParameterValue(TSQLFuncIsNull.TSQLFuncIsNullParams.checkExpression, new TSQLColumn { DataType = new TSQLDataType(SqlDbType.Variant), ColumnName = "ColumnA" });
        func.SetParameterValue(TSQLFuncIsNull.TSQLFuncIsNullParams.replacementValue, new TSQLFuncGetDate());

        // ACTION
        var sql = func.SQL();

        var expected = $"ISNULL([ColumnA], GETDATE())";

        // ASSERT
        Assert.Equal(expected, sql);
        TestHelper.Assert.SerializationWorks(expected, func);
    }

    [Fact]
    public void SQL_Returns_IsNull_With_Calculation()
    {
        // ARRANGE
        var calculation = new TSQLCalculation
        {
            DataType = new TSQLDataType(SqlDbType.Int),
            LeftOperand = new TSQLColumn { DataType = new TSQLDataType(SqlDbType.Int), ColumnName = "ColumnA" },
            Operator = SqlArithmeticOperators.Multiply,
            RightOperand = new TSQLColumn { DataType = new TSQLDataType(SqlDbType.Int), ColumnName = "ColumnB" }
        };

        var func = new TSQLFuncIsNull(SqlDbType.Int);

        func.SetParameterValue(TSQLFuncIsNull.TSQLFuncIsNullParams.checkExpression, calculation);
        func.SetParameterValue(TSQLFuncIsNull.TSQLFuncIsNullParams.replacementValue, new TSQLLiteral { DataType = new TSQLDataType(SqlDbType.Int), Value = "0" });

        // ACTION
        var sql = func.SQL();

        var expected = $"ISNULL(([ColumnA] * [ColumnB]), 0)";

        // ASSERT
        Assert.Equal(expected, sql);
        TestHelper.Assert.SerializationWorks(expected, func);
    }
}
