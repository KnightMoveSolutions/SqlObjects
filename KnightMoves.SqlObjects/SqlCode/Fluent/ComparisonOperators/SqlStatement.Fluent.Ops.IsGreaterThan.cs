using System;

namespace KnightMoves.SqlObjects.SqlCode
{
    public abstract partial class SqlStatement : IFluentSqlIsGreaterThan
    {
        public abstract SqlStatement IsGreaterThan();
        public SqlStatement IsGreaterThan(string value) => IsGreaterThan(string.Empty, value);
        public abstract SqlStatement IsGreaterThan(string multiPartIdentifier, string value);
        public abstract SqlStatement IsGreaterThan(int value);
        public abstract SqlStatement IsGreaterThan(DateTime value);
        public abstract SqlStatement IsGreaterThan(bool value);
        public abstract SqlStatement IsGreaterThan(long value);
        public abstract SqlStatement IsGreaterThan(Guid value);
        public abstract SqlStatement IsGreaterThan(char value);
        public abstract SqlStatement IsGreaterThan(decimal value);

    }
}
