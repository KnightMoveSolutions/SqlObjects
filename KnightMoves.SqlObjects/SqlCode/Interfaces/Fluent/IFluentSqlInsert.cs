using System;

namespace KnightMoves.SqlObjects.SqlCode;

public interface IFluentSqlInsert
{
    SqlStatement INSERT();
    SqlStatement INTO(string table);
    SqlStatement INTO(string schema, string table);
    SqlStatement VALUES();
    SqlStatement VALUE(string value);
    SqlStatement VALUE(int value);
    SqlStatement VALUE(DateTime value);
    SqlStatement VALUE(bool value);
    SqlStatement VALUE(long value);
    SqlStatement VALUE(Guid value);
    SqlStatement VALUE(char value);
    SqlStatement VALUE(decimal value);

}
