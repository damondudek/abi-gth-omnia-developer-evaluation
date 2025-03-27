using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Repositories;

/// <summary>
/// Repository interface for BusinessRule entity operations.
/// </summary>
public interface IBusinessRuleRepository
{
    /// <summary>
    /// Retrieves a business rule by its configuration key.
    /// </summary>
    /// <param name="configKey">The configuration key to search for.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The business rule if found, null otherwise.</returns>
    BusinessRule? GetByConfigKey(string configKey);
    int GetConfigValueAsIntegerByConfigKey(string configKey);
    IDictionary<string, string> GetAll();
}