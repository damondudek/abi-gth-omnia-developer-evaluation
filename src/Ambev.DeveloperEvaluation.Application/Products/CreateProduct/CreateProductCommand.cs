using Ambev.DeveloperEvaluation.Common.Validation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.CreateProduct;

/// <summary>
/// Command for creating a new product.
/// </summary>
/// <remarks>
/// This command is used to capture the required data for creating a product, 
/// including title, price, description, category, image, and rating details. 
/// It implements <see cref="IRequest{TResponse}"/> to initiate the request 
/// that returns a <see cref="CreateProductResult"/>.
///
/// The data provided in this command is validated using the 
/// <see cref="CreateProductCommandValidator"/> which extends 
/// <see cref="AbstractValidator{T}"/> to ensure that the fields are correctly 
/// populated and follow the required rules.
/// </remarks>
public class CreateProductCommand : IRequest<CreateProductResult>
{
    /// <summary>
    /// The title of the product.
    /// </summary>
    public string Title { get; set; }

    /// <summary>
    /// The price of the product.
    /// </summary>
    public decimal Price { get; set; }

    /// <summary>
    /// A brief description of the product.
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// The category the product belongs to.
    /// </summary>
    public string Category { get; set; }

    /// <summary>
    /// The URL of the product image.
    /// </summary>
    public string Image { get; set; }

    /// <summary>
    /// The rating details of the product.
    /// </summary>
    public CreateProductRatingCommand Rating { get; set; }

    /// <summary>
    /// Validates the command object using the <see cref="CreateProductCommandValidator"/>.
    /// </summary>
    /// <returns>
    /// A <see cref="ValidationResultDetail"/> object containing validation results.
    /// </returns>
    public ValidationResultDetail Validate()
    {
        var validator = new CreateProductCommandValidator();
        var result = validator.Validate(this);
        return new ValidationResultDetail
        {
            IsValid = result.IsValid,
            Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
        };
    }
}

/// <summary>
/// Represents the rating details of a product.
/// </summary>
public class CreateProductRatingCommand
{
    /// <summary>
    /// The average rating score of the product.
    /// </summary>
    public double Rate { get; set; }

    /// <summary>
    /// The total count of ratings for the product.
    /// </summary>
    public int Count { get; set; }
}