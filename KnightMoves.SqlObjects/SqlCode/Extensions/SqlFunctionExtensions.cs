using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace KnightMoves.SqlObjects.SqlCode;

public static class SqlFunctionExtensions
{
    public static void SetParameterValue(this ISqlFunction sqlFunc, List<ISqlParameter> parameters, string paramName, ISqlQueryExpression paramValue)
    {
        ISqlParameter parameter = parameters.SingleOrDefault(p => p.ParameterName == paramName);

        if (parameter is null)
            throw new ArgumentException("Invalid paramName value. The parameter '" + paramName + "' does not exist.");

        if (parameter.IsVariant == false && paramValue.DataType.DataType != SqlDbType.Variant.ToString() && parameter.ParameterDataType != paramValue.DataType)
            throw new ArgumentException("The DataType of the parameter value does not match the DataType of the parameter being set.  The " + paramName + " parameter value must be a(n) " + parameter.ParameterDataType.ToString() + " but the value provided is a(n) " + paramValue.DataType.ToString());

        parameter.Value = paramValue;
    }

    // A bit of a hack to deal with Parameters that are duplicated when deserialized form JSON.
    // Parameters are created first in the constructor normally, then re-created when pulling 
    // the parameter data from the JSON. The constructor parameters have a NULL value whereas
    // parameters from JSON may have values. Constructor parameters are always created first.
    public static void DeDuplicateParameters(this ISqlFunction sqlFunc)
    {
        var parametersToRemove = new List<ISqlParameter>();

        foreach (var p in sqlFunc.Parameters)
        {
            if (sqlFunc.Parameters.Count(chk => chk.ParameterName == p.ParameterName) == 1)
                continue;

            var defaultParam = parametersToRemove.FirstOrDefault(r => r.ParameterName == p.ParameterName && r.Value == null);

            if (defaultParam == null)
                parametersToRemove.Add(p);
        };

        parametersToRemove.ForEach(p => sqlFunc.Parameters.Remove(p));
    }
}
