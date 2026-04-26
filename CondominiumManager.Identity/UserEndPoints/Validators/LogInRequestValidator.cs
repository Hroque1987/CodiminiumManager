using CondominiumManager.Identity.UserEndPoints.Requests;
using FastEndpoints;
using FluentValidation;

namespace CondominiumManager.Identity.UserEndPoints.Validators;

internal class LogInRequestValidator : Validator<LogInRequest>
{
  public LogInRequestValidator()
    {
        RuleFor(log => log.Email).NotEmpty().WithMessage("Email is required").EmailAddress().WithMessage("Invalid Email");
        RuleFor(log => log.Password).NotEmpty().WithMessage("Password Required");
    }
}
