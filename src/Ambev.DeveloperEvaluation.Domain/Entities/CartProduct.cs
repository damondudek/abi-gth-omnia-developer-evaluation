using Ambev.DeveloperEvaluation.Domain.Common;

/// <summary>
/// Represents a shopping cart in the system, containing products added by a user.
/// This entity follows domain-driven design principles and includes business rules validation.
/// </summary>
namespace Ambev.DeveloperEvaluation.Domain.Entities
{
    /// <summary>
    /// Represents a product within a shopping cart.
    /// </summary>
    public class CartProduct : BaseEntity
    {
        /// <summary>
        /// Gets or sets the unique identifier of the cart.
        /// </summary>
        public Guid CartId { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier of the product.
        /// </summary>
        public Guid ProductId { get; set; }

        /// <summary>
        /// Gets or sets the product`s title.
        /// </summary>
        public string ProductTitle { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the quantity of the product in the cart.
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// Gets or sets the price of the product in the cart.
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Gets or sets the discount of the product in the cart.
        /// </summary>
        public decimal Discount { get; set; }
    }
}
