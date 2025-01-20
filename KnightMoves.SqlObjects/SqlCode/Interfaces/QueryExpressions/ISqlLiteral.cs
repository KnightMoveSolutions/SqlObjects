namespace KnightMoves.SqlObjects.SqlCode;

public interface ISqlLiteral : ISqlQueryExpression
{
    bool IsQuoted { get; }
    string Value { get; set; }
}