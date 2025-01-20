using System;

namespace KnightMoves.SqlObjects.SqlCode;

public interface IFluentSqlWhere
{
    SqlStatement WHERE();
    SqlStatement WHERE(ISqlLiteral leftLiteral);
    SqlStatement WHERE(string operand);
    SqlStatement WHERE(string multiPartIdentifier, string operand);
    SqlStatement WHERE(int value);
    SqlStatement WHERE(DateTime value);
    SqlStatement WHERE(bool value);
    SqlStatement WHERE(long value);
    SqlStatement WHERE(Guid value);
    SqlStatement WHERE(char value);
    SqlStatement WHERE(decimal value);
}
