using backend.Models.DTO.Task;
using FluentValidation;

namespace backend.Validations
{
    public class TaskValidator : AbstractValidator<AddTaskDTO>
    {
        public TaskValidator() { 
            RuleFor(task => task.CallBackNumber)
                .MaximumLength(12).WithMessage("Callback number must contain 12 characters")
                .Matches(@"^\+").WithMessage("Callback number must start with '+'");
            RuleFor(task => task.Category)
                .NotEmpty().WithMessage("Category must fill with a given value");
            RuleFor(task => task.UrgencyLevel)
                .NotEmpty().WithMessage("Urgency level must fill with a given value");
        }
    }
}
