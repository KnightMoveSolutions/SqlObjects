using System.Collections.Generic;

namespace KnightMoves.SqlObjects.SqlCode
{
    public interface ISqlInListCondition : ISqlCondition
    {
        List<ISqlQueryExpression> InList { get; set; }
    }
}