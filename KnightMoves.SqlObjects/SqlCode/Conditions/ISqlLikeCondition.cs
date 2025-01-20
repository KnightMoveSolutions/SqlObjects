namespace KnightMoves.SqlObjects.SqlCode;

public interface ISqlLikeCondition : ISqlCondition
{
    string Pattern { get; set; }
}