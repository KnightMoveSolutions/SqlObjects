﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using KnightMoves.Hierarchical;
using Newtonsoft.Json;

namespace KnightMoves.SqlObjects.SqlCode.TSQL;

public enum TSQLFuncDayParams { dateExpression }

/// <summary>
/// Implements the creation of the SQL Day function
/// </summary>
public class TSQLFuncDay : TSQLStatement, ISqlFunctionDay
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

    public void SetParameterValue(TSQLFuncDayParams paramName, ISqlQueryExpression paramValue) => SetParameterValue(paramName.ToString(), paramValue);

    /// <summary>
    /// This method sets a value for a specified parameter name. The <see cref="ISqlParameter"/> specified 
    /// by the <paramref name="paramName"/> parameter must be of the same <see cref="SqlDataType"/> as the
    /// <paramref name="paramValue"/> parameter's <see cref="SqlDataType"/>. If not an exception will be 
    /// thrown.
    /// </summary>
    public void SetParameterValue(string paramName, ISqlQueryExpression paramValue) => this.SetParameterValue(Parameters, paramName, paramValue);

    public TSQLFuncDay()
    {
        Name = "Day(dateExpression)";
        Description = "Returns an integer representing the day (day of the month) of the specified date.";
        DataType = new TSQLDataType(SqlDbType.Int);
        IsAggregate = false;

        Parameters.Add(new TSQLParameter
        {
            ParameterName = "dateExpression",
            ParameterDescription = "The date where the Day value is derived from.",
            ParameterDataType = new TSQLDataType(SqlDbType.DateTime),
            DefaultValue = "GETDATE()",
            Order = 1
        });

    }

    /// <summary>
    /// <para>
    /// Produces a T-SQL DatePart function call that uses the 'dateExpression' parameter from the 
    /// <see cref="ISqlFunction.Parameters"/> collection to feed as the values to the 
    /// Day function call.
    /// </para>
    /// <code>
    /// DAY([[dateExpression])
    /// </code>
    /// <code>
    /// DAY(GetDate())
    /// </code>
    /// </summary>
    public override string SQL()
    {
        this.DeDuplicateParameters();

        if (Parameters.Count < 1)
            throw new InvalidOperationException("Missing required parameters from the Parameters collection.");

        string sql = "";

        if (Parent != null)
            sql += this.IndentString;

        sql += $"DAY({Parameters.First()})";

        if (!string.IsNullOrEmpty(Alias))
            sql += $" AS [{Alias.SanitizeDelimitedValue()}]";

        return sql;
    }
}
