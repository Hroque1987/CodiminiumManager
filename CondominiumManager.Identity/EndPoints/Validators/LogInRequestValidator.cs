using CondominiumManager.Identity.Errors;
using CondominiumManager.Identity.UserEndPoints.Requests;
using FastEndpoints;
using FluentValidation;

namespace CondominiumManager.Identity.UserEndPoints.Validators;

internal class LogInRequestValidator : Validator<LogInRequest>
{
  public LogInRequestValidator()
    {
        RuleFor(log => log.Email).Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage(IdentityErrors.LoginErrors.InvalidLogin.Message)
            .EmailAddress().WithMessage(IdentityErrors.LoginErrors.InvalidLogin.Message);
        RuleFor(log => log.Password)
            .NotEmpty().WithMessage(IdentityErrors.LoginErrors.InvalidLogin.Message);
    }
}
