using System;
using System.Collections.Generic;
using KnightMoves.SqlObjects.SqlCode.Security;
using Moq;
using Xunit;

namespace KnightMoves.SqlObjects.Tests.SqlCode.Security
{
    public class SQLSecurityFilter_Tests
    {
        [Fact]
        public void SanitizeInput_T_ExecutesOverloadedMethod()
        {
            // ARRANGE
            var mockFilterRule1 = new Mock<ISQLSecurityFilterRule>();
            var mockFilterRule2 = new Mock<ISQLSecurityFilterRule>();

            var mockFilterRules = new List<ISQLSecurityFilterRule>
            {
                mockFilterRule1.Object,
                mockFilterRule2.Object
            };

            var securityFilter = new SQLSecurityFilter(mockFilterRules);

            // ACTION
            securityFilter.SanitizeInput<string>("test");

            // ASSERT
            mockFilterRule1.Verify(r => r.SanitizeInput(It.IsAny<Type>(), It.IsAny<string>()));
            mockFilterRule2.Verify(r => r.SanitizeInput(It.IsAny<Type>(), It.IsAny<string>()));
        }

        [Fact]
        public void SanitizeInput_ReturnsOrigVal_When_DisableSecurityCheck_IsTrue()
        {
            // ARRANGE
            var mockFilterRule1 = new Mock<ISQLSecurityFilterRule>();
            var mockFilterRule2 = new Mock<ISQLSecurityFilterRule>();

            mockFilterRule2
                .Setup(r => r.SanitizeInput(It.IsAny<Type>(), It.IsAny<string>()))
                .Returns("sanitized string");

            var mockFilterRules = new List<ISQLSecurityFilterRule>
            {
                mockFilterRule1.Object,
                mockFilterRule2.Object
            };

            var securityFilter = new SQLSecurityFilter(mockFilterRules) { DisableSecurityCheck = true };

            // ACTION
            var result = securityFilter.SanitizeInput(typeof(string), "test");

            // ASSERT
            Assert.Equal("test", result);
        }

        [Fact]
        public void SanitizeInput_Returns_Sanitized_Value()
        {
            // ARRANGE
            var mockFilterRule1 = new Mock<ISQLSecurityFilterRule>();
            var mockFilterRule2 = new Mock<ISQLSecurityFilterRule>();

            mockFilterRule2
                .Setup(r => r.SanitizeInput(It.IsAny<Type>(), It.IsAny<string>()))
                .Returns("sanitized string");

            var mockFilterRules = new List<ISQLSecurityFilterRule>
            {
                mockFilterRule1.Object,
                mockFilterRule2.Object
            };

            var securityFilter = new SQLSecurityFilter(mockFilterRules);

            // ACTION
            var result = securityFilter.SanitizeInput(typeof(string), "test");

            // ASSERT
            Assert.Equal("sanitized string", result);
        }

        [Fact]
        public void CheckInput_T_ExecutesOverloadedMethod()
        {
            // ARRANGE
            var mockFilterRule1 = new Mock<ISQLSecurityFilterRule>();
            var mockFilterRule2 = new Mock<ISQLSecurityFilterRule>();

            var mockFilterRules = new List<ISQLSecurityFilterRule>
            {
                mockFilterRule1.Object,
                mockFilterRule2.Object
            };

            var securityFilter = new SQLSecurityFilter(mockFilterRules);

            // ACTION
            securityFilter.CheckInput<string>("test");

            // ASSERT
            mockFilterRule1.Verify(r => r.CheckInput(It.IsAny<Type>(), It.IsAny<string>()));
            mockFilterRule2.Verify(r => r.CheckInput(It.IsAny<Type>(), It.IsAny<string>()));
        }

        [Fact]
        public void CheckInput_ReturnsOriginalVal_When_DisabledSecurityCheck_IsTrue()
        {
            // ARRANGE
            var mockFilterRule1 = new Mock<ISQLSecurityFilterRule>();
            var mockFilterRule2 = new Mock<ISQLSecurityFilterRule>();

            var mockFilterRules = new List<ISQLSecurityFilterRule>
            {
                mockFilterRule1.Object,
                mockFilterRule2.Object
            };

            var securityFilter = new SQLSecurityFilter(mockFilterRules) { DisableSecurityCheck = true };

            // ACTION
            var (result, errors) = securityFilter.CheckInput(typeof(string), "test");

            // ASSERT
            Assert.True(result);
            Assert.Empty(errors);
        }

        [Fact]
        public void CheckInput_Executes_FilterRules_Returns_True()
        {
            // ARRANGE
            var mockFilterRule1 = new Mock<ISQLSecurityFilterRule>();
            var mockFilterRule2 = new Mock<ISQLSecurityFilterRule>();

            mockFilterRule1
                .Setup(r => r.CheckInput(It.IsAny<Type>(), It.IsAny<string>()))
                .Returns((true, null));

            mockFilterRule2
                .Setup(r => r.CheckInput(It.IsAny<Type>(), It.IsAny<string>()))
                .Returns((true, null));

            var mockFilterRules = new List<ISQLSecurityFilterRule>
            {
                mockFilterRule1.Object,
                mockFilterRule2.Object
            };

            var securityFilter = new SQLSecurityFilter(mockFilterRules);

            // ACTION
            var (result, errors) = securityFilter.CheckInput(typeof(string), "test");

            // ASSERT
            Assert.True(result);
            Assert.Empty(errors);
        }

        [Fact]
        public void CheckInput_Executes_FilterRules_Returns_False()
        {
            // ARRANGE
            var mockFilterRule1 = new Mock<ISQLSecurityFilterRule>();
            var mockFilterRule2 = new Mock<ISQLSecurityFilterRule>();

            mockFilterRule1
                .Setup(r => r.CheckInput(It.IsAny<Type>(), It.IsAny<string>()))
                .Returns((false, new List<string> { "Test Error" }));

            mockFilterRule2
                .Setup(r => r.CheckInput(It.IsAny<Type>(), It.IsAny<string>()))
                .Returns((true, null));

            var mockFilterRules = new List<ISQLSecurityFilterRule>
            {
                mockFilterRule1.Object,
                mockFilterRule2.Object
            };

            var securityFilter = new SQLSecurityFilter(mockFilterRules);

            // ACTION
            var (result, errors) = securityFilter.CheckInput(typeof(string), "test");

            // ASSERT
            Assert.False(result);
            Assert.NotEmpty(errors);
        }
    }
}
