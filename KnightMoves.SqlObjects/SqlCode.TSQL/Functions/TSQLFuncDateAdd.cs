using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using KnightMoves.Hierarchical;
using Newtonsoft.Json;

namespace KnightMoves.SqlObjects.SqlCode.TSQL
{
    public enum TSQLFuncDateAddParams { datePart, increment, dateExpression }

    /// <summary>
    /// Implements the creation of the SQL DateAdd function
    /// </summary>
    public class TSQLFuncDateAdd : TSQLStatement, ISqlFunctionDateAdd
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

        public void SetParameterValue(TSQLFuncDateAddParams paramName, ISqlQueryExpression paramValue) => SetParameterValue(paramName.ToString(), paramValue);

        /// <summary>
        /// This method sets a value for a specified parameter name. The <see cref="ISqlParameter"/> specified 
        /// by the <paramref name="paramName"/> parameter must be of the same <see cref="SqlDataType"/> as the
        /// <paramref name="paramValue"/> parameter's <see cref="SqlDataType"/>. If not an exception will be 
        /// thrown.
        /// </summary>
        public void SetParameterValue(string paramName, ISqlQueryExpression paramValue) => this.SetParameterValue(Parameters, paramName, paramValue);

        public TSQLFuncDateAdd()
        {
            Name = "DateAdd(datePart,increment,dateExpression)";
            Description = "Returns a specified date with the specified number interval added/subtracted to the specified DatePart of that date.";
            DataType = new TSQLDataType(SqlDbType.DateTime);
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
                ParameterName = "increment",
                ParameterDescription = "A positive or negative integer. If positive, it will add this number of 'DateParts' to the date. If negative it will subtract.",
                ParameterDataType = new TSQLDataType(SqlDbType.Int),
                DefaultValue = "0",
                Order = 2
            });


            Parameters.Add(new TSQLParameter
            {
                ParameterName = "dateExpression",
                ParameterDescription = "The date value that you want to add/subtract to/from.",
                ParameterDataType = new TSQLDataType(SqlDbType.DateTime),
                DefaultValue = "GETDATE()",
                Order = 3
            });
        }

        /// <summary>
        /// <para>
        /// Produces a T-SQL DateAdd function call that uses the 'datePart', 'increment', and 'dateExpression' parameters from the 
        /// <see cref="ISqlFunction.Parameters"/> collection to feed as the values to the DateAdd function call.
        /// </para>
        /// <code>
        /// DATEADD([datePart], [increment], [dateExpression])
        /// </code>
        /// </summary>
        public override string SQL()
        {
            this.DeDuplicateParameters();

            if (Parameters.Count < 3)
                throw new InvalidOperationException("Required TSQLParameters are missing from the Parameters collection.");

            string sql = "";

            if (Parent != null)
                sql += IndentString;

            sql += "DATEADD(";

            foreach (ISqlParameter parameter in Parameters.OrderBy(p => p.Order))
                sql += $"{parameter}, ";

            sql = sql.Remove(sql.Length - 2, 2);

            sql += ")";

            if (!string.IsNullOrEmpty(Alias))
                sql += $" AS [{Alias.SanitizeDelimitedValue()}]";

            return sql;
        }

    }
}
