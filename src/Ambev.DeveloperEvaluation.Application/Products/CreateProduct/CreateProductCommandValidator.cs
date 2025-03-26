using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Products.CreateProduct;

/// <summary>
/// Validator for the CreateProductCommand class.
/// </summary>
/// <remarks>
/// This validator ensures that all fields in the CreateProductCommand class are correctly 
/// populated and follow the required rules. It extends <see cref="AbstractValidator{T}"/> 
/// to provide a structured approach for defining validation logic.
/// </remarks>
public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title is required.")
            .MaximumLength(255).WithMessage("Title cannot exceed 255 characters.");

        RuleFor(x => x.Price)
            .GreaterThan(0).WithMessage("Price must be greater than 0.");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Description is required.")
            .MaximumLength(1000).WithMessage("Description cannot exceed 1000 characters.");

        RuleFor(x => x.Category)
            .NotEmpty().WithMessage("Category is required.")
            .MaximumLength(255).WithMessage("Category cannot exceed 255 characters.");

        RuleFor(x => x.Image)
            .NotEmpty().WithMessage("Image URL is required.")
            .Must(uri => Uri.TryCreate(uri, UriKind.Absolute, out _)).WithMessage("Image must be a valid URL.");

        RuleFor(x => x.Rating).SetValidator(new CreateProductRatingCommandValidator());
    }
}

/// <summary>
/// Validator for the ProductRating class.
/// </summary>
/// <remarks>
/// Ensures that the rating fields (rate and count) are valid.
/// </remarks>
public class CreateProductRatingCommandValidator : AbstractValidator<CreateProductRatingCommand>
{
    public CreateProductRatingCommandValidator()
    {
        RuleFor(x => x.Rate)
            .InclusiveBetween(0, 5).WithMessage("Rate must be between 0 and 5.");

        RuleFor(x => x.Count)
            .GreaterThanOrEqualTo(0).WithMessage("Count must be a non-negative integer.");
    }
}