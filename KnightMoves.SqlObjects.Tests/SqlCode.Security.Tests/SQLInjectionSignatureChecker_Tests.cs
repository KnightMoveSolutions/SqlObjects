using System.Collections.Generic;
using KnightMoves.SqlObjects.SqlCode.Security;
using Moq;
using Xunit;

namespace KnightMoves.SqlObjects.Tests.SqlCode.Security
{
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
}
