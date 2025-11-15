namespace Application.Results
{
    /// <summary>
    /// Represents a single error instance containing both its type and message.
    /// </summary>
    public class Error
    {
        public ErrorType Type { get; }
        public IReadOnlyList<string> Message { get; }

        public Error(ErrorType type, IReadOnlyList<string> message)
        {
            Type = type;
            Message = message;
        }
        public override string ToString() => $"{Type}: {Message}";
    }
}
