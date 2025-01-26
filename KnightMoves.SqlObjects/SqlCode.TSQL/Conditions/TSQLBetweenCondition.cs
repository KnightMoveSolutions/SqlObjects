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

using KnightMoves.Hierarchical;
using Newtonsoft.Json;

namespace KnightMoves.SqlObjects.SqlCode.TSQL;

/// <summary>
/// This class implements <see cref="ISqlBetweenCondition"/> and builds a BETWEEN clause 
/// <code>
/// [LeftOperand] BETWEEN [StartVal] AND [EndVal]
/// </code>
/// </summary>
public class TSQLBetweenCondition : TSQLStatement, ISqlBetweenCondition
{
    /// <summary>
    /// A <see cref="ISqlQueryExpression"/> object that represents the beginning value of a BETWEEN SQL clause.
    /// </summary>
    [JsonConverter(typeof(TreeNodeJsonConverter))]
    public ISqlQueryExpression StartVal { get; set; }

    /// <summary>
    /// A <see cref="ISqlQueryExpression"/> object that represents the end value of a BETWEEN SQL clause.
    /// </summary>
    [JsonConverter(typeof(TreeNodeJsonConverter))]
    public ISqlQueryExpression EndVal { get; set; }

    /// <summary>
    /// Returns a BETWEEN clause using the LeftOperand property as the left operand, 
    /// the StartVal property as the left range value (inclusive) and the EndVal property 
    /// as the right range value (inclusive).
    /// </summary>
    public override string SQL()
    {
        return $"{LeftOperand} BETWEEN {StartVal} AND {EndVal}";
    }
}
