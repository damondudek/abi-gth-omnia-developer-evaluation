using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.CreateProduct;

public class CreateProductRequestValidator : AbstractValidator<CreateProductRequest>
{
    public CreateProductRequestValidator()
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

        RuleFor(x => x.Rating).SetValidator(new CreateProductRatingRequestValidator());
    }
}

/// <summary>
/// Validates the ProductRating data.
/// </summary>
public class CreateProductRatingRequestValidator : AbstractValidator<CreateProductRatingRequest>
{
    public CreateProductRatingRequestValidator()
    {
        RuleFor(x => x.Rate)
            .InclusiveBetween(0, 5).WithMessage("Rate must be between 0 and 5.");

        RuleFor(x => x.Count)
            .GreaterThanOrEqualTo(0).WithMessage("Count must be a non-negative integer.");
    }
}
