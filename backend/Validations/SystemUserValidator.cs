using backend.Models.DTO.SystemUser;
using FluentValidation;

namespace backend.Validations
{
    public class SystemUserValidator : AbstractValidator<AddSystemUserDTO>
    {
        public SystemUserValidator()
        {
            RuleFor(user => user.FirstName)
                .NotEmpty().WithMessage("It is required to fill the First name");
            RuleFor(user => user.LastName)
               .NotEmpty().WithMessage("It is required to fill the Last name");
            RuleFor(user => user.Address)
               .NotEmpty().WithMessage("It is required to fill the Address");
            RuleFor(user => user.Email)
               .NotEmpty().WithMessage("It is required to fill the Email")
               .EmailAddress().WithMessage("Invalid Email format")
               .Matches(@"^[a-zA-Z0-9.!$'*+?/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$")
               .WithMessage("Invalid email format. Email cannot contain special characters like '#', '%', '&', or '*'.");
            RuleFor(user => user.Role)
                .NotEmpty().WithMessage("It is required to fill the Role");
            RuleFor(user => user.Phone01)
                .NotEmpty().WithMessage("It is required to fill the Mobile Number")
                .MaximumLength(12).WithMessage("Callback number must contain 12 characters")
                .Matches(@"^\+").WithMessage("Callback number must start with '+'");
            RuleFor(user => user.Phone02)
                .MaximumLength(12).WithMessage("Callback number must contain 12 characters")
                .Matches(@"^\+").WithMessage("Callback number must start with '+'");
            RuleFor(customer => customer.Username)
                .NotEmpty().WithMessage("It is required to give a Username");
            RuleFor(customer => customer.Password)
                .MinimumLength(6).WithMessage("Password must contain at least 6 characters")
                .MaximumLength(30).WithMessage("Password must not exeed 30 characters");
        }

    }
}
