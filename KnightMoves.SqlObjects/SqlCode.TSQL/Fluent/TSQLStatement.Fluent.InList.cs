using System;
using System.Data;

namespace KnightMoves.SqlObjects.SqlCode.TSQL;

public abstract partial class TSQLStatement : SqlStatement
{
    /// <summary>
    /// Finishes a condition with a SQL IN (...) clause using the specified <see cref="string"/> <paramref name="values"/> as the list
    /// </summary>
    public override SqlStatement IN(params string[] values)
    {
        var parent = GetConditionParent();

        var inList = new TSQLInListCondition();

        inList.Children.Add(GetLeftOperand() as SqlStatement);

        foreach (string value in values)
        {
            inList.InList.Add(new TSQLLiteral
            {
                DataType = new TSQLDataType(SqlDbType.VarChar),
                Value = value
            });
        }

        parent.Children.Add(inList);

        return parent;
    }

    /// <summary>
    /// Finishes a condition with a SQL IN (...) clause using the specified <see cref="int"/> <paramref name="values"/> as the list
    /// </summary>
    public override SqlStatement IN(params int[] values)
    {
        var parent = GetConditionParent();

        var inList = new TSQLInListCondition();

        inList.Children.Add(GetLeftOperand() as SqlStatement);

        foreach (int value in values)
        {
            inList.InList.Add(new TSQLLiteral
            {
                DataType = new TSQLDataType(SqlDbType.Int),
                Value = value.ToString()
            });
        }

        parent.Children.Add(inList);

        return parent;
    }

    /// <summary>
    /// Finishes a condition with a SQL IN (...) clause using the specified <see cref="DateTime"/> <paramref name="values"/> as the list
    /// </summary>
    public override SqlStatement IN(params DateTime[] values)
    {
        var parent = GetConditionParent();

        var inList = new TSQLInListCondition();

        inList.Children.Add(GetLeftOperand() as SqlStatement);

        foreach (DateTime value in values)
        {
            inList.InList.Add(new TSQLLiteral
            {
                DataType = new TSQLDataType(SqlDbType.DateTime),
                Value = value.ToString(SqlDataType.SQL_DATE_STRING_FORMAT)
            });
        }

        parent.Children.Add(inList);

        return parent;
    }

    /// <summary>
    /// Finishes a condition with a SQL IN (...) clause using the specified <see cref="long"/> <paramref name="values"/> as the list
    /// </summary>
    public override SqlStatement IN(params long[] values)
    {
        var parent = GetConditionParent();

        var inList = new TSQLInListCondition();

        inList.Children.Add(GetLeftOperand() as SqlStatement);

        foreach (long value in values)
        {
            inList.InList.Add(new TSQLLiteral
            {
                DataType = new TSQLDataType(SqlDbType.BigInt),
                Value = value.ToString()
            });
        }

        parent.Children.Add(inList);

        return parent;
    }

    /// <summary>
    /// Finishes a condition with a SQL IN (...) clause using the specified <see cref="char"/> <paramref name="values"/> as the list
    /// </summary>
    public override SqlStatement IN(params char[] values)
    {
        var parent = GetConditionParent();

        var inList = new TSQLInListCondition();

        inList.Children.Add(GetLeftOperand() as SqlStatement);

        foreach (char value in values)
        {
            inList.InList.Add(new TSQLLiteral
            {
                DataType = new TSQLDataType(SqlDbType.Char),
                Value = value.ToString()
            });
        }

        parent.Children.Add(inList);

        return parent;
    }

    /// <summary>
    /// Finishes a condition with a SQL IN (...) clause using the specified <see cref="decimal"/> <paramref name="values"/> as the list
    /// </summary>
    public override SqlStatement IN(params decimal[] values)
    {
        var parent = GetConditionParent();

        var inList = new TSQLInListCondition();

        inList.Children.Add(GetLeftOperand() as SqlStatement);

        foreach (decimal value in values)
        {
            inList.InList.Add(new TSQLLiteral
            {
                DataType = new TSQLDataType(SqlDbType.Decimal),
                Value = value.ToString()
            });
        }

        parent.Children.Add(inList);

        return parent;
    }

}
