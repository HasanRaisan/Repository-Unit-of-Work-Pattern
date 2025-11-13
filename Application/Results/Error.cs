namespace Application.Results
{
    /// <summary>
    /// Represents a single error instance containing both its type and message.
    /// </summary>
    public class Error
    {
        public ErrorType Type { get; }
        public List<string> Message { get; }

        public Error(ErrorType type, List<string> message)
        {
            Type = type;
            Message = message;
        }
        public override string ToString() => $"{Type}: {Message}";
    }
}
