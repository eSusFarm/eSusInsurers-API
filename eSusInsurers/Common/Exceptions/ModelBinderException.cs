namespace eSusInsurers.Common.Exceptions;

public class ModelBinderException : Exception
{
    public ModelBinderException()
    {
    }

    public ModelBinderException(string message)
        : base(message)
    {
    }
}