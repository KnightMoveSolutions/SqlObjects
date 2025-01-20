using System;

namespace KnightMoves.SqlObjects.SqlCode;

public abstract partial class SqlStatement : IFluentSqlMonth
{
    public abstract SqlStatement MONTH(DateTime dateExpression);
    public abstract SqlStatement MONTH(string dateExpression);
    public abstract SqlStatement MONTH(string multipartIdentifier, string column);
}
