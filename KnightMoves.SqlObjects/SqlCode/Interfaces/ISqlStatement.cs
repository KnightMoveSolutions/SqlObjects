﻿/*
    THE LICENSED WORK IS PROVIDED UNDER THE TERMS OF THE DEVELOPER TOOL LIMITED 
    LICENSE (“LICENSE”) AS FIRST COMPLETED BY THE ORIGINAL AUTHOR. ANY USE, PUBLIC 
    DISPLAY, PUBLIC PERFORMANCE, REPRODUCTION OR DISTRIBUTION OF, OR PREPARATION OF 
    DERIVATIVE WORKS BASED ON THE LICENSED WORK CONSTITUTES RECIPIENT’S ACCEPTANCE 
    OF THIS LICENSE AND ITS TERMS, WHETHER OR NOT SUCH RECIPIENT READS THE TERMS OF THE 
    LICENSE. “LICENSED WORK” AND “RECIPIENT” ARE DEFINED IN THE LICENSE. A COPY OF THE 
    LICENSE IS LOCATED IN THE TEXT FILE ENTITLED “LICENSE.TXT” ACCOMPANYING THE CONTENTS 
    OF THIS FILE. IF A COPY OF THE LICENSE DOES NOT ACCOMPANY THIS FILE, A COPY OF THE 
    LICENSE MAY ALSO BE OBTAINED AT THE FOLLOWING WEB SITE:  
 
    https://docs.knightmovesolutions.com/sql-objects/license.html
*/

namespace KnightMoves.SqlObjects.SqlCode;

public interface ISqlStatement
{
    string Id { get; set; }
    string Name { get; set; }
    string Description { get; set; }

    bool IsDelete { get; }
    bool IsFrom { get; }
    bool IsGroupBy { get; }
    bool IsInsert { get; }
    bool IsJoin { get; }
    bool IsRightJoin { get; }
    bool IsLeftJoin { get; }
    bool IsInnerJoin { get; }
    bool IsScript { get; }
    bool IsSelect { get; }
    bool IsUnion { get; }
    bool IsUpdate { get; }
    bool IsWhereClause { get; }
    bool IsBasicCondition { get; }
    bool IsBetween { get; }
    bool IsCondition { get; }
    bool IsConditionGroup { get; }
    bool IsInList { get; }
    bool IsLike { get; }
    bool IsFunction { get; }
    bool IsFunctionAbs { get; }
    bool IsFunctionAvg { get; }
    bool IsFunctionCeiling { get; }
    bool IsFunctionCount { get; }
    bool IsFunctionDateAdd { get; }
    bool IsFunctionDateDiff { get; }
    bool IsFunctionDateName { get; }
    bool IsFunctionDatePart { get; }
    bool IsFunctionDay { get; }
    bool IsFunctionFloor { get; }
    bool IsFunctionGetDate { get; }
    bool IsFunctionMax { get; }
    bool IsFunctionMin { get; }
    bool IsFunctionMonth { get; }
    bool IsFunctionSum { get; }
    bool IsFunctionYear { get; }
    bool IsFunctionParameter { get; }
    bool IsCalculation { get; }
    bool IsColumn { get; }
    bool IsLiteral { get; }
    bool IsQueryExpression { get; }
    bool IsSubQuery { get; }
    bool IsOrderBy { get; }
    bool IsOrderByExpression { get; }
    bool IsHaving { get; }
    bool IsComment { get; }

    string GetOperatorString(SqlArithmeticOperators op);
    string GetOperatorString(SqlComparisonOperators op);
    string GetOperatorString(SqlLogicalOperators op);
    string SQL();
    string ToString();
    string Build();
    string Build(object parameters);

    SqlStatement WithId(string id);
    SqlStatement WithName(string name);

    string ToJson();
}