using System;
using System.Data;

namespace KnightMoves.SqlObjects.SqlCode.TSQL;

public abstract partial class TSQLStatement : SqlStatement
{
    /// <summary>
    /// Starts a new condition using a SQL AND with the <paramref name="leftLiteral"/> object as the left operand
    /// or the EndVal of a BETWEEN clause if invoked after calling one of the BETWEEN(...) methods
    /// </summary>
    public override SqlStatement AND(ISqlLiteral leftLiteral)
    {
        if (leftLiteral.DataType == null)
            throw new ArgumentException($"The {nameof(leftLiteral.DataType)} property cannot be null for argument {nameof(leftLiteral)}");

        var between = this as ISqlBetweenCondition;

        if (between == null)
            return base.AND(leftLiteral);

        between.EndVal = leftLiteral;

        return GetConditionParent();
    }

    /// <summary>
    /// Starts a new condition using a SQL AND with a column as the left operand or the 
    /// EndVal of a BETWEEN clause if invoked after alling one of the BETWEEN(...) methods
    /// </summary>
    public override SqlStatement AND(string multiPartIdentifier, string operand)
    {
        var between = this as ISqlBetweenCondition;

        if (between == null || between.EndVal != null)
            return base.AND(multiPartIdentifier, operand);

        if (!string.IsNullOrEmpty(multiPartIdentifier))
        {
            between.EndVal = new TSQLColumn { MultiPartIdentifier = multiPartIdentifier, ColumnName = operand };
        }
        else
        {
            between.EndVal = new TSQLLiteral
            {
                DataType = new TSQLDataType(SqlDbType.VarChar),
                Value = operand
            };
        }

        return GetConditionParent();
    }

    /// <summary>
    /// Starts a new condition using a SQL AND with an <see cref="int"/> as the left operand or the 
    /// EndVal of a BETWEEN clause if invoked after alling one of the BETWEEN(...) methods
    /// </summary>
    public override SqlStatement AND(int operand)
    {
        var literal = new TSQLLiteral
        {
            DataType = new TSQLDataType(SqlDbType.Int),
            Value = operand.ToString()
        };

        return AND(literal);
    }

    /// <summary>
    /// Starts a new condition using a SQL AND with an <see cref="DateTime"/> as the left operand or the 
    /// EndVal of a BETWEEN clause if invoked after alling one of the BETWEEN(...) methods
    /// </summary>
    public override SqlStatement AND(DateTime operand)
    {
        var literal = new TSQLLiteral
        {
            DataType = new TSQLDataType(SqlDbType.DateTime),
            Value = operand.ToString(SqlDataType.SQL_DATE_STRING_FORMAT)
        };

        return AND(literal);
    }

    /// <summary>
    /// Starts a new condition using a SQL AND with an <see cref="bool"/> as the left operand 
    /// </summary>
    public override SqlStatement AND(bool operand)
    {
        var literal = new TSQLLiteral
        {
            DataType = new TSQLDataType(SqlDbType.Bit),
            Value = operand ? "1" : "0"
        };

        return AND(literal);
    }

    /// <summary>
    /// Starts a new condition using a SQL AND with an <see cref="long"/> as the left operand or the 
    /// EndVal of a BETWEEN clause if invoked after alling one of the BETWEEN(...) methods
    /// </summary>
    public override SqlStatement AND(long operand)
    {
        var literal = new TSQLLiteral
        {
            DataType = new TSQLDataType(SqlDbType.BigInt),
            Value = operand.ToString()
        };

        return AND(literal);
    }

    /// <summary>
    /// Starts a new condition using a SQL AND with an <see cref="Guid"/> as the left operand
    /// </summary>
    public override SqlStatement AND(Guid operand)
    {
        var literal = new TSQLLiteral
        {
            DataType = new TSQLDataType(SqlDbType.UniqueIdentifier),
            Value = operand.ToString()
        };

        return AND(literal);
    }
    /// <summary>
    /// Starts a new condition using a SQL AND with an <see cref="char"/> as the left operand 
    /// </summary>
    public override SqlStatement AND(char operand)
    {
        var literal = new TSQLLiteral
        {
            DataType = new TSQLDataType(SqlDbType.Char),
            Value = operand.ToString()
        };

        return AND(literal);
    }

    /// <summary>
    /// Starts a new condition using a SQL AND with an <see cref="decimal"/> as the left operand or the 
    /// EndVal of a BETWEEN clause if invoked after alling one of the BETWEEN(...) methods
    /// </summary>
    public override SqlStatement AND(decimal operand)
    {
        var literal = new TSQLLiteral
        {
            DataType = new TSQLDataType(SqlDbType.Decimal),
            Value = operand.ToString()
        };

        return AND(literal);
    }
}
