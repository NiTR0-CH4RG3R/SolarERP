using backend.Models.DTO.ProjectCommisionReport;
using FluentValidation;

namespace backend.Validations
{
    public class ProjectCommisionReportValidator : AbstractValidator<AddProjectCommisionReportDTO>
    {
        public ProjectCommisionReportValidator()
        {
            RuleFor(report => report.URL)
                .NotEmpty().WithMessage("It is required to enter the valid report URL");
            RuleFor(report => report.CommissionDate)
                .Must(date => date.Value.Date <= DateTime.UtcNow.Date).WithMessage("Add a valid Commision date.Can't add a future date");
        }

    }
}
