using System;
using eSusInsurers.Common.Exceptions;
using Xunit;

public class UnauthorizedExceptionTests
{
    [Fact]
    public void DefaultConstructor_ShouldCreateException()
    {
        var ex = new UnauthorizedException();
        Assert.NotNull(ex);
    }

    [Fact]
    public void Constructor_WithMessage_ShouldSetMessage()
    {
        var message = "Unauthorized access";
        var ex = new UnauthorizedException(message);
        Assert.Equal(message, ex.Message);
    }

    [Fact]
    public void Constructor_WithMessageAndInnerException_ShouldSetBoth()
    {
        var message = "Access denied";
        var inner = new Exception("Inner error");
        var ex = new UnauthorizedException(message, inner);
        Assert.Equal(message, ex.Message);
        Assert.Equal(inner, ex.InnerException);
    }

    [Fact]
    public void Constructor_WithEntityNameAndKey_ShouldFormatMessage()
    {
        var ex = new UnauthorizedException("User", 42);
        Assert.Equal("Entity \"User\" (42) was not found.", ex.Message);
    }
}