using System;

namespace KnightMoves.SqlObjects.SqlCode
{
    public abstract partial class SqlStatement : IFluentSqlIsNotEqualTo
    {
        public abstract SqlStatement IsNotEqualTo();
        public SqlStatement IsNotEqualTo(string value) => IsNotEqualTo(string.Empty, value);
        public abstract SqlStatement IsNotEqualTo(string multiPartIdentifier, string value);
        public abstract SqlStatement IsNotEqualTo(int value);
        public abstract SqlStatement IsNotEqualTo(DateTime value);
        public abstract SqlStatement IsNotEqualTo(bool value);
        public abstract SqlStatement IsNotEqualTo(long value);
        public abstract SqlStatement IsNotEqualTo(Guid value);
        public abstract SqlStatement IsNotEqualTo(char value);
        public abstract SqlStatement IsNotEqualTo(decimal value);

    }
}
