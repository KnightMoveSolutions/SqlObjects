using System;

namespace KnightMoves.SqlObjects.SqlCode
{
    public abstract partial class SqlStatement : IFluentSqlNullCheck
    {
        public abstract SqlStatement ISNULL(string multipartIdentifier, string column, string replaceExpression);
        public abstract SqlStatement ISNULL(string multipartIdentifier, string column, int replaceExpression);
        public abstract SqlStatement ISNULL(string multipartIdentifier, string column, DateTime replaceExpression);
        public abstract SqlStatement ISNULL(string multipartIdentifier, string column, bool replaceExpression);
        public abstract SqlStatement ISNULL(string multipartIdentifier, string column, long replaceExpression);
        public abstract SqlStatement ISNULL(string multipartIdentifier, string column, Guid replaceExpression);
        public abstract SqlStatement ISNULL(string multipartIdentifier, string column, char replaceExpression);
        public abstract SqlStatement ISNULL(string multipartIdentifier, string column, decimal replaceExpression);

        public abstract SqlStatement ISNULL(string checkExpression, string replaceExpression);
        public abstract SqlStatement ISNULL(string checkExpression, int replaceExpression);
        public abstract SqlStatement ISNULL(string checkExpression, DateTime replaceExpression);
        public abstract SqlStatement ISNULL(string checkExpression, bool replaceExpression);
        public abstract SqlStatement ISNULL(string checkExpression, long replaceExpression);
        public abstract SqlStatement ISNULL(string checkExpression, Guid replaceExpression);
        public abstract SqlStatement ISNULL(string checkExpression, char replaceExpression);
        public abstract SqlStatement ISNULL(string checkExpression, decimal replaceExpression);

        public abstract SqlStatement ISNULL(ISqlQueryExpression checkExpression, string replaceExpression);
        public abstract SqlStatement ISNULL(ISqlQueryExpression checkExpression, int replaceExpression);
        public abstract SqlStatement ISNULL(ISqlQueryExpression checkExpression, DateTime replaceExpression);
        public abstract SqlStatement ISNULL(ISqlQueryExpression checkExpression, bool replaceExpression);
        public abstract SqlStatement ISNULL(ISqlQueryExpression checkExpression, long replaceExpression);
        public abstract SqlStatement ISNULL(ISqlQueryExpression checkExpression, Guid replaceExpression);
        public abstract SqlStatement ISNULL(ISqlQueryExpression checkExpression, char replaceExpression);
        public abstract SqlStatement ISNULL(ISqlQueryExpression checkExpression, decimal replaceExpression);

        public abstract SqlStatement ISNULL(ISqlQueryExpression checkExpression, ISqlQueryExpression replaceExpression);
    }
}
