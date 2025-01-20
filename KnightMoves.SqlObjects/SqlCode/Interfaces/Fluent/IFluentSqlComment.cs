using System.Collections.Generic;

namespace KnightMoves.SqlObjects.SqlCode;

public interface IFluentSqlComment
{
    SqlStatement Comment(string comment, bool singleLineOnly = false);
    SqlStatement Comment(IEnumerable<string> comment, bool singleLineOnly = false);
}
