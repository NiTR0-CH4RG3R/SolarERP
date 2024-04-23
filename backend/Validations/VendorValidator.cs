using backend.Models.DTO.Vendor;
using FluentValidation;

namespace backend.Validations
{
    public class VendorValidator : AbstractValidator<AddVendorDTO>
    {
        public VendorValidator()
        {
            RuleFor(vendor => vendor.Name)
               .NotEmpty().WithMessage("It is required to fill the Name");
            RuleFor(vendor => vendor.Address)
               .NotEmpty().WithMessage("It is required to fill the Address");
            RuleFor(vendor => vendor.Email)
               .NotEmpty().WithMessage("It is required to fill the Email")
               .EmailAddress().WithMessage("Invalid Email format")
               .Matches(@"^[a-zA-Z0-9.!$'*+?/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$")
               .WithMessage("Invalid email format. Email cannot contain special characters like '#', '%', '&', or '*'.");
            RuleFor(vendor => vendor.Phone01)
                .NotEmpty().WithMessage("It is required to fill the Mobile Number")
                .MaximumLength(12).WithMessage("Callback number must contain 12 characters")
                .Matches(@"^\+").WithMessage("Callback number must start with '+'");
            RuleFor(vendor => vendor.Phone02)
                .MaximumLength(12).WithMessage("Callback number must contain 12 characters")
                .Matches(@"^\+").WithMessage("Callback number must start with '+'");
            RuleFor(vendor => vendor.Name)
               .NotEmpty().WithMessage("It is required to fill the First name");
            RuleFor(vendor => vendor.VendorRegistrationNumber)
               .NotEmpty().WithMessage("It is required to fill the Registration number");
        }
    }
}
