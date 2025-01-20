using System.Collections.Generic;

namespace KnightMoves.SqlObjects.SqlCode.TSQL;

public abstract partial class TSQLStatement : SqlStatement
{
    /// <summary>
    /// Starts a SQL GROUP BY clause
    /// </summary>
    /// <returns></returns>
    public override SqlStatement GROUPBY()
    {
        var groupBy = new TSQLGroupBy();
        var parent = GetParentScope();
        parent.Children.Add(groupBy);
        return groupBy;
    }

    /// <summary>
    /// Outputs a SQL GROUP BY clause using the <paramref name="columns"/> collection as the grouping specification.
    /// Columns can represent query expressions that have been aliased with a name where the alias becomes a column name.
    /// </summary>
    public override SqlStatement GROUPBY(IEnumerable<ISqlColumn> columns)
    {
        var groupBy = new TSQLGroupBy();

        foreach(SqlStatement column in columns)
            groupBy.Children.Add(column);

        var parent = GetParentScope();

        parent.Children.Add(groupBy);

        return parent;
    }

    /// <summary>
    /// Outputs a SQL GROUP BY clause using the <paramref name="groupItems"/> collection as the grouping specification.
    /// Each of the <paramref name="groupItems"/> is a raw <see cref="ISqlQueryExpression"/> and can be any valid 
    /// grouping query expression (e.g. a calculation)
    /// </summary>
    public override SqlStatement GROUPBY(IEnumerable<ISqlQueryExpression> groupItems)
    {
        var groupBy = new TSQLGroupBy();

        foreach (SqlStatement item in groupItems)
            groupBy.Children.Add(item);

        var parent = GetParentScope();

        parent.Children.Add(groupBy);

        return parent;
    }
}
