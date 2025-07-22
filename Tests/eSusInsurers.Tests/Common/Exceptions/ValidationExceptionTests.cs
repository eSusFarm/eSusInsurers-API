using System;
using System.Collections.Generic;
using System.Linq;
using eSusInsurers.Common.Exceptions;
using FluentValidation.Results;
using Xunit;

namespace eSusInsurers.Tests.Common.Exceptions;

public class ValidationExceptionTests
{
    [Fact]
    public void DefaultConstructor_ShouldSetDefaultMessageAndEmptyErrors()
    {
        // Act
        var ex = new ValidationException();

        // Assert
        Assert.Equal("One or more validation failures have occurred.", ex.Message);
        Assert.NotNull(ex.Errors);
        Assert.Empty(ex.Errors);
    }

    [Fact]
    public void Constructor_WithValidationFailures_ShouldPopulateErrorsCorrectly()
    {
        // Arrange
        var failures = new List<ValidationFailure>
        {
            new("Email", "Email is required"),
            new("Email", "Email must be valid"),
            new("Password", "Password is required")
        };

        // Act
        var ex = new ValidationException(failures);

        // Assert
        Assert.Equal("One or more validation failures have occurred.", ex.Message);
        Assert.Equal(2, ex.Errors.Count);

        Assert.True(ex.Errors.ContainsKey("Email"));
        Assert.Equal(2, ex.Errors["Email"].Length);
        Assert.Contains("Email is required", ex.Errors["Email"]);
        Assert.Contains("Email must be valid", ex.Errors["Email"]);

        Assert.True(ex.Errors.ContainsKey("Password"));
        Assert.Single(ex.Errors["Password"]);
        Assert.Equal("Password is required", ex.Errors["Password"].First());
    }

    [Fact]
    public void ErrorsProperty_ShouldReturnEmptyDictionary_IfNoFailuresProvided()
    {
        // Act
        var ex = new ValidationException(new List<ValidationFailure>());

        // Assert
        Assert.NotNull(ex.Errors);
        Assert.Empty(ex.Errors);
    }
}