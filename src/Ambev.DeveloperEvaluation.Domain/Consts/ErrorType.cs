namespace Ambev.DeveloperEvaluation.Domain.Const;

/// <summary>
/// Contains predefined error types for standard error handling.
/// </summary>
public static class ErrorType
{
    /// <summary>
    /// Resource not found error.
    /// </summary>
    public const string ResourceNotFound = "ResourceNotFound";

    /// <summary>
    /// Authentication error.
    /// </summary>
    public const string AuthenticationError = "AuthenticationError";

    /// <summary>
    /// Validation error.
    /// </summary>
    public const string ValidationError = "ValidationError";

    /// <summary>
    /// Internal server error.
    /// </summary>
    public const string InternalServerError = "InternalServerError";
}
