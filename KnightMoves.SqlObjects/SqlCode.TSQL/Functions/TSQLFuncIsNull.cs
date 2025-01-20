using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using KnightMoves.Hierarchical;
using Newtonsoft.Json;

namespace KnightMoves.SqlObjects.SqlCode.TSQL;

/// <summary>
/// Implements the creation of the SQL ISNULL() function.
/// </summary>
public class TSQLFuncIsNull : TSQLStatement, ISqlFunctionIsNull
{
    public enum TSQLFuncIsNullParams { checkExpression, replacementValue }

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

    public void SetParameterValue(TSQLFuncIsNullParams paramName, ISqlQueryExpression paramValue) => SetParameterValue(paramName.ToString(), paramValue);

    /// <summary>
    /// This method sets a value for a specified parameter name. The <see cref="ISqlParameter"/> specified 
    /// by the <paramref name="paramName"/> parameter must be of the same <see cref="SqlDataType"/> as the
    /// <paramref name="paramValue"/> parameter's <see cref="SqlDataType"/>. If not an exception will be 
    /// thrown.
    /// </summary>
    public void SetParameterValue(string paramName, ISqlQueryExpression paramValue) => this.SetParameterValue(Parameters, paramName, paramValue);

    public TSQLFuncIsNull() { }

    /// <summary>
    /// An ISNULL() function must return the same database as its checkExpression and its replaceExpression 
    /// specified by the constructor argument <paramref name="checkExpressionType"/>
    /// </summary>
    public TSQLFuncIsNull(SqlDbType checkExpressionType) : this(new TSQLDataType(checkExpressionType)) { }

    /// <summary>
    /// An ISNULL() function must return the same database as its checkExpression and its replaceExpression 
    /// specified by the constructor argument <paramref name="checkExpressionType"/>
    /// </summary>
    public TSQLFuncIsNull(TSQLDataType checkExpressionType)
    {
        Name = "ISNULL(checkExpression,replacementValue)";
        Description = "Replaces NULL with the specified replacement value";
        DataType = checkExpressionType;
        IsAggregate = false;

        Parameters.Add(new TSQLParameter
        {
            ParameterName = "checkExpression",
            ParameterDescription = "Is the expression to be checked for NULL",
            ParameterDataType = checkExpressionType,
            Order = 1
        });

        Parameters.Add(new TSQLParameter
        {
            ParameterName = "replacementValue",
            ParameterDescription = "The expression to be returned if check_expression is NULL",
            ParameterDataType = checkExpressionType,
            Order = 2
        });
    }

    /// <summary>
    /// <para>
    /// Produces a T-SQL ISNULL() function call that uses the 'checkExpression' parameter and the 'replacementValue' 
    /// parameter from the <see cref="ISqlFunction.Parameters"/> collection to feed as the values to the ISNULL() function call.
    /// </para>
    /// <code>
    /// ISNULL([checkExpression], [replacementValue])
    /// </code>
    /// <code>
    /// ISNULL([Active], 0)
    /// </code>
    /// </summary>
    public override string SQL()
    {
        this.DeDuplicateParameters();

        if (Parameters.Count < 2)
            throw new InvalidOperationException("Required TSQLParameters are missing from the Parameters collection.");

        string sql = "";

        if (Parent != null)
            sql += IndentString;

        sql += "ISNULL(";

        foreach (ISqlParameter parameter in Parameters.OrderBy(p => p.Order))
            sql += $"{parameter}, ";

        sql = sql.Remove(sql.Length - 2, 2);

        sql += ")";

        if (!string.IsNullOrEmpty(Alias))
            sql += $" AS [{Alias.SanitizeDelimitedValue()}]";

        return sql;
    }
}
