using CondominiumManager.Identity.Domain.ValueObjects;
using CondominiumManager.Identity.UserEndPoints.Requests;
using FastEndpoints;
using FluentValidation;

namespace CondominiumManager.Identity.UserEndPoints.Validators;

internal class UserRequestValidator : Validator<UserRequest>
{
    public UserRequestValidator()
    {
        RuleFor(user => user.FirstName).NotEmpty().WithMessage("First name is required").MaximumLength(FullName.MaxFirstNameLength).WithMessage($"First name max lenght {FullName.MaxFirstNameLength} characters");
        RuleFor(user => user.LastName).NotEmpty().WithMessage("Last name is required").MaximumLength(FullName.MaxLastNameLength).WithMessage($"Last name max lenght {FullName.MaxLastNameLength} characters");
        RuleFor(user => user.Email).NotEmpty().WithMessage("Email is required").EmailAddress().WithMessage("Invalid Email");
        RuleFor(user => user.Password).Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^A-Za-z\d]).{8,}$").WithMessage("Password is too weak");
	}
}
