namespace eSusInsurers.Common.Exceptions;

public class DbConnectionException : Exception
{
    protected DbConnectionException()
    {
    }

    protected DbConnectionException(string message)
        : base(message)
    {
    }

    public DbConnectionException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}