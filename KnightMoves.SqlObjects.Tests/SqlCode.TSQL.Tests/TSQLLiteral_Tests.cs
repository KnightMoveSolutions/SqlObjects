using KnightMoves.SqlObjects.SqlCode.TSQL;
using System.Data;
using Xunit;

namespace KnightMoves.SqlObjects.Tests.TSql;

public class TSQLLiteral_Tests
{
    [Fact]
    public void IsQuoted_Returns_ExpectedResults()
    {
        var literal = new TSQLLiteral();

        literal.DataType = new TSQLDataType(System.Data.SqlDbType.BigInt);
        Assert.False(literal.IsQuoted, "IsQuoted should return false for BigInt");

        literal.DataType = new TSQLDataType(System.Data.SqlDbType.Binary);
        Assert.False(literal.IsQuoted, "IsQuoted should return false for Binary");

        literal.DataType = new TSQLDataType(System.Data.SqlDbType.Bit);
        Assert.False(literal.IsQuoted, "IsQuoted should return false for Bit");

        literal.DataType = new TSQLDataType(System.Data.SqlDbType.Char);
        Assert.True(literal.IsQuoted, "IsQuoted should return true for Char");

        literal.DataType = new TSQLDataType(System.Data.SqlDbType.Date);
        Assert.True(literal.IsQuoted, "IsQuoted should return true for Date");

        literal.DataType = new TSQLDataType(System.Data.SqlDbType.DateTime);
        Assert.True(literal.IsQuoted, "IsQuoted should return true for DateTime");

        literal.DataType = new TSQLDataType(System.Data.SqlDbType.DateTime2);
        Assert.True(literal.IsQuoted, "IsQuoted should return true for DateTime");

        literal.DataType = new TSQLDataType(System.Data.SqlDbType.DateTimeOffset);
        Assert.True(literal.IsQuoted, "IsQuoted should return true for DateTimeOffset");

        literal.DataType = new TSQLDataType(System.Data.SqlDbType.Decimal);
        Assert.False(literal.IsQuoted, "IsQuoted should return false for Decimal");

        literal.DataType = new TSQLDataType(System.Data.SqlDbType.Float);
        Assert.False(literal.IsQuoted, "IsQuoted should return false for Float");

        literal.DataType = new TSQLDataType(System.Data.SqlDbType.Image);
        Assert.False(literal.IsQuoted, "IsQuoted should return false for Image");

        literal.DataType = new TSQLDataType(System.Data.SqlDbType.Int);
        Assert.False(literal.IsQuoted, "IsQuoted should return false for Int");

        literal.DataType = new TSQLDataType(System.Data.SqlDbType.Money);
        Assert.False(literal.IsQuoted, "IsQuoted should return false for Money");

        literal.DataType = new TSQLDataType(System.Data.SqlDbType.NChar);
        Assert.True(literal.IsQuoted, "IsQuoted should return true for NChar");

        literal.DataType = new TSQLDataType(System.Data.SqlDbType.NText);
        Assert.True(literal.IsQuoted, "IsQuoted should return true for NText");

        literal.DataType = new TSQLDataType(System.Data.SqlDbType.NVarChar);
        Assert.True(literal.IsQuoted, "IsQuoted should return true for NVarChar");

        literal.DataType = new TSQLDataType(System.Data.SqlDbType.Real);
        Assert.False(literal.IsQuoted, "IsQuoted should return false for Real");

        literal.DataType = new TSQLDataType(System.Data.SqlDbType.SmallDateTime);
        Assert.True(literal.IsQuoted, "IsQuoted should return true for SmallDateTime");

        literal.DataType = new TSQLDataType(System.Data.SqlDbType.SmallInt);
        Assert.False(literal.IsQuoted, "IsQuoted should return false for SmallInt");

        literal.DataType = new TSQLDataType(System.Data.SqlDbType.SmallMoney);
        Assert.False(literal.IsQuoted, "IsQuoted should return false for SmallMoney");

        literal.DataType = new TSQLDataType(System.Data.SqlDbType.Structured);
        Assert.False(literal.IsQuoted, "IsQuoted should return false for Structured");

        literal.DataType = new TSQLDataType(System.Data.SqlDbType.Text);
        Assert.True(literal.IsQuoted, "IsQuoted should return true for Text");

        literal.DataType = new TSQLDataType(System.Data.SqlDbType.Time);
        Assert.True(literal.IsQuoted, "IsQuoted should return true for Time");

        literal.DataType = new TSQLDataType(System.Data.SqlDbType.Timestamp);
        Assert.True(literal.IsQuoted, "IsQuoted should return true for Timestamp");

        literal.DataType = new TSQLDataType(System.Data.SqlDbType.TinyInt);
        Assert.False(literal.IsQuoted, "IsQuoted should return false for TinyInt");

        literal.DataType = new TSQLDataType(System.Data.SqlDbType.Udt);
        Assert.False(literal.IsQuoted, "IsQuoted should return false for Udt");

        literal.DataType = new TSQLDataType(System.Data.SqlDbType.UniqueIdentifier);
        Assert.True(literal.IsQuoted, "IsQuoted should return true for UniqueIdentifier");

        literal.DataType = new TSQLDataType(System.Data.SqlDbType.VarBinary);
        Assert.False(literal.IsQuoted, "IsQuoted should return false for VarBinary");

        literal.DataType = new TSQLDataType(System.Data.SqlDbType.VarChar);
        Assert.True(literal.IsQuoted, "IsQuoted should return true for VarChar");

        literal.DataType = new TSQLDataType(System.Data.SqlDbType.Variant);
        Assert.False(literal.IsQuoted, "IsQuoted should return false for Variant");

        literal.DataType = new TSQLDataType(System.Data.SqlDbType.Xml);
        Assert.False(literal.IsQuoted, "IsQuoted should return false for Xml");

        literal.DataType = new TSQLDataType(System.Data.SqlDbType.VarChar);
        literal.Value = "*";
        Assert.False(literal.IsQuoted, "IsQuoated should return false if the Value property is '*' because it's a special SQL syntax.");
    }

    [Fact]
    public void SQL_Returns_UnQuoted_Value()
    {
        // ARRANGE
        var literal = new TSQLLiteral
        {
            DataType = new TSQLDataType(System.Data.SqlDbType.Int),
            Value = "9999"
        };

        // ACTION
        var sql = literal.SQL();

        var expected = "9999";

        // ASSERT
        Assert.Equal(expected, literal.SQL());
        TestHelper.Assert.SerializationWorks(expected, literal);
    }

    [Fact]
    public void SQL_Returns_Quoted_Value()
    {
        // ARRANGE
        var literal = new TSQLLiteral
        {
            DataType = new TSQLDataType(System.Data.SqlDbType.VarChar),
            Value = "String Data"
        };

        // ACTION
        var sql = literal.SQL();

        var expected = "'String Data'";

        // ASSERT
        Assert.Equal(expected, sql);
        TestHelper.Assert.SerializationWorks(expected, literal);
    }

    [Fact]
    public void SQL_Returns_UnQuoted_Literal_WhenDbType_Is_Null()
    {
        // ARRANGE
        var literal = new TSQLLiteral 
        { 
            DataType = new TSQLDataType(SqlDbType.Variant),
            Value = "@variable" 
        };

        // ACTION
        var sql = literal.SQL();

        var expected = "@variable";

        // ASSERT
        Assert.Equal(expected, sql);
        TestHelper.Assert.SerializationWorks(expected, literal);
    }
}
