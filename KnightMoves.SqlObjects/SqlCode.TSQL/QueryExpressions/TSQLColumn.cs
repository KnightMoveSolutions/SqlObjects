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

using System;
using System.Data;

namespace KnightMoves.SqlObjects.SqlCode.TSQL;

/// <summary>
/// This class implements <see cref="ISqlColumn"/> and builds a SQL Server database column reference as a string
/// <code>
/// [Schema].[TableName].[ColumnName]
/// </code>
/// </summary>
public class TSQLColumn : TSQLStatement, ISqlColumn
{
    private SqlDataType _dataType;

    /// <summary>
    /// The SQL Server database schema. (e.g. dbo)
    /// </summary>
    public string Schema { get; set; }

    /// <summary>
    /// The name of the SQL Server table that the column belongs to. 
    /// </summary>
    public string TableName { get; set; }

    /// <summary>
    /// The multi-part identifier of the column (e.g. c.CustomerID whre c is the multi-part identifier)
    /// </summary>
    public string MultiPartIdentifier { get; set; }

    /// <summary>
    /// The name of a column in a table, view, or sub-query
    /// </summary>
    public string ColumnName { get; set; }

    /// <summary>
    /// The alias of the SQL expression so it can be referenced easily in other SQL fragments.
    /// </summary>
    public string Alias { get; set; }

    /// <summary>
    /// The data type of the column
    /// </summary>
    public SqlDataType DataType
    {
        get { return _dataType; }
        set
        {
            if (value == null)
                return;

            if (!Enum.TryParse(value.DataType, out SqlDbType valTest))
                throw new ArgumentException("The input value '" + value + "' is invalid. The DataType must be a valid string representation of SqlDbType.");

            _dataType = value;
        }
    }

    /// <summary>
    /// Adds the specified alias to the column
    /// </summary>
    public override SqlStatement AS(string alias)
    {
        Alias = alias;
        return this;
    }

    public SqlStatement TYPE(SqlDbType sqlDataType)
    {
        DataType = new TSQLDataType(sqlDataType);
        return this;
    }

    /// <summary>
    /// <para>
    /// Returns a SQL Server database column reference as a string in the following form.
    /// </para>
    /// <code>
    /// [Schema].[TableName].[ColumnName]
    /// </code>
    /// <code>
    /// [dbo].[Customers].[FirstName]
    /// </code>
    /// </summary>
    public override string SQL()
    {
        string sql = "";

        if (Parent != null && Parent.GetType() != typeof(TSQLCaseWhen))
            sql += IndentString;

        if (!string.IsNullOrEmpty(MultiPartIdentifier))
        {
            sql += $"[{MultiPartIdentifier.SanitizeDelimitedValue()}].";
        }
        else if (!string.IsNullOrEmpty(TableName))
        {
            // The schema is only relevant if a table name has been specified
            if (!string.IsNullOrEmpty(Schema))
                sql += $"[{Schema.SanitizeDelimitedValue()}].";

            sql += $"[{TableName.SanitizeDelimitedValue()}].";
        }

        sql += $"[{ColumnName.SanitizeDelimitedValue()}]";

        if (!string.IsNullOrEmpty(Alias))
            sql += $" AS [{Alias.SanitizeDelimitedValue()}]";

        return sql;
    }
}
