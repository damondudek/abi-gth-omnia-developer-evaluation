using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.ORM.Extensions;
using Ambev.DeveloperEvaluation.Unit.Repository.TestData;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Repository
{
    public class QueryableExtensionsTests
    {
        private readonly IQueryable<User> _query;
        private readonly List<User> _testUsers;

        public QueryableExtensionsTests()
        {
            _testUsers = UserTestData.GenerateTestUsers();
            _query = _testUsers.AsQueryable();
            var mockQueryable = Substitute.For<IQueryable<User>>();
            var mockAsyncQueryable = mockQueryable.As<IAsyncEnumerable<User>>();
        }

        [Fact(DisplayName = "ApplyOrderBy should order ascending when no direction specified")]
        public void ApplyOrderBy_NoDirectionSpecified_OrdersAscending()
        {
            // Arrange
            var orderBy = "Username";

            // Act
            var result = _query.ApplyOrderBy(orderBy).ToList();

            // Assert
            result.Should().BeInAscendingOrder(u => u.Username);
        }

        [Fact(DisplayName = "ApplyOrderBy should order descending when specified")]
        public void ApplyOrderBy_WithDescending_OrdersDescending()
        {
            // Arrange
            var orderBy = "Username desc";

            // Act
            var result = _query.ApplyOrderBy(orderBy).ToList();

            // Assert
            result.Should().BeInDescendingOrder(u => u.Username);
        }

        [Fact(DisplayName = "ApplyOrderBy should handle multiple order clauses")]
        public void ApplyOrderBy_MultipleOrderClauses_OrdersCorrectly()
        {
            // Arrange
            var orderBy = "Status desc, Username asc";

            // Act
            var result = _query.ApplyOrderBy(orderBy).ToList();

            // Assert
            result.Should().BeInDescendingOrder(u => u.Status)
                .And.ThenBeInAscendingOrder(u => u.Username);
        }

        [Fact(DisplayName = "ApplyFilters should filter by exact string match")]
        public void ApplyFilters_ExactStringMatch_FiltersCorrectly()
        {
            // Arrange
            var filters = new Dictionary<string, string> { { "Username", "user1" } };

            // Act
            var result = _query.ApplyFilters(filters).ToList();

            // Assert
            result.Should().ContainSingle(u => u.Username == "user1");
        }

        [Fact(DisplayName = "ApplyFilters should filter by contains wildcard")]
        public void ApplyFilters_ContainsWildcard_FiltersCorrectly()
        {
            // Arrange
            var filters = new Dictionary<string, string> { { "Username", "*ser*" } };

            // Act
            var result = _query.ApplyFilters(filters).ToList();

            // Assert
            result.Should().OnlyContain(u => u.Username.Contains("ser"));
        }

        [Fact(DisplayName = "ApplyFilters should filter by starts with wildcard")]
        public void ApplyFilters_StartsWithWildcard_FiltersCorrectly()
        {
            // Arrange            
            var filters = new Dictionary<string, string> { { "Username", "user*" } };

            // Act
            var result = _query.ApplyFilters(filters).ToList();

            // Assert
            result.Should().OnlyContain(u => u.Username.StartsWith("user"));
        }

        [Fact(DisplayName = "ApplyFilters should filter by ends with wildcard")]
        public void ApplyFilters_EndsWithWildcard_FiltersCorrectly()
        {
            // Arrange            
            var filters = new Dictionary<string, string> { { "Username", "*1" } };

            // Act
            var result = _query.ApplyFilters(filters).ToList();

            // Assert
            result.Should().OnlyContain(u => u.Username.EndsWith("1"));
        }

        [Fact(DisplayName = "ApplyFilters should filter by enum value")]
        public void ApplyFilters_EnumValue_FiltersCorrectly()
        {
            // Arrange
            var filters = new Dictionary<string, string> { { "Status", "Active" } };

            // Act
            var result = _query.ApplyFilters(filters).ToList();

            // Assert
            result.Should().OnlyContain(u => u.Status == UserStatus.Active);
        }

        [Fact(DisplayName = "ApplyFilters should filter by min range")]
        public void ApplyFilters_MinRange_FiltersCorrectly()
        {
            // Arrange
            var filters = new Dictionary<string, string> { { "_minCreatedAt", "2023-01-02" } };

            // Act
            var result = _query.ApplyFilters(filters).ToList();

            // Assert
            result.Should().OnlyContain(u => u.CreatedAt >= new DateTime(2023, 1, 2));
        }

        [Fact(DisplayName = "ApplyFilters should filter by max range")]
        public void ApplyFilters_MaxRange_FiltersCorrectly()
        {
            // Arrange
            var filters = new Dictionary<string, string> { { "_maxCreatedAt", "2023-01-02" } };

            // Act
            var result = _query.ApplyFilters(filters).ToList();

            // Assert
            result.Should().OnlyContain(u => u.CreatedAt <= new DateTime(2023, 1, 2));
        }

        [Fact(DisplayName = "ApplyFilters should ignore unknown properties")]
        public void ApplyFilters_UnknownProperty_DoesNotFilter()
        {
            // Arrange
            var filters = new Dictionary<string, string> { { "UnknownProperty", "value" } };

            // Act
            var result = _query.ApplyFilters(filters).ToList();

            // Assert
            result.Should().HaveCount(_testUsers.Count);
        }
    }
}