using System;
using System.Reflection;
using eSusInsurers.Common.Exceptions;
using Xunit;

public class DbConnectionExceptionTests
{
    [Fact]
    public void PublicConstructor_SetsMessageAndInnerException()
    {
        // Arrange
        string message = "Database connection failed.";
        var inner = new InvalidOperationException("Timeout occurred.");

        // Act
        var ex = new DbConnectionException(message, inner);

        // Assert
        Assert.Equal(message, ex.Message);
        Assert.Equal(inner, ex.InnerException);
    }

    [Fact]
    public void ProtectedConstructor_WithMessage_SetsOnlyMessage()
    {
        // Arrange
        string message = "Connection error";

        // Act
        var ex = (DbConnectionException)Activator.CreateInstance(
            typeof(DbConnectionException),
            BindingFlags.Instance | BindingFlags.NonPublic,
            binder: null,
            args: new object[] { message },
            culture: null);

        // Assert
        Assert.Equal(message, ex.Message);
        Assert.Null(ex.InnerException);
    }

    [Fact]
    public void ProtectedParameterlessConstructor_SetsDefaultMessage()
    {
        // Act
        var ex = (DbConnectionException)Activator.CreateInstance(
            typeof(DbConnectionException),
            BindingFlags.Instance | BindingFlags.NonPublic,
            binder: null,
            args: null,
            culture: null);

        // Assert
        Assert.NotNull(ex);
        Assert.StartsWith("Exception of type", ex.Message); // More accurate than expecting null or empty
        Assert.Null(ex.InnerException);
    }
}