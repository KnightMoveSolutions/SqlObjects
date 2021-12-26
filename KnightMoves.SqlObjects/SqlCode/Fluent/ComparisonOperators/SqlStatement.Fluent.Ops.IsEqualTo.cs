using System;

namespace KnightMoves.SqlObjects.SqlCode
{
    public abstract partial class SqlStatement : IFluentSqlIsEqualTo
    {
        public abstract SqlStatement IsEqualTo();
        public SqlStatement IsEqualTo(string value) => IsEqualTo(string.Empty, value);
        public abstract SqlStatement IsEqualTo(string multiPartIdentifier, string value);
        public abstract SqlStatement IsEqualTo(int value);
        public abstract SqlStatement IsEqualTo(DateTime value);
        public abstract SqlStatement IsEqualTo(bool value);
        public abstract SqlStatement IsEqualTo(long value);
        public abstract SqlStatement IsEqualTo(Guid value);
        public abstract SqlStatement IsEqualTo(char value);
        public abstract SqlStatement IsEqualTo(decimal value);
    }
}
