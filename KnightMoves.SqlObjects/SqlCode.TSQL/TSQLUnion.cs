using System;

namespace KnightMoves.SqlObjects.SqlCode.TSQL;

/// <summary>
/// This class inplements <see cref="ISqlUnion"/> and builds a SQL UNION statement
/// </summary>
public class TSQLUnion : TSQLStatement, ISqlUnion
{
    /// <summary>
    /// Outputs a UNION statement
    /// </summary>
    public override string SQL()
    {
        return $"{Environment.NewLine}{IndentString}UNION{Environment.NewLine}{Environment.NewLine}";
    }
}
