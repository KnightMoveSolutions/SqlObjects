using System;
using System.Data;

namespace KnightMoves.SqlObjects.SqlCode;

/// <summary>
/// This class implements the <see cref="KnightMoves.SqlObjects.SqlCode.SqlDataType"/> abstract class 
/// using the <see cref="System.Data.DbType"/> enum as the list of SQL Data Types to be used for the 
/// <see cref="DataType"/> abstract property.
/// </summary>
public class AnsiSqlDataType : SqlDataType
{
    private DbType _dataType;

    /// <summary>
    /// Sets the <see cref="System.Data.DbType"/> enum value ToString() as the <see cref="KnightMoves.SqlObjects.SqlCode.SqlDataType.DataType"/> property.
    /// </summary>
    /// <param name="dataType">The <see cref="System.Data.DbType"/> enum value that will be converted to a string to be returned as the <see cref="KnightMoves.SqlObjects.SqlCode.SqlDataType.DataType"/> property</param>
    public AnsiSqlDataType(DbType dataType)
    {
        _dataType = dataType;
    }

    /// <summary>
    /// Returns the <see cref="System.Data.DbType"/> enum value as a string.
    /// </summary>
    public override string DataType
    {
        get { return _dataType.ToString(); }

        set
        {
            if (!Enum.TryParse<DbType>(value, out _dataType))
                throw new Exception("The data type must be a valid string representation of a DbType enumeration value.");
        }
    }
}
