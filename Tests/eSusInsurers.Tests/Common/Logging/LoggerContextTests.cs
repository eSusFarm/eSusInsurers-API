using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using eSusInsurers.Common.Logging;
using Moq;
using Serilog;
using Xunit;

public class LoggerContextTests
{
    private readonly Mock<ILogger> _mockLogger;
    private readonly Mock<ILogger> _mockEnrichedLogger;

    public LoggerContextTests()
    {
        _mockLogger = new Mock<ILogger>();
        _mockEnrichedLogger = new Mock<ILogger>();

        // Setup ForContext chaining for dictionary enrichment
        _mockLogger
            .Setup(l => l.ForContext(It.IsAny<string>(), It.IsAny<object>(), false))
            .Returns(_mockEnrichedLogger.Object);
        _mockLogger
            .Setup(l => l.ForContext(It.IsAny<string>(), It.IsAny<string>(), false))
            .Returns(_mockEnrichedLogger.Object);
        _mockEnrichedLogger
            .Setup(l => l.ForContext(It.IsAny<string>(), It.IsAny<object>(), false))
            .Returns(_mockEnrichedLogger.Object);

        // Stub logging calls to prevent runtime exceptions
        _mockEnrichedLogger
            .Setup(l => l.Information(It.IsAny<string>()))
            .Verifiable();
        _mockEnrichedLogger
            .Setup(l => l.Error(It.IsAny<Exception>(), It.IsAny<string>()))
            .Verifiable();
    }

    [Fact]
    public async Task LogMessageAsync_WithCustomProperties_DoesNotThrow()
    {
        var context = new LoggerContext<string>(_mockLogger.Object);
        var props = new Dictionary<string, string> { { "Category", "Test" } };

        var ex = await Record.ExceptionAsync(() =>
            context.LogMessageAsync("request", "sample-response", CancellationToken.None, props));

        Assert.Null(ex);
    }

    [Fact]
    public async Task LogErrorAsync_WithCustomProperties_DoesNotThrow()
    {
        var context = new LoggerContext<string>(_mockLogger.Object);
        var error = new InvalidOperationException("Simulated failure");
        var props = new Dictionary<string, string> { { "Severity", "Critical" } };

        var ex = await Record.ExceptionAsync(() =>
            context.LogErrorAsync("request", error, "error-response", CancellationToken.None, props));

        Assert.Null(ex);
    }

    [Fact]
    public async Task LogMessageAsync_WithNullCustomProperties_DoesNotThrow()
    {
        var context = new LoggerContext<string>(_mockLogger.Object);

        var ex = await Record.ExceptionAsync(() =>
            context.LogMessageAsync("request", "null-response", CancellationToken.None, null));

        Assert.Null(ex);
    }

    [Fact]
    public async Task LogErrorAsync_WithNullCustomProperties_DoesNotThrow()
    {
        var context = new LoggerContext<string>(_mockLogger.Object);
        var error = new Exception("Null test");

        var ex = await Record.ExceptionAsync(() =>
            context.LogErrorAsync("request", error, "null-error-response", CancellationToken.None, null));

        Assert.Null(ex);
    }
}