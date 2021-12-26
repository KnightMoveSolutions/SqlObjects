using System;

namespace KnightMoves.SqlObjects.SqlCode
{
    public abstract partial class SqlStatement : IFluentSqlHaving
    {
        public abstract SqlStatement HAVING();

        public abstract SqlStatement HAVING(ISqlLiteral leftLiteral);

        public abstract SqlStatement HAVING(string operand);

        public abstract SqlStatement HAVING(string multiPartIdentifier, string operand);

        public abstract SqlStatement HAVING(int value);

        public abstract SqlStatement HAVING(DateTime value);

        public abstract SqlStatement HAVING(bool value);

        public abstract SqlStatement HAVING(long value);

        public abstract SqlStatement HAVING(Guid value);

        public abstract SqlStatement HAVING(char value);

        public abstract SqlStatement HAVING(decimal value);

    }
}
