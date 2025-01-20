using System;
using System.Text;

namespace KnightMoves.SqlObjects.SqlCode.TSQL;

/// <summary>
/// This class implements <see cref="ISqlDelete"/> and builds a SQL DELETE FROM statement
/// </summary>
public class TSQLDelete : TSQLStatement, ISqlDelete
{
    /// <summary>
    /// The schema of the Table to delete from
    /// </summary>
    public string Schema { get; set; }

    /// <summary>
    /// The name of the Table to delete from
    /// </summary>
    public string Table { get; set; }

    public TSQLDelete() { }

    public TSQLDelete(string table) : this(string.Empty, table) { }

    public TSQLDelete(string schema, string table)
    {
        Schema = schema;
        Table = table;
    }

    /// <summary>
    /// Builds the SQL DELETE FROM statement using the Schema (if provided) and Table 
    /// properties to specify the table to delete from.
    /// </summary>
    public override string SQL()
    {
        var sql = new StringBuilder();

        sql.Append($"{IndentString}DELETE FROM ");

        if (!string.IsNullOrEmpty(Schema))
            sql.Append($"[{Schema.SanitizeDelimitedValue()}].");

        sql.Append($"[{Table.SanitizeDelimitedValue()}]{Environment.NewLine}");

        return sql.ToString();
    }
}
