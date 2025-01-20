namespace KnightMoves.SqlObjects.SqlCode;

public abstract partial class SqlStatement : IFluentSqlAdHoc 
{

    public abstract SqlStatement Script(string scriptCode);

    public abstract SqlStatement Literal(string literal);
}
