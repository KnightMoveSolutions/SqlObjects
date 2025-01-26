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
