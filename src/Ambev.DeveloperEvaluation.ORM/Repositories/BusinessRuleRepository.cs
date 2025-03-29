using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using System.Globalization;

namespace Ambev.DeveloperEvaluation.ORM.Repositories;

/// <summary>
/// Implementation of BusinessRuleRepository for BusinessRule-specific operations
/// </summary>
public class BusinessRuleRepository : IBusinessRuleRepository
{
    private readonly List<BusinessRule> _businessRules;

    public BusinessRuleRepository()
    {
        _businessRules =
        [
            new BusinessRule { ConfigKey = "MinQuantityForDiscount", ConfigValue = "4" },
            new BusinessRule { ConfigKey = "MaxQuantityForDiscountTier1", ConfigValue = "9" },
            new BusinessRule { ConfigKey = "MinQuantityForDiscountTier2", ConfigValue = "10" },
            new BusinessRule { ConfigKey = "MaxQuantityLimit", ConfigValue = "20" },
            new BusinessRule { ConfigKey = "Tier1Discount", ConfigValue = "0.10" },
            new BusinessRule { ConfigKey = "Tier2Discount", ConfigValue = "0.20" }
        ];
    }

    public BusinessRule? GetByConfigKey(string configKey)
        => _businessRules.FirstOrDefault(rule => rule.ConfigKey == configKey);

    public int GetValueAsInt(string configKey)
    {
        var rule = _businessRules.FirstOrDefault(rule => rule.ConfigKey == configKey);
        int.TryParse(rule?.ConfigValue, CultureInfo.InvariantCulture, out var ruleValue);

        return ruleValue;
    }

    public IDictionary<string, string> GetAll()
        => _businessRules.ToDictionary(rule => rule.ConfigKey, rule => rule.ConfigValue);
}