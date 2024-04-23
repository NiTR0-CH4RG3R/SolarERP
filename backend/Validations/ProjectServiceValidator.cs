using backend.Models.DTO.ProjectService;
using FluentValidation;

namespace backend.Validations
{
    public class ProjectServiceValidator : AbstractValidator<AddProjectServiceDTO>
    {
        public ProjectServiceValidator()
        {
            RuleFor(service => service.Status)
                .NotEmpty().WithMessage("It is required to fill the Status");
            RuleFor(service => service.ConductedBy)
               .NotEmpty().WithMessage("It is required to fill the Conductor Id");
            RuleFor(service => service.Description)
               .NotEmpty().WithMessage("It is required to fill the Description");
        }
    }
}
