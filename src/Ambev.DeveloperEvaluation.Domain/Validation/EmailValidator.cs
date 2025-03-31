using FluentValidation;

namespace Ambev.DeveloperEvaluation.Domain.Validation;

public class EmailValidator : AbstractValidator<string>
{
    public EmailValidator()
    {
        RuleFor(email => email)
            .NotEmpty()
            .WithMessage("The email address cannot be empty.")
            .MaximumLength(100)
            .WithMessage("The email address cannot be longer than 100 characters.")
            .EmailAddress()
            .WithMessage("The provided email address is not valid.");
    }
}
