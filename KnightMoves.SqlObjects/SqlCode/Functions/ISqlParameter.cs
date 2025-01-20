namespace KnightMoves.SqlObjects.SqlCode;

public interface ISqlParameter : ISqlQueryExpression
{
    string DefaultValue { get; set; }
    string EnumType { get; set; }
    bool IsVariant { get; set; }
    int Order { get; set; }
    SqlDataType ParameterDataType { get; set; }
    string ParameterDescription { get; set; }
    string ParameterName { get; set; }
    ISqlQueryExpression Value { get; set; }
}