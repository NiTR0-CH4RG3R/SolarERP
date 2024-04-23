using backend.Models.DTO.TaskResource;
using FluentValidation;

namespace backend.Validations
{
    public class TaskResourceValidator : AbstractValidator<AddTaskResourceDTO>
    {
        public TaskResourceValidator()
        {
            RuleFor(resorce => resorce.Category)
               .NotEmpty().WithMessage("It is required to fill the Category");
            RuleFor(resorce => resorce.URL)
               .NotEmpty().WithMessage("Empty URL or missing URL");
        }
    }
}
