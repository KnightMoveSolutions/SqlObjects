using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KnightMoves.Hierarchical;
using Newtonsoft.Json;

namespace KnightMoves.SqlObjects.SqlCode.TSQL;

/// <summary>
/// <para>
/// This class implements <see cref="ISqlInListCondition"/> and builds a T-SQL IN (...) clause 
/// from the <see cref="ISqlQueryExpression"/> objects in the <see cref="ISqlInListCondition.InList"/> 
/// collection
/// </para>
/// <para>
/// [LeftOperand] IN ( <see cref="ISqlQueryExpression"/>1, <see cref="ISqlQueryExpression"/>2, <see cref="ISqlQueryExpression"/>N )
/// </para>
/// </summary>
public class TSQLInListCondition : TSQLStatement, ISqlInListCondition
{
    /// <summary>
    /// A collection of <see cref="ISqlQueryExpression"/> objects that represent the list of values in the IN SQL clause.
    /// </summary>
    [JsonProperty(ItemConverterType = typeof(TreeNodeJsonConverter))]
    public List<ISqlQueryExpression> InList { get; set; } = new List<ISqlQueryExpression>();

    /// <summary>
    /// <para>
    /// Returns the <see cref="ISqlQueryExpression"/> objects in the <see cref="ISqlInListCondition.InList"/> collection in a T-SQL IN (...) clause
    /// </para>
    /// <para>
    /// [LeftOperand] IN ( <see cref="ISqlQueryExpression"/>1, <see cref="ISqlQueryExpression"/>2, <see cref="ISqlQueryExpression"/>N )
    /// </para>
    /// </summary>
    public override string SQL()
    {
        if (!InList.Any())
            throw new InvalidOperationException("Cannot produce valid SQL for the TSQLInListCondition object.  The InList collection must have one or more SqlQueryExpression objects to produce as a list.");

        var leftOperand = Children.FirstOrDefault(c => c is ISqlQueryExpression);

        var sql = new StringBuilder();

        sql.Append($"{leftOperand.ToString().Trim()} IN (");

        InList.ForEach(e => sql.Append($"{e},"));

        sql.Remove(sql.Length - 1, 1);

        sql.Append(")");

        return sql.ToString();
    }
}
