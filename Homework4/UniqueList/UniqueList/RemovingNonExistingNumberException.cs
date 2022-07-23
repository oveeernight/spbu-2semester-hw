namespace UniqueList;

/// <summary>
/// Represents errors that occur when a non-existent element in a list is removed
/// </summary>
public class RemovingNonExistingNumberException : Exception
{
    public RemovingNonExistingNumberException() {}
    public RemovingNonExistingNumberException(string message) : base(message) {}
    public RemovingNonExistingNumberException(string message, Exception inner) : base(message, inner) {}
}