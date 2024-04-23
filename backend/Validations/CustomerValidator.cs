using backend.Models.DTO.Customer;
using FluentValidation;

namespace backend.Validations
{
    public class CustomerValidator : AbstractValidator<AddCustomerDTO>
    {
        public CustomerValidator()
        {
            RuleFor(customer => customer.FirstName)
                .NotEmpty().WithMessage("It is required to fill the First name");
            RuleFor(customer => customer.LastName)
               .NotEmpty().WithMessage("It is required to fill the Last name");
            RuleFor(customer => customer.Address)
               .NotEmpty().WithMessage("It is required to fill the Address");
            RuleFor(customer => customer.Email)
               .NotEmpty().WithMessage("It is required to fill the Email")
               .EmailAddress().WithMessage("Invalid Email format")
               //.Must(email => !email.Any(cha => cha == '#')).WithMessage("Email cannot contain '#' symbol.")
               .Matches(@"^[a-zA-Z0-9.!$'*+?/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$")
               .WithMessage("Invalid email format. Email cannot contain special characters like '#', '%', '&', or '*'.");
            RuleFor(customer => customer.Category)
                .NotEmpty().WithMessage("Catagory level must fill with a given value");
            RuleFor(customer => customer.Phone01)
                .NotEmpty().WithMessage("It is required to fill the Mobile Number")
                .MaximumLength(12).WithMessage("Callback number must contain 12 characters")
                .Matches(@"^\+").WithMessage("Callback number must start with '+'");
            RuleFor(customer => customer.Phone02)
                .MaximumLength(12).WithMessage("Callback number must contain 12 characters")
                .Matches(@"^\+").WithMessage("Callback number must start with '+'");
        }
    }
}

