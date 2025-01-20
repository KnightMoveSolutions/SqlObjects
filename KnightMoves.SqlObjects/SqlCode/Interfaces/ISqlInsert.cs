namespace KnightMoves.SqlObjects.SqlCode;

public interface ISqlInsert : ISqlStatement
{
    string Schema { get; set; }
    string Table { get; set; }
}
