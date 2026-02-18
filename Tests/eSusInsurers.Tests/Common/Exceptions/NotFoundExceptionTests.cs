using System;
using eSusInsurers.Common.Exceptions;
using Xunit;

public class NotFoundExceptionTests
{
    [Fact]
    public void Constructor_Default_SetsDefaultMessage()
    {
        var ex = new NotFoundException();

        Assert.NotNull(ex.Message); // Message is always set by base Exception
        Assert.Null(ex.InnerException);
    }

    [Fact]
    public void Constructor_WithMessage_SetsMessage()
    {
        var message = "Item not found";
        var ex = new NotFoundException(message);

        Assert.Equal(message, ex.Message);
        Assert.Null(ex.InnerException);
    }

    [Fact]
    public void Constructor_WithMessageAndInnerException_SetsBoth()
    {
        var message = "Failed to locate item";
        var inner = new Exception("Inner details");
        var ex = new NotFoundException(message, inner);

        Assert.Equal(message, ex.Message);
        Assert.Equal(inner, ex.InnerException);
    }

    [Fact]
    public void Constructor_WithNameAndKey_SetsFormattedMessage()
    {
        var name = "Program";
        var key = 42;

        var ex = new NotFoundException(name, key);

        Assert.Equal($"Entity \"{name}\" ({key}) was not found.", ex.Message);
        Assert.Null(ex.InnerException);
    }
}