/*
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