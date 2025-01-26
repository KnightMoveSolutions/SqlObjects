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

using System;
using KnightMoves.SqlObjects.SqlCode;
using Xunit;

namespace KnightMoves.SqlObjects.Tests.TSql.Fluent;

public class FluentApi_Build_Tests
{
    private readonly string NL = Environment.NewLine;

    [Fact]
    public void Build_With_Parameters()
    {
        // ARRANGE 
        var today = DateTime.Today;
        var todayStr = today.ToString(SqlDataType.SQL_DATE_STRING_FORMAT);
        var guid = Guid.NewGuid();

        var parameters = new
        {
            StringParam = "stringValue",
            IntParam = 99,
            DateTimeParam = today,
            BoolParam = true,
            LongParam = long.MaxValue,
            GuidParam = guid,
            CharParam = 'X',
            DecimalParam = decimal.MaxValue
        };

        var sqlObj = TSQL
            .SELECT()
              .STAR()
            .FROM("SomeTable")
            .WHERE("StringColumn").IsEqualTo("@StringParam")
              .AND("IntColumn").IsEqualTo("@IntParam")
              .AND("DateTimeColumn").IsEqualTo("@DateTimeParam")
              .AND("BoolColumn").IsEqualTo("@BoolParam")
              .AND("LongColumn").IsEqualTo("@LongParam")
              .AND("GuidColumn").IsEqualTo("@GuidParam")
              .AND("CharColumn").IsEqualTo("@CharParam")
              .AND("DecimalColumn").IsEqualTo("@DecimalParam");

        // ACTION
        var sql = sqlObj.Build(parameters);

        var expected = $"  SELECT{NL}" +
                       $"   *{NL}" +
                       $"  FROM [SomeTable]{NL}" +
                       $"  WHERE 1=1{NL}" +
                       $"   AND [StringColumn] = 'stringValue'{NL}" +
                       $"   AND [IntColumn] = 99{NL}" +
                       $"   AND [DateTimeColumn] = '{todayStr}'{NL}" +
                       $"   AND [BoolColumn] = 1{NL}" +
                       $"   AND [LongColumn] = {long.MaxValue}{NL}" +
                       $"   AND [GuidColumn] = '{guid}'{NL}" +
                       $"   AND [CharColumn] = 'X'{NL}" +
                       $"   AND [DecimalColumn] = {decimal.MaxValue}{NL}";

        // ASSERT
        Assert.Equal(expected, sql);
    }
}
