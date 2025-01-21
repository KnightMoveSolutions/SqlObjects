using System;
using System.Collections.Generic;
using System.Linq;

namespace KnightMoves.SqlObjects.SqlCode.TSQL;

public abstract partial class TSQLStatement : SqlStatement
{
    /// <summary>
    /// Starts a SQL ORDER BY clause
    /// </summary>
    public override SqlStatement ORDERBY()
    {
        var orderBy = new TSQLOrderBy();
        var parent = GetParentScope();
        parent.Children.Add(orderBy);
        return orderBy;
    }

    /// <summary>
    /// Outputs a SQL ORDER BY clause using <paramref name="orderByExpression"/> as the first sorting 
    /// specification. Ascending (ASC) is the default direction.
    /// </summary>
    public override SqlStatement ORDERBY(string orderByExpression)
    {
        var expression = new TSQLOrderByExpression { Direction = SqlOrderByDirections.ASC };

        expression.Children.Add(new TSQLColumn { ColumnName = orderByExpression });

        var orderBy = new TSQLOrderBy();

        orderBy.Children.Add(expression);

        var parent = GetParentScope();

        parent.Children.Add(orderBy);

        return orderBy;
    }

    /// <summary>
    /// Outputs a SQL ORDER BY clause using a column as the first sorting 
    /// specification. Ascending (ASC) is the default direction.
    /// </summary>
    public override SqlStatement ORDERBY(string multipartIdentifier, string column)
    {
        var expression = new TSQLOrderByExpression { Direction = SqlOrderByDirections.ASC };

        expression.Children.Add(
            new TSQLColumn 
            { 
                MultiPartIdentifier = multipartIdentifier, 
                ColumnName = column 
            }
        );

        var orderBy = new TSQLOrderBy();

        orderBy.Children.Add(expression);

        var parent = GetParentScope();

        parent.Children.Add(orderBy);

        return orderBy;
    }

    /// <summary>
    /// Outputs a SQL ORDER BY clause using an <see cref="ISqlQueryExpression"/> object as the first sorting 
    /// specification. Ascending (ASC) is the default direction.
    /// </summary>
    public override SqlStatement ORDERBY(ISqlQueryExpression orderByExpression)
    {
        var expression = new TSQLOrderByExpression { Direction = SqlOrderByDirections.ASC };

        expression.Children.Add(orderByExpression as SqlStatement);

        var orderBy = new TSQLOrderBy();

        orderBy.Children.Add(expression);

        var parent = GetParentScope();

        parent.Children.Add(orderBy);

        return orderBy;
    }

    /// <summary>
    /// Outputs a SQL ORDER BY clause using a collection of <see cref="ISqlQueryExpression"/> objects that provide
    /// multiple sort expressions at one time. Ascending (ASC) is the default direction.
    /// </summary>
    public override SqlStatement ORDERBY(IEnumerable<ISqlOrderByExpression> orderByExpressions)
    {
        var orderBy = new TSQLOrderBy();

        foreach (var expression in orderByExpressions)
            orderBy.Children.Add(expression as SqlStatement);

        var parent = GetParentScope();

        parent.Children.Add(orderBy);

        return orderBy;
    }

    /// <summary>
    /// Applies the ascending sort direction (ASC) to the immediately preceding order by expression
    /// </summary>
    public override SqlStatement ASC()
    {
        var parent = GetExpressionParent();

        if (!parent.IsOrderBy)
            throw new InvalidOperationException("ASC() can be used only within the scope of an ORDER BY clause");

        var expression = parent.Children.LastOrDefault(e => e.IsOrderByExpression) as ISqlOrderByExpression;

        if (expression == null)
            return parent;

        expression.Direction = SqlOrderByDirections.ASC;

        return parent;
    }

    /// <summary>
    /// Applies the descending sort direction (DESC) to the immediately preceding order by expression
    /// </summary>
    public override SqlStatement DESC()
    {
        var parent = GetExpressionParent();

        if (!parent.IsOrderBy)
            throw new InvalidOperationException("DESC() can be used only within the scope of an ORDER BY clause");

        var expression = parent.Children.LastOrDefault(e => e.IsOrderByExpression) as ISqlOrderByExpression;

        if (expression == null)
            return parent;

        expression.Direction = SqlOrderByDirections.DESC;

        return parent;
    }
}
