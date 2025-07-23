using System;
using eSusInsurers.Common.Exceptions;
using Microsoft.EntityFrameworkCore;
using Xunit;

public class SqlDbUpdateExceptionTests
{
    [Fact]
    public void Constructor_Parameterless_CreatesInstance()
    {
        // Act
        var exception = (SqlDbUpdateException)Activator.CreateInstance(typeof(SqlDbUpdateException), nonPublic: true)!;

        // Assert
        Assert.NotNull(exception);
        Assert.IsType<SqlDbUpdateException>(exception);
    }

    [Fact]
    public void Constructor_WithMessage_SetsMessage()
    {
        // Arrange
        var message = "Custom error message";

        // Act
        var exception = new SqlDbUpdateException(message);

        // Assert
        Assert.Equal(message, exception.Message);
    }

    [Fact]
    public void Constructor_WithMessageAndInnerException_SetsProperties()
    {
        // Arrange
        var message = "Outer error";
        var inner = new InvalidOperationException("Inner error");

        // Act
        var exception = new SqlDbUpdateException(message, inner);

        // Assert
        Assert.Equal(message, exception.Message);
        Assert.Equal(inner, exception.InnerException);
    }
}