using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using KnightMoves.Hierarchical;
using Newtonsoft.Json;

namespace KnightMoves.SqlObjects.SqlCode.TSQL;

public enum TSQLFuncSumParams { aggregateExpression }

/// <summary>
/// Implements the creation of the SQL Sum function
/// </summary>
public class TSQLFuncSum : TSQLStatement, ISqlFunctionSum
{
    /// <summary>
    /// The alias of the SQL expression so it can be referenced easily in other SQL fragments.
    /// </summary>
    public string Alias { get; set; }

    /// <summary>
    /// A <see cref="SqlDataType"/> object that represents the return data type of the SQL function
    /// </summary>
    public SqlDataType DataType { get; set; }

    /// <summary>
    /// A collection of <see cref="ISqlParameter"/> objects that define the parameters 
    /// accepted by the SQL function
    /// </summary>
    [JsonProperty(ItemConverterType = typeof(TreeNodeJsonConverter))]
    public List<ISqlParameter> Parameters { get; private set; } = new List<ISqlParameter>();

    /// <summary>
    /// True if the SQL function represents an aggregate function that requires a GROUP BY clause 
    /// in order to mix the function with other non-aggregate SQL expressions (e.g. columns) in 
    /// a SELECT list. Examples of aggregate SQL functions include SUM(), AVG(), etc.
    /// </summary>
    public bool IsAggregate { get; set; }

    public void SetParameterValue(TSQLFuncSumParams paramName, ISqlQueryExpression paramValue) => SetParameterValue(paramName.ToString(), paramValue);

    /// <summary>
    /// This method sets a value for a specified parameter name. The <see cref="ISqlParameter"/> specified 
    /// by the <paramref name="paramName"/> parameter must be of the same <see cref="SqlDataType"/> as the
    /// <paramref name="paramValue"/> parameter's <see cref="SqlDataType"/>. If not an exception will be 
    /// thrown.
    /// </summary>
    public void SetParameterValue(string paramName, ISqlQueryExpression paramValue) => this.SetParameterValue(Parameters, paramName, paramValue);

    public TSQLFuncSum()
    {
        Name = "Sum(aggregateExpression)";
        Description = "Returns the sum of all the values in the expression. Null values are ignored.";
        DataType = new TSQLDataType(SqlDbType.Decimal);
        IsAggregate = true;

        Parameters.Add(new TSQLParameter
        {
            ParameterName = "aggregateExpression",
            ParameterDescription = "An expression that represents a set of values to be summed.  A 'Column' is the most likely type of expression.",
            ParameterDataType = new TSQLDataType(SqlDbType.Variant),
            IsVariant = true,
            DefaultValue = "0",
            Order = 1
        });
    }

    /// <summary>
    /// <para>
    /// Produces a T-SQL Sum function call that uses the 'expression' parameter from the 
    /// <see cref="ISqlFunction.Parameters"/> collection to 
    /// feed as the value to the Sum function call.
    /// </para>
    /// <code>
    /// SUM([expression])
    /// </code>
    /// </summary>
    public override string SQL()
    {
        this.DeDuplicateParameters();

        if (Parameters.Count < 1)
            throw new InvalidOperationException("Missing required parameters from the Parameters collection.");

        string sql = "";

        if (Parent != null)
            sql += IndentString;

        sql += $"SUM({Parameters.First()})";

        if (!string.IsNullOrEmpty(Alias))
            sql += $" AS [{Alias.SanitizeDelimitedValue()}]";

        return sql;
    }
}
