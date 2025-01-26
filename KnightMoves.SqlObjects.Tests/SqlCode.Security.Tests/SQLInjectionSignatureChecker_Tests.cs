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
using KnightMoves.SqlObjects.SqlCode.Security;
using Moq;
using Xunit;

namespace KnightMoves.SqlObjects.Tests.SqlCode.Security;

public class SQLInjectionSignatureChecker_Tests
{
    [Fact]
    public void ContainsSqlInjection_Returns_False()
    {
        // ARRANGE
        var mockSignature1 = new Mock<ISQLInjectionSignatureRule>();
        var mockSignature2 = new Mock<ISQLInjectionSignatureRule>();

        mockSignature1
            .Setup(s => s.ContainsSqlInjection(It.IsAny<string>()))
            .Returns(false);

        mockSignature2
            .Setup(s => s.ContainsSqlInjection(It.IsAny<string>()))
            .Returns(false);

        var mockSignatureRules = new List<ISQLInjectionSignatureRule>
        {
            mockSignature1.Object,
            mockSignature2.Object
        };

        var signatureChecker = new SQLInjectionSignatureChecker(mockSignatureRules);

        // ACTION
        var (injectionDetected, sigsFound) = signatureChecker.ContainsSqlInjection("test");

        // ASSERT
        Assert.Empty(sigsFound);
        Assert.False(injectionDetected);
    }

    [Fact]
    public void ContainsSqlInjection_Returns_True()
    {
        // ARRANGE
        var mockSignature1 = new Mock<ISQLInjectionSignatureRule>();
        var mockSignature2 = new Mock<ISQLInjectionSignatureRule>();

        mockSignature1
            .Setup(s => s.ContainsSqlInjection(It.IsAny<string>()))
            .Returns(false);

        mockSignature1
            .Setup(s => s.ErrorMessage)
            .Returns("Test Error Msg");

        mockSignature2
            .Setup(s => s.ContainsSqlInjection(It.IsAny<string>()))
            .Returns(true);

        var mockSignatureRules = new List<ISQLInjectionSignatureRule>
        {
            mockSignature1.Object,
            mockSignature2.Object
        };

        var signatureChecker = new SQLInjectionSignatureChecker(mockSignatureRules);

        // ACTION
        var (injectionDetected, sigsFound) = signatureChecker.ContainsSqlInjection("te'st");

        // ASSERT
        Assert.NotEmpty(sigsFound);
        Assert.True(injectionDetected);
    }
}
