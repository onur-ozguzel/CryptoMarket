using CryptoMarket.Core.CrossCuttingConcerns.Exceptions;
using CryptoMarket.Core.CrossCuttingConcerns.Serilog;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Moq;
using System.Security.Claims;

namespace CryptoMarket.Core.Tests.Extensions
{
    public class ExceptionMiddlewareTests
    {
        private readonly Mock<RequestDelegate> _nextMock;
        private readonly Mock<IHttpContextAccessor> _httpContextAccessorMock;
        private readonly Mock<ILoggerServiceBase> _loggerServiceBaseMock;
        private readonly ExceptionMiddleware _middleware;

        public ExceptionMiddlewareTests()
        {
            _nextMock = new Mock<RequestDelegate>();
            _httpContextAccessorMock = new Mock<IHttpContextAccessor>();
            _loggerServiceBaseMock = new Mock<ILoggerServiceBase>();
            _middleware = new ExceptionMiddleware(_nextMock.Object, _httpContextAccessorMock.Object, _loggerServiceBaseMock.Object);

        }

        [Fact]
        public async Task ExceptionMiddleware_InvokeAsync_CallsNextDelegate()
        {
            // Arrange
            var context = new DefaultHttpContext();
            _nextMock.Setup(next => next(It.IsAny<HttpContext>())).Returns(Task.CompletedTask);

            // Act
            await _middleware.InvokeAsync(context);

            // Assert
            _nextMock.Verify(next => next(It.IsAny<HttpContext>()), Times.Once);
        }

        [Fact]
        public async Task ExceptionMiddleware_InvokeWithException_LogsAndHandlesException()
        {
            // Arrange
            var context = new DefaultHttpContext();
            var exception = new Exception("Test exception");

            _httpContextAccessorMock
                .Setup(accessor => accessor.HttpContext)
                .Returns(context);
            _nextMock
                .Setup(next => next(It.IsAny<HttpContext>()))
                .Throws(exception);
            _loggerServiceBaseMock
                .Setup(s => s.Error(It.IsAny<string>()));

            // Act
            await _middleware.InvokeAsync(context);

            // Assert
            _loggerServiceBaseMock.Verify(logger => logger.Error(It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public async Task ExceptionMiddleware_WithException_SetsResponseContentTypeToJson()
        {
            // Arrange
            var context = new DefaultHttpContext();
            var exception = new Exception("Test exception");

            _nextMock.Setup(next => next(It.IsAny<HttpContext>())).Throws(exception);

            // Act
            await _middleware.InvokeAsync(context);

            // Assert
            context.Response.ContentType.Should().Be("application/json");
        }

        [Fact]
        public async Task ExceptionMiddleware_LogException_LogsCorrectDetails()
        {
            // Arrange
            var context = new DefaultHttpContext();
            var exception = new Exception("Test exception");
            var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity([ new Claim(ClaimTypes.Name, "TestUser") ], "mock"));
            
            _nextMock.Setup(next => next(It.IsAny<HttpContext>())).Throws(exception);
            _httpContextAccessorMock.Setup(accessor => accessor.HttpContext).Returns(new DefaultHttpContext
            {
                User = claimsPrincipal
            });

            // Act
            await _middleware.InvokeAsync(context);

            // Assert
            _loggerServiceBaseMock.Verify(logger => logger.Error(It.Is<string>(log =>
                log.Contains("Test exception") &&
                log.Contains("TestUser")
            )), Times.Once);
        }
    }
}