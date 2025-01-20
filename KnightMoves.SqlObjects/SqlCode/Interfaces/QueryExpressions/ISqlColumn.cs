using System.Data;

namespace KnightMoves.SqlObjects.SqlCode;

public interface ISqlColumn : ISqlQueryExpression
{
    string MultiPartIdentifier { get; set; }
    string ColumnName { get; set; }

    SqlStatement TYPE(SqlDbType sqlDataType);
}