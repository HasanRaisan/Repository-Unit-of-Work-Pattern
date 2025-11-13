namespace Application.Results
{
    /// <summary>
    /// Represents the category or type of an application-level error.
    /// </summary>
    public enum ErrorType
    {
        None = 0,
        ValidationError = 1,
        NotFound = 2,
        Conflict = 3,
        InternalError = 4,
        NoChangesDetected = 5
    }
}
