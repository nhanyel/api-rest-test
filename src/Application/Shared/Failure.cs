namespace Application.Shared;

public class Failure
{
    public string Code { get; }
    public string Message { get; }

    public Failure(string code, string message)
    {
        Code = code;
        Message = message;
    }
}