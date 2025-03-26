using System;
using System.Collections.Generic;

namespace Ambev.DeveloperEvaluation.Application.Carts.GetCart;

/// <summary>
/// Response model for GetCart operation
/// </summary>
public class GetCartResult
{
    /// <summary>
    /// The unique identifier of the cart
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// The unique identifier of the user associated with the cart
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// The date of the cart
    /// </summary>
    public DateTime Date { get; set; }

    /// <summary>
    /// The list of products in the cart
    /// </summary>
    public List<GetCartProductResult> Products { get; set; } = new List<GetCartProductResult>();
}

/// <summary>
/// Represents a product in the cart
/// </summary>
public class GetCartProductResult
{
    /// <summary>
    /// The unique identifier of the product
    /// </summary>
    public Guid ProductId { get; set; }

    /// <summary>
    /// The quantity of the product
    /// </summary>
    public int Quantity { get; set; }
}