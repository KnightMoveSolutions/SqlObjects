using System;

namespace KnightMoves.SqlObjects.SqlCode
{
    public interface IFluentSqlLogicalOperators
    {
        SqlStatement AND();
        SqlStatement AND(ISqlLiteral leftLiteral);
        SqlStatement AND(string operand);
        SqlStatement AND(string multiPartIdentifier, string operand);
        SqlStatement AND(int operand);
        SqlStatement AND(DateTime operand);
        SqlStatement AND(bool operand);
        SqlStatement AND(long operand);
        SqlStatement AND(Guid operand);
        SqlStatement AND(char operand);
        SqlStatement AND(decimal operand);

        SqlStatement OR();
        SqlStatement OR(ISqlLiteral leftLiteral);
        SqlStatement OR(string operand);
        SqlStatement OR(string multiPartIdentifier, string operand);
    }
}
