using backend.Models.DTO.Project;
using FluentValidation;

namespace backend.Validations
{
    public class ProjectValidator : AbstractValidator<AddProjectDTO>
    {
        public ProjectValidator()
        {
            RuleFor(project => project.Address)
                .NotEmpty().WithMessage("It is required to fill the Address");
            RuleFor(project => project.StartDate)
                .NotEmpty().WithMessage("It is required to fill the Start date");
            RuleFor(project => project.CustomerId)
                .NotEmpty().WithMessage("It is required to fill the Customer Id");
            RuleFor(project => project.Description)
                .NotEmpty().WithMessage("It is required to fill the Description");
            RuleFor(project => project.Status)
                .NotEmpty().WithMessage("It is required to fill the Status with valid value");
            RuleFor(project => project.LocationCoordinates)
                .NotEmpty().WithMessage("It is required to fill the LocationCoordinates with valid value");
            RuleFor(project => project.CoordinatorId)
                .NotEmpty().WithMessage("It is required to fill the Co-ordinator Id");
            RuleFor(project => project.StartDate)
                .NotEmpty().WithMessage("It is required to fill the Address")
                .Must(date => date.Value.Date <= DateTime.UtcNow.Date).WithMessage("Add a valid Start date.Can't add a future date");
            RuleFor(project => project.CommissionDate)
                .Must(date => date.Value.Date <= DateTime.UtcNow.Date).WithMessage("Add a valid Start date.Can't add a future date");
        }

    }
}
