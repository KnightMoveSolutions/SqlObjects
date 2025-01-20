using System.Collections.Generic;

namespace KnightMoves.SqlObjects.SqlCode;

public interface ISqlFunction : ISqlQueryExpression
{
    List<ISqlParameter> Parameters { get; }

    bool IsAggregate { get; set; }

    void SetParameterValue(string paramName, ISqlQueryExpression paramValue);
}

public interface ISqlFunctionAbs : ISqlFunction { }
public interface ISqlFunctionAvg : ISqlFunction { }
public interface ISqlFunctionCeiling : ISqlFunction { }
public interface ISqlFunctionCount : ISqlFunction { }
public interface ISqlFunctionDateAdd : ISqlFunction { }
public interface ISqlFunctionDateDiff : ISqlFunction { }
public interface ISqlFunctionDateName : ISqlFunction { }
public interface ISqlFunctionDatePart : ISqlFunction { }
public interface ISqlFunctionDay : ISqlFunction { }
public interface ISqlFunctionFloor : ISqlFunction { }
public interface ISqlFunctionGetDate : ISqlFunction { }
public interface ISqlFunctionMax : ISqlFunction { }
public interface ISqlFunctionMin : ISqlFunction { }
public interface ISqlFunctionMonth : ISqlFunction { }
public interface ISqlFunctionSum : ISqlFunction { }
public interface ISqlFunctionYear : ISqlFunction { }
public interface ISqlFunctionIsNull : ISqlFunction { }