namespace KnightMoves.SqlObjects.SqlCode;

public interface IFluentSqlAdHoc
{
    SqlStatement Script(string scriptCode);
    SqlStatement Literal(string literal);
}
