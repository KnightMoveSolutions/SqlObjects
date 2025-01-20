using System;

namespace KnightMoves.SqlObjects.SqlCode;

public abstract partial class SqlStatement : IFluentSqlWhere
{
    public abstract SqlStatement WHERE();

    public abstract SqlStatement WHERE(ISqlLiteral leftLiteral);

    public abstract SqlStatement WHERE(string operand);

    public abstract SqlStatement WHERE(string multiPartIdentifier, string operand);

    public abstract SqlStatement WHERE(int value);

    public abstract SqlStatement WHERE(DateTime value);

    public abstract SqlStatement WHERE(bool value);

    public abstract SqlStatement WHERE(long value);

    public abstract SqlStatement WHERE(Guid value);

    public abstract SqlStatement WHERE(char value);

    public abstract SqlStatement WHERE(decimal value);

}
