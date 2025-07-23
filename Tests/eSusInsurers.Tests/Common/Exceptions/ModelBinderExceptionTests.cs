using System;
using System.Reflection;
using eSusInsurers.Common.Exceptions;
using Xunit;

public class ModelBinderExceptionTests
{
    [Fact]
    public void DefaultConstructor_SetsDefaultMessage()
    {
        // Act
        var ex = new ModelBinderException();

        // Assert
        Assert.NotNull(ex);
        Assert.StartsWith("Exception of type", ex.Message);
        Assert.Null(ex.InnerException);
    }

    [Fact]
    public void Constructor_WithMessage_SetsMessageOnly()
    {
        // Arrange
        string message = "Binder failed during model conversion";

        // Act
        var ex = new ModelBinderException(message);

        // Assert
        Assert.Equal(message, ex.Message);
        Assert.Null(ex.InnerException);
    }
}