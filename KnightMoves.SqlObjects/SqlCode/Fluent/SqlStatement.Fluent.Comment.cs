using System.Collections.Generic;

namespace KnightMoves.SqlObjects.SqlCode
{
    public abstract partial class SqlStatement : IFluentSqlComment
    {
        public abstract SqlStatement Comment(string comment, bool singleLineOnly = false);
        public abstract SqlStatement Comment(IEnumerable<string> comment, bool singleLineOnly = false);
    }
}
