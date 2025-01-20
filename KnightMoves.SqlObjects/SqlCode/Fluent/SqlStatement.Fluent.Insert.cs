using System;

namespace KnightMoves.SqlObjects.SqlCode;

public abstract partial class SqlStatement : IFluentSqlInsert
{
    public abstract SqlStatement INSERT();
    public abstract SqlStatement INTO(string table);
    public abstract SqlStatement INTO(string schema, string table);
    public abstract SqlStatement VALUES();
    public abstract SqlStatement VALUE(string value);
    public abstract SqlStatement VALUE(int value);
    public abstract SqlStatement VALUE(DateTime value);
    public abstract SqlStatement VALUE(bool value);
    public abstract SqlStatement VALUE(long value);
    public abstract SqlStatement VALUE(Guid value);
    public abstract SqlStatement VALUE(char value);
    public abstract SqlStatement VALUE(decimal value);

}
