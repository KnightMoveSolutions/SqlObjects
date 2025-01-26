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
