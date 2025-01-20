namespace KnightMoves.SqlObjects.SqlCode;

public interface ISqlJoin
{
    string Schema { get; set; }
    string Table { get; set; }
    string MultipartIdentifier { get; set; }
}

public interface ISqlInnerJoin : ISqlJoin { }
public interface ISqlLeftJoin : ISqlJoin { }
public interface ISqlRightJoin : ISqlJoin { }
