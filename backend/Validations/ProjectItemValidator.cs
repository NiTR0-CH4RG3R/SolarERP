using backend.Models.DTO.ProjectItem;
using FluentValidation;

namespace backend.Validations
{
    public class ProjectItemValidator : AbstractValidator<AddProjectItemDTO>
    {
        public ProjectItemValidator()
        {
            RuleFor(item => item.ProjectId)
               .NotEmpty().WithMessage("It is required to fill the Project Id");
            RuleFor(item => item.ModuleNo)
               .NotEmpty().WithMessage("It is required to fill the Module number");
            RuleFor(item => item.SerialNo)
               .NotEmpty().WithMessage("It is required to fill the Serial number");
        }
    }
}
