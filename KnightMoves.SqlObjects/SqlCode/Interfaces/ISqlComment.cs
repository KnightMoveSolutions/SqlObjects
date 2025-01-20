namespace KnightMoves.SqlObjects.SqlCode;

public interface ISqlComment : ISqlStatement
{
    bool SingleLineCommentsOnly { get; set; }
    string CommentText { get; set; }
}
