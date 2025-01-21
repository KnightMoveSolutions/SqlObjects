using KnightMoves.SqlObjects.SqlCode.Security;
using System;
using System.Data;

namespace KnightMoves.SqlObjects.SqlCode.TSQL;

/// <summary>
/// This class implements <see cref="ISqlLiteral"/>. 
/// It returns the <see cref="ISqlLiteral.Value"/> as-is (i.e. literal) 
/// and will surround it with single quotes per T-SQL syntax rules if the <see cref="DataType"/> 
/// indicates that it is required.
/// </summary>
public class TSQLLiteral : TSQLStatement, ISqlLiteral
{
    /// <summary>
    /// The alias of the SQL expression so it can be referenced easily in other SQL fragments.
    /// </summary>
    public string Alias { get; set; }

    /// <summary>
    /// The data type of the literal value
    /// </summary>
    public SqlDataType DataType { get; set; }

    /// <summary>
    /// The literal value
    /// </summary>
    public string Value { get; set; }

    /// <summary>
    /// Returns true if the <see cref="ISqlLiteral.Value"/> should be surrounded 
    /// in single quotes. False if not. The decision is based on examining the value of the 
    /// <see cref="DataType"/> property.
    /// </summary>
    public bool IsQuoted
    {
        get
        {
            if (
                DataType is null || 
                (
                    !string.IsNullOrEmpty(Value) 
                    && 
                    (
                        Value.IsParameter() ||
                        Value.IsDelimited()
                    )
                )
               )
                return false;

            Enum.TryParse(((SqlDataType)DataType).DataType, out SqlDbType dbType);

            return
                (
                    dbType == SqlDbType.Char ||
                    dbType == SqlDbType.Date ||
                    dbType == SqlDbType.DateTime ||
                    dbType == SqlDbType.DateTime2 ||
                    dbType == SqlDbType.DateTimeOffset ||
                    dbType == SqlDbType.NChar ||
                    dbType == SqlDbType.NText ||
                    dbType == SqlDbType.NVarChar ||
                    dbType == SqlDbType.SmallDateTime ||
                    dbType == SqlDbType.Text ||
                    dbType == SqlDbType.Time ||
                    dbType == SqlDbType.Timestamp ||
                    dbType == SqlDbType.UniqueIdentifier ||
                    dbType == SqlDbType.VarChar
                )
                && Value != "*";
        }
    }

    /// <summary>
    /// <para>
    /// Returns the <see cref="ISqlLiteral.Value"/> as-is (e.g. literal) and will surround it with single quotes 
    /// per T-SQL syntax rules if the <see cref="DataType"/> indicates that it is required. 
    /// </para>
    /// <code>
    /// Examples: 999 or '2014-01-01 00:00:00' 
    /// </code>
    /// </summary>
    public override string SQL()
    {
        var securityFilter = SQLSecurityFactory.Create();

        // Here we sanitize the value protecting the resulting SQL from SQL Injection attacks
        var safeValue = securityFilter.SanitizeInput(GetNativeDataType(), Value);

        string sql = "";

        if (Parent != null && Parent.GetType() != typeof(TSQLCaseWhen))
            sql += IndentString;

        if (IsQuoted) sql += "'";
        
        sql += safeValue;

        if (IsQuoted) sql += "'";

        return sql;
    }

    private Type GetNativeDataType()
    {
        Enum.TryParse((DataType).DataType, out SqlDbType dbType);

        if (
            dbType == SqlDbType.Char || 
            dbType == SqlDbType.NChar || 
            dbType == SqlDbType.NText || 
            dbType == SqlDbType.NVarChar || 
            dbType == SqlDbType.Text || 
            dbType == SqlDbType.VarChar 
           )
            return typeof(string);

        if (dbType == SqlDbType.Int)
            return typeof(int);

        if (dbType == SqlDbType.BigInt)
            return typeof(long);

        if (dbType == SqlDbType.Decimal)
            return typeof(decimal);

        if (dbType == SqlDbType.Bit)
            return typeof(bool);

        if (dbType == SqlDbType.UniqueIdentifier)
            return typeof(Guid);

        if (
            dbType == SqlDbType.DateTime || 
            dbType == SqlDbType.DateTime2 || 
            dbType == SqlDbType.Date || 
            dbType == SqlDbType.DateTimeOffset ||
            dbType == SqlDbType.SmallDateTime
           )
            return typeof(DateTime);

        // Causes value to be ignored by the SQL sanitizer, which is intended for situations where raw SQL is to
        // be passed. This bypasses security and is dangerous but no different than concatenating strings if the
        // dev really needs to include custom SQL. Dev can craft custom SQL attheir own peril.
        if (dbType == SqlDbType.Variant)
            return typeof(DBNull);

        // Default to string
        return typeof(string);
    }
}
