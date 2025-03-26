using Ambev.DeveloperEvaluation.Application.Products.CreateProduct;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.UpdateProduct;

/// <summary>
/// Command for updating an existing product.
/// </summary>
/// <remarks>
/// This command is used to capture the required data for updating a product, 
/// including its title, price, description, category, image, and rating.
/// It implements <see cref="IRequest{TResponse}"/> to initiate the request 
/// that returns an <see cref="UpdateProductResult"/>.
/// 
/// The data provided in this command is validated using the 
/// <see cref="UpdateProductCommandValidator"/> which extends 
/// <see cref="AbstractValidator{T}"/> to ensure that the fields are correctly 
/// populated and follow the required rules.
/// </remarks>
public class UpdateProductCommand : IRequest<UpdateProductResult>
{
    /// <summary>
    /// The unique identifier of the product to update
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// The title of the product
    /// </summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// The price of the product
    /// </summary>
    public decimal Price { get; set; }

    /// <summary>
    /// A description of the product
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// The category of the product
    /// </summary>
    public string Category { get; set; } = string.Empty;

    /// <summary>
    /// The URL of the product image
    /// </summary>
    public string Image { get; set; } = string.Empty;

    /// <summary>
    /// The rating details of the product
    /// </summary>
    public CreateProductRatingCommand Rating { get; set; } = new CreateProductRatingCommand();
}