using System;
using System.Collections.Generic;
using System.Data;

namespace KnightMoves.SqlObjects.SqlCode.TSQL;

public abstract partial class TSQLStatement : SqlStatement
{
    /// <summary>
    /// Starts a SQL SELECT query
    /// </summary>
    public override SqlStatement SELECT()
    {
        var scope = GetParentScope();

        var select = new TSQLSelect();

        scope.Children.Add(select);

        return select;
    }

    /// <summary>
    /// Outputs the asterisk used to represent all columns in a SELECT list (i.e. SELECT * )
    /// </summary>
    public override SqlStatement STAR()
    {
        var starLiteral = new TSQLLiteral { Value = "*", DataType = new TSQLDataType(SqlDbType.Char) };

        Children.Add(starLiteral);

        return this;
    }

    /// <summary>
    /// Adds the collection of <paramref name="columns"/> to the immediately preceding SELECT or ORDER BY statement
    /// </summary>
    public override SqlStatement COLUMNS(IEnumerable<ISqlColumn> columns)
    {
        if (!IsSelect && !IsOrderBy)
            throw new InvalidOperationException($"Error: Invoked from {GetType().Name}. A column list can only be added to a SELECT clause (i.e. {nameof(ISqlSelect)})");

        foreach (SqlStatement column in columns)
        {
            if (IsSelect)
            {
                Children.Add(column);
            }
            else if (IsOrderBy)
            {
                var orderBy = new TSQLOrderByExpression();
                orderBy.Children.Add(column);
                Children.Add(orderBy);
            }
        }

        var parent = GetParentScope();

        if (parent != null)
            return parent;

        return this;
    }

    /// <summary>
    /// Adds the specified column to the immediately preceding ORDER BY, UPDATE, or SELECT statement
    /// </summary>
    public override SqlStatement COLUMN(string name)
    {
        var parent = GetExpressionParent();

        if (parent != null && parent.IsOrderBy)
        {
            var expression = new TSQLOrderByExpression();
            
            expression.Children.Add(new TSQLColumn { ColumnName = name });

            parent.Children.Add(expression);

            return expression;
        }

        if (parent != null && !parent.IsUpdate)
        {
            var column = new TSQLColumn { ColumnName = name };
            parent.Children.Add(column);
            return column;
        }

        LeftMultiPartIdentifier = string.Empty;
        LeftOperandValue = name;

        parent = GetParentScope();

        if (parent != null)
            return parent;

        return this;
    }

    /// <summary>
    /// Adds the specified column to the immediately preceding ORDER BY, UPDATE, or SELECT statement
    /// </summary>
    public override SqlStatement COLUMN(string multiPartIdentifier, string name)
    {
        var parent = GetExpressionParent();

        if (parent !=null && parent.IsOrderBy)
        {
            var expression = new TSQLOrderByExpression();

            expression.Children.Add(
                new TSQLColumn 
                { 
                    MultiPartIdentifier = multiPartIdentifier, 
                    ColumnName = name 
                }
            );

            parent.Children.Add(expression);

            return expression;
        }

        if (parent != null && !parent.IsUpdate)
        {
            var column = new TSQLColumn { MultiPartIdentifier = multiPartIdentifier, ColumnName = name };
            parent.Children.Add(column);
            return column;
        }

        LeftMultiPartIdentifier = multiPartIdentifier;
        LeftOperandValue = name;

        parent = GetParentScope();

        if (parent != null)
            return parent;

        return this;
    }

    /// <summary>
    /// Adds the specified column to the immediately preceding ORDER BY, UPDATE, or SELECT statement with
    /// the specified <paramref name="alias"/> output with the AS keyword (i.e. AS [Alias] )
    /// </summary>
    public override SqlStatement COLUMN(string multiPartIdentifier, string name, string alias)
    {
        var parent = GetExpressionParent();

        if (parent == null || parent.IsUpdate || parent.IsOrderBy)
            return COLUMN(multiPartIdentifier, name); // Alias not valid in this situation so just ignore it

        var column = new TSQLColumn { MultiPartIdentifier = multiPartIdentifier, ColumnName = name, Alias = alias };

        parent.Children.Add(column);

        return column;
    }

    /// <summary>
    /// Adds the specified <paramref name="alias"/> to the immediately preceding query expression
    /// </summary>
    public override SqlStatement AS(string alias)
    {
        var qryExpression = this as ISqlQueryExpression;

        if (qryExpression == null)
            return this;

        qryExpression.Alias = alias;

        if (IsSubQuery)
            return Parent as SqlStatement;

        return this;
    }

}
