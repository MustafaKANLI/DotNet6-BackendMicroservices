namespace Common.Exceptions;

using System.Globalization;

public class ApiException : Exception
{
    public List<string>? Errors { get; set; }
    public ApiException() : base() { }

    public ApiException(string message) : base(message) { }

    public ApiException(string message, params object[] args)
        : base(String.Format(CultureInfo.CurrentCulture, message, args))
    {
    }
}
