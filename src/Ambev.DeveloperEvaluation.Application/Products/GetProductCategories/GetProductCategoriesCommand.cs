using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.GetProductCategories;

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
public class GetProductCategoriesCommand : IRequest<List<string>>
{
}