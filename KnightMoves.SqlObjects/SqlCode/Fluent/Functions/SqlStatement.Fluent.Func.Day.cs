using System;

namespace KnightMoves.SqlObjects.SqlCode;

public abstract partial class SqlStatement : IFluentSqlDay
{
    public abstract SqlStatement DAY(DateTime dateExpression);
    public abstract SqlStatement DAY(string dateExpression);
    public abstract SqlStatement DAY(string multipartIdentifier, string column);
}
