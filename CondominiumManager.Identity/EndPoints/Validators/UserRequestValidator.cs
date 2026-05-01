using CondominiumManager.Identity.Domain.Entities;
using CondominiumManager.Identity.Domain.ValueObjects;
using CondominiumManager.Identity.Errors;
using CondominiumManager.Identity.UserEndPoints.Requests;
using FastEndpoints;
using FluentValidation;

namespace CondominiumManager.Identity.UserEndPoints.Validators;

internal class UserRequestValidator : Validator<UserRequest>
{
    public UserRequestValidator()
    {
        RuleFor(user => user.FirstName).Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage(IdentityErrors.FullNameErrors.FirstNameEmpty.Message)
            .MaximumLength(FullName.MaxFirstNameLength).WithMessage(IdentityErrors.FullNameErrors.FirstNameTooLong.Message);

        RuleFor(user => user.LastName).Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage(IdentityErrors.FullNameErrors.LastNameEmpty.Message)
            .MaximumLength(FullName.MaxLastNameLength).WithMessage(IdentityErrors.FullNameErrors.LastNameTooLong.Message);
        RuleFor(user => user.Email).Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage(IdentityErrors.EmailErrors.Empty.Message)
            .EmailAddress().WithMessage(IdentityErrors.EmailErrors.InvalidFormat.Message);

        RuleFor(user => user.Password)
            .Matches(User.PasswordRegex).WithMessage(IdentityErrors.UserErrors.EmptyPassword.Message);
	}
}
