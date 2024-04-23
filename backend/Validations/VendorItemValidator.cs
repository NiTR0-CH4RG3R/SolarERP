using backend.Models.DTO.VendorItem;
using FluentValidation;

namespace backend.Validations
{
    public class VendorItemValidator : AbstractValidator<AddVendorItemDTO>
    {
        public VendorItemValidator()
        {
            RuleFor(item => item.ProductName)
                .NotEmpty().WithMessage("It is required to fill the Product name");
            RuleFor(item => item.VendorId)
                .NotEmpty().WithMessage("It is required to fill the Vender Id");
            RuleFor(item => item.Price)
                .NotEmpty().WithMessage("It is required to fill the Price")
                .Must(price => price >= 0).WithMessage("Price must be greater than 0");
            RuleFor(item => item.WarrantyDuration)
                .NotEmpty().WithMessage("It is required to fill the Warranty duration");
            RuleFor(item => item.Capacity)
                .NotEmpty().WithMessage("It is required to fill the Capacity")
                .Must(capacity => capacity >= 0).WithMessage("Capacity must be greater than 0");
            RuleFor(item => item.Brand)
               .NotEmpty().WithMessage("It is required to fill the Brand");
            RuleFor(item => item.ProductCode)
                .NotEmpty().WithMessage("It is required to fill the Product code");
        }
    }
}
