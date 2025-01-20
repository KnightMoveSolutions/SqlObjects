namespace KnightMoves.SqlObjects.SqlCode;

public interface ISqlDelete : ISqlStatement
{
    string Schema { get; set; }
    string Table { get; set; }
}
