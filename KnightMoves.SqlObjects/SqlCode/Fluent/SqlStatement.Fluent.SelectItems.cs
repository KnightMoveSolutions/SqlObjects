using System.Collections.Generic;

namespace KnightMoves.SqlObjects.SqlCode
{
    public abstract partial class SqlStatement : IFluentSqlSelectItems
    {
        public abstract SqlStatement SELECT();

        public abstract SqlStatement STAR();

        public abstract SqlStatement COLUMNS(IEnumerable<ISqlColumn> columns);

        public abstract SqlStatement COLUMN(string name);

        public abstract SqlStatement COLUMN(string multiPartIdentifier, string name);

        public abstract SqlStatement COLUMN(string multiPartIdentifier, string name, string alias);

        public abstract SqlStatement AS(string alias);

    }
}
