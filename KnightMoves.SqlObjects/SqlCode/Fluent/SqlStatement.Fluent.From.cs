namespace KnightMoves.SqlObjects.SqlCode;

public abstract partial class SqlStatement : IFluentSqlFrom
{
    public abstract SqlStatement FROM();

    public abstract SqlStatement FROM(string table);

    public abstract SqlStatement FROM(string schema, string table);

    public abstract SqlStatement FROM(string schema, string table, string multipartIdentifier);

}
