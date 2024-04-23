using backend.Models.DTO.ProjectTest;
using FluentValidation;
using static System.Net.Mime.MediaTypeNames;

namespace backend.Validations
{
    public class ProjectTestValidator : AbstractValidator<AddProjectTestDTO>
    {
        public ProjectTestValidator()
        {
            RuleFor(test => test.Name)
                .NotEmpty().WithMessage("It is required to fill the Test name");
            RuleFor(test => test.Passed)
               .NotEmpty().WithMessage("It is required to fill the Result");
            RuleFor(test => test.ConductedBy)
               .NotEmpty().WithMessage("It is required to fill the Condutor Id");
            RuleFor(test => test.ConductedDate)
               .NotEmpty().WithMessage("It is required to fill the Conducted date")
               .Must(IsValidDate).WithMessage("Conducted date must be a valid date");
        }

        private bool IsValidDate(DateTime? date)
        {
            return date.HasValue && date.Value.Date <= DateTime.UtcNow.Date;
        }
    }
}
