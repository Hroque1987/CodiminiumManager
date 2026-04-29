namespace Sharedkernel.Errors;

public sealed record class Error
{
    public string Message { get; }
    public string Code { get; }

    public ErrorType Type { get; }

    public Dictionary<string, string[]>? Details { get; }
    private Error(ErrorType type, string code, string message, Dictionary<string, string[]>? details = null)
    {
        Type = type;
        Code = code;
        Message = message;
        Details = details;
    }

    public static Error Domain(string code, string message)
        => new(ErrorType.Domain, code, message);

    public static Error Validation(string code, string message, Dictionary<string, string[]> details)
        => new(ErrorType.Validation, code, message, details);

    public static Error NotFound(string code, string message)
        => new(ErrorType.NotFound, code, message);

    public static Error Conflict(string code, string message)
        => new(ErrorType.Conflict, code, message);

    public static Error Infrastructure(string code, string message)
     => new(ErrorType.Infrastructure, code, message);
    public static Error Authentication(string code, string message)
       => new(ErrorType.Authentication, code, message);



}