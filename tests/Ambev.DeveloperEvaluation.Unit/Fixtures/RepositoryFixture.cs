using Ambev.DeveloperEvaluation.Domain.Repositories;
using NSubstitute;

namespace Ambev.DeveloperEvaluation.Unit.Fixtures;

/// <summary>
/// Provides a fixture for setting up repository dependencies for unit testing.
/// </summary>
public class RepositoryFixture
{
    /// <summary>
    /// Gets the mock instance of <see cref="IBusinessRuleRepository"/>.
    /// </summary>
    public IBusinessRuleRepository BusinessRuleRepository { get; private set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="RepositoryFixture"/> class.
    /// Sets up mock repositories for testing.
    /// </summary>
    public RepositoryFixture()
    {
        BusinessRuleRepository = Substitute.For<IBusinessRuleRepository>();

        SetupDefaultBusinessRules();
    }

    /// <summary>
    /// Sets up default mock behavior for the <see cref="IBusinessRuleRepository"/>.
    /// </summary>
    private void SetupDefaultBusinessRules()
    {
        BusinessRuleRepository.GetAll().Returns(new Dictionary<string, string>
        {
            { "MinQuantityForDiscount", "4" },
            { "MaxQuantityForDiscountTier1", "9" },
            { "MinQuantityForDiscountTier2", "10" },
            { "MaxQuantityLimit", "20" },
            { "Tier1Discount", "0.10" },
            { "Tier2Discount", "0.20" }
        });

        BusinessRuleRepository.GetValueAsInt("MaxQuantityLimit").Returns(20);
    }
}