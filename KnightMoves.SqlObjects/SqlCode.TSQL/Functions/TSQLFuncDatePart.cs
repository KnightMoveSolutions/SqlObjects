using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using KnightMoves.Hierarchical;
using Newtonsoft.Json;

namespace KnightMoves.SqlObjects.SqlCode.TSQL;

public enum TSQLFuncDatePartParams { datePart, dateExpression }

/// <summary>
/// Implements the creation of the SQL DatePart function
/// </summary>
public class TSQLFuncDatePart : TSQLStatement, ISqlFunctionDatePart
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

    public void SetParameterValue(TSQLFuncDatePartParams paramName, ISqlQueryExpression paramValue) => SetParameterValue(paramName.ToString(), paramValue);

    /// <summary>
    /// This method sets a value for a specified parameter name. The <see cref="ISqlParameter"/> specified 
    /// by the <paramref name="paramName"/> parameter must be of the same <see cref="SqlDataType"/> as the
    /// <paramref name="paramValue"/> parameter's <see cref="SqlDataType"/>. If not an exception will be 
    /// thrown.
    /// </summary>
    public void SetParameterValue(string paramName, ISqlQueryExpression paramValue) => this.SetParameterValue(Parameters, paramName, paramValue);

    /// <summary>
    /// Default constructor
    /// </summary>
    public TSQLFuncDatePart()
    {
        Name = "DatePart(datePart,dateExpression)";
        Description = "Returns an integer that represents the specified datePart of the specified dateExpression.";
        DataType = new TSQLDataType(SqlDbType.Int);
        IsAggregate = false;

        Parameters.Add(new TSQLParameter
        {
            ParameterName = "datePart",
            ParameterDescription = "A valid DatePart specification (e.g. Year, Quarter, Month, etc.)",
            ParameterDataType = new TSQLDataType(SqlDbType.Variant),
            DefaultValue = "Day",
            EnumType = typeof(DateParts).AssemblyQualifiedName,
            Order = 1
        });

        Parameters.Add(new TSQLParameter
        {
            ParameterName = "dateExpression",
            ParameterDescription = "The date that the value of the datePart is derived from.",
            ParameterDataType = new TSQLDataType(SqlDbType.DateTime),
            DefaultValue = "GETDATE()",
            Order = 2
        });

    }

    /// <summary>
    /// <para>
    /// Produces a T-SQL DatePart function call that uses the 'datePart' and 'dateExpression' parameters from the 
    /// <see cref="ISqlFunction.Parameters"/> collection to feed as the values to the 
    /// DatePart function call.
    /// </para>
    /// <code>
    /// DATEPART([datePart], [dateExpression])
    /// </code>
    /// <code>
    /// DATEPART(Day, GetDate())
    /// </code>
    /// </summary>
    public override string SQL()
    {
        this.DeDuplicateParameters();

        if (Parameters.Count < 2)
            throw new InvalidOperationException("Missing required parameters from the Parameters collection.");

        string sql = "";

        if (Parent != null)
            sql += IndentString;

        sql += "DATEPART(";

        foreach (ISqlParameter parameter in Parameters.OrderBy(p => p.Order))
            sql += $"{parameter}, ";

        sql = sql.Remove(sql.Length - 2, 2);

        sql += ")";

        if (!string.IsNullOrEmpty(Alias))
            sql += $" AS [{Alias.SanitizeDelimitedValue()}]";

        return sql;
    }
}
