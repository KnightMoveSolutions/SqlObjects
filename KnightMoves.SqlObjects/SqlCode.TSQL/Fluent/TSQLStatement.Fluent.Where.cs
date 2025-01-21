using System;

namespace KnightMoves.SqlObjects.SqlCode.TSQL;

public abstract partial class TSQLStatement : SqlStatement
{
    /// <summary>
    /// Starts a SQL WHERE clause
    /// </summary>
    public override SqlStatement WHERE()
    {
        var where = new TSQLWhereClause();
        var scope = GetParentScope();
        scope.Children.Add(where);
        return where;
    }

    /// <summary>
    /// Starts a SQL WHERE clause using <paramref name="leftLiteral"/> object as the left operand of the first condition
    /// </summary>
    public override SqlStatement WHERE(ISqlLiteral leftLiteral)
    {
        if (leftLiteral.DataType == null)
            throw new ArgumentException($"The {nameof(leftLiteral.DataType)} property cannot be null for argument {nameof(leftLiteral)}");

        var where = new TSQLWhereClause();
        var scope = GetParentScope();
        scope.Children.Add(where);
        where.AND(leftLiteral);
        return where;
    }

    /// <summary>
    /// Starts a SQL WHERE clause using <paramref name="operand"/> string as the left operand of the first condition
    /// </summary>
    public override SqlStatement WHERE(string operand)
    {
        return WHERE(string.Empty, operand);
    }

    /// <summary>
    /// Starts a SQL WHERE clause using a column as the left operand of the first condition 
    /// </summary>
    public override SqlStatement WHERE(string multiPartIdentifier, string operand)
    {
        var where = new TSQLWhereClause();
        var parent = GetParentScope();

        if (parent.IsUpdate || parent.IsDelete)
            parent = parent.Parent;

        parent.Children.Add(where);
        where.AND(multiPartIdentifier, operand);
        return where;
    }

    /// <summary>
    /// Starts a SQL WHERE clause using an <see cref="int"/> as the left operand of the first condition
    /// </summary>
    public override SqlStatement WHERE(int value)
    {
        var where = MakeWhere(value);
        where.AND(value);
        return where;
    }

    /// <summary>
    /// Starts a SQL WHERE clause using a <see cref="DateTime"/> as the left operand of the first condition
    /// </summary>
    public override SqlStatement WHERE(DateTime value)
    {
        var where = MakeWhere(value);
        where.AND(value);
        return where;
    }

    /// <summary>
    /// Starts a SQL WHERE clause using a <see cref="bool"/> as the left operand of the first condition
    /// </summary>
    public override SqlStatement WHERE(bool value)
    {
        var where = MakeWhere(value);
        where.AND(value);
        return where;
    }

    /// <summary>
    /// Starts a SQL WHERE clause using a <see cref="long"/> as the left operand of the first condition
    /// </summary>
    public override SqlStatement WHERE(long value)
    {
        var where = MakeWhere(value);
        where.AND(value);
        return where;
    }

    /// <summary>
    /// Starts a SQL WHERE clause using a <see cref="Guid"/> as the left operand of the first condition
    /// </summary>
    public override SqlStatement WHERE(Guid value)
    {
        var where = MakeWhere(value);
        where.AND(value);
        return where;
    }

    /// <summary>
    /// Starts a SQL WHERE clause using a <see cref="char"/> as the left operand of the first condition
    /// </summary>
    public override SqlStatement WHERE(char value)
    {
        var where = MakeWhere(value);
        where.AND(value);
        return where;
    }

    /// <summary>
    /// Starts a SQL WHERE clause using a <see cref="decimal"/> as the left operand of the first condition
    /// </summary>
    public override SqlStatement WHERE(decimal value)
    {
        var where = MakeWhere(value);
        where.AND(value);
        return where;
    }

    private TSQLWhereClause MakeWhere<T>(T value)
    {
        var where = new TSQLWhereClause();
        var parent = GetParentScope();

        if (parent.IsUpdate || parent.IsDelete)
            parent = parent.Parent;

        parent.Children.Add(where);
        return where;
    }
}
