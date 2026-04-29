using CondominiumManager.Condominium.CondominiumEndPoints.Requests;
using FastEndpoints;
using FluentValidation;

namespace CondominiumManager.Condominium.CondominiumEndPoints.Validators;

internal class CreateBuildingRequestValidator : Validator<CreateBuildingRequest>
{
    public CreateBuildingRequestValidator()
    {
        RuleFor(request => request.Name).NotEmpty().WithMessage("Name is required").MaximumLength(200).WithMessage("Name max length is 200 characters");
        RuleFor(request => request.Street).NotEmpty().WithMessage("Street is required").MaximumLength(100).WithMessage("Street max length is 100 characters");
        RuleFor(request => request.DoorNumber).NotEmpty().WithMessage("DoorNumber is required").MaximumLength(100).WithMessage("DoorNumber max length is 100 characters");
        RuleFor(request => request.PostalCode).NotEmpty().WithMessage("PostalCode is required").MaximumLength(100).WithMessage("PostalCode max length is 100 characters");
        RuleFor(request => request.City).NotEmpty().WithMessage("City is required").MaximumLength(100).WithMessage("City max length is 100 characters");
        RuleFor(request => request.Country).NotEmpty().WithMessage("Country is required").MaximumLength(100).WithMessage("Country max length is 100 characters");
      
    }
}
