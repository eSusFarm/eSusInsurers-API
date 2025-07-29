using System;
using eSusInsurers.Common.Exceptions;
using Xunit;

public class BadRequestExceptionTests
{
    [Fact]
    public void Constructor_Default_SetsDefaultMessage()
    {
        var ex = new BadRequestException();

        Assert.NotNull(ex.Message); // ✅ Message is always non-null
        Assert.Null(ex.InnerException);
        Assert.Null(ex.Url);
    }

    [Fact]
    public void Constructor_WithMessage_SetsMessage()
    {
        var message = "Invalid input";
        var ex = new BadRequestException(message);

        Assert.Equal(message, ex.Message);
        Assert.Null(ex.InnerException);
        Assert.Null(ex.Url);
    }

    [Fact]
    public void Constructor_WithMessageAndUrl_SetsMessageAndUrl()
    {
        var message = "Invalid data";
        var url = "http://api/test";
        var ex = new BadRequestException(message, url);

        Assert.Equal(message, ex.Message);
        Assert.Equal(url, ex.Url);
        Assert.Null(ex.InnerException);
    }

    [Fact]
    public void Constructor_WithMessageAndInnerException_SetsBoth()
    {
        var message = "Invalid operation";
        var inner = new Exception("Root cause");
        var ex = new BadRequestException(message, inner);

        Assert.Equal(message, ex.Message);
        Assert.Equal(inner, ex.InnerException);
        Assert.Null(ex.Url);
    }

    [Fact]
    public void Constructor_WithNameAndKey_SetsDefaultCustomMessage()
    {
        var ex = new BadRequestException("EntityName", 123);

        Assert.Equal("Your custome message", ex.Message);
        Assert.Null(ex.InnerException);
        Assert.Null(ex.Url);
    }

    [Fact]
    public void UrlProperty_CanBeSetAndRetrieved()
    {
        var ex = new BadRequestException();
        var testUrl = "/bad-request";
        ex.Url = testUrl;

        Assert.Equal(testUrl, ex.Url);
    }
}