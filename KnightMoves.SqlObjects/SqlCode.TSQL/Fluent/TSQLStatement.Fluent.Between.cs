using System;
using System.Data;

namespace KnightMoves.SqlObjects.SqlCode.TSQL;

public abstract partial class TSQLStatement : SqlStatement
{
    /// <summary>
    /// Starts a SQL BETWEEN clause with the provided StartVal. If <paramref name="multipartIdentifier"/> 
    /// is provided then it will interpret <paramref name="value"/> as a column name. Otherwise, it will 
    /// treat <paramref name="value"/> as a string that will be surrounded in single quotes.
    /// </summary>
    public override SqlStatement BETWEEN(string multipartIdentifier, string value)
    {
        ISqlQueryExpression startVal;

        if (!string.IsNullOrEmpty(multipartIdentifier))
        {
            startVal = new TSQLColumn { MultiPartIdentifier = multipartIdentifier, ColumnName = value };
        }
        else
        {
            startVal = new TSQLLiteral
            {
                DataType = new TSQLDataType(SqlDbType.VarChar),
                Value = value
            };
        }

        var between = new TSQLBetweenCondition
        {
            LeftOperand = GetLeftOperand(),
            StartVal = startVal
        };

        var parentStatement = GetConditionParent();

        parentStatement.Children.Add(between);

        return between;
    }

    /// <summary>
    /// Starts a SQL BETWEEN clause with the provided <see cref="int"/> <paramref name="value"/> as the StartVal
    /// </summary>
    public override SqlStatement BETWEEN(int value)
    {
        var between = new TSQLBetweenCondition
        {
            LeftOperand = GetLeftOperand(),
            StartVal = new TSQLLiteral
            {
                DataType = new TSQLDataType(SqlDbType.Int),
                Value = value.ToString()
            }
        };

        var parentStatement = GetConditionParent();

        parentStatement.Children.Add(between);

        return between;
    }

    /// <summary>
    /// Starts a SQL BETWEEN clause with the provided <see cref="DateTime"/> <paramref name="value"/> as the StartVal
    /// </summary>
    public override SqlStatement BETWEEN(DateTime value)
    {
        var between = new TSQLBetweenCondition
        {
            LeftOperand = GetLeftOperand(),
            StartVal = new TSQLLiteral
            {
                DataType = new TSQLDataType(SqlDbType.DateTime),
                Value = value.ToString(SqlDataType.SQL_DATE_STRING_FORMAT)
            }
        };

        var parentStatement = GetConditionParent();

        parentStatement.Children.Add(between);

        return between;
    }

    /// <summary>
    /// Starts a SQL BETWEEN clause with the provided <see cref="long"/> <paramref name="value"/> as the StartVal
    /// </summary>
    public override SqlStatement BETWEEN(long value)
    {
        var between = new TSQLBetweenCondition
        {
            LeftOperand = GetLeftOperand(),
            StartVal = new TSQLLiteral
            {
                DataType = new TSQLDataType(SqlDbType.BigInt),
                Value = value.ToString()
            }
        };

        var parentStatement = GetConditionParent();

        parentStatement.Children.Add(between);

        return between;
    }

    /// <summary>
    /// Starts a SQL BETWEEN clause with the provided <see cref="char"/> <paramref name="value"/> as the StartVal
    /// </summary>
    public override SqlStatement BETWEEN(char value)
    {
        var between = new TSQLBetweenCondition
        {
            LeftOperand = GetLeftOperand(),
            StartVal = new TSQLLiteral
            {
                DataType = new TSQLDataType(SqlDbType.Char),
                Value = value.ToString()
            }
        };

        var parentStatement = GetConditionParent();

        parentStatement.Children.Add(between);

        return between;
    }

    /// <summary>
    /// Starts a SQL BETWEEN clause with the provided <see cref="decimal"/> <paramref name="value"/> as the StartVal
    /// </summary>
    public override SqlStatement BETWEEN(decimal value)
    {
        var between = new TSQLBetweenCondition
        {
            LeftOperand = GetLeftOperand(),
            StartVal = new TSQLLiteral
            {
                DataType = new TSQLDataType(SqlDbType.Decimal),
                Value = value.ToString()
            }
        };

        var parentStatement = GetConditionParent();

        parentStatement.Children.Add(between);

        return between;
    }

}
