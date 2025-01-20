using System.Collections.Generic;

namespace KnightMoves.SqlObjects.SqlCode;

public interface IFluentSqlSelectItems
{
    SqlStatement SELECT();
    SqlStatement STAR();
    SqlStatement COLUMNS(IEnumerable<ISqlColumn> columns);
    SqlStatement COLUMN(string name);
    SqlStatement COLUMN(string multiPartIdentifier, string name);
    SqlStatement COLUMN(string multiPartIdentifier, string name, string alias);
    SqlStatement AS(string alias);
}
