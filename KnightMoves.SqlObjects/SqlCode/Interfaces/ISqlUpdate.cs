namespace KnightMoves.SqlObjects.SqlCode
{
    public interface ISqlUpdate : ISqlStatement
    {
        string Schema { get; set; }
        string Table { get; set; }
    }
}
