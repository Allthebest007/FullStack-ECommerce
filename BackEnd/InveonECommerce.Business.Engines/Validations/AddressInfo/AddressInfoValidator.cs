using FluentValidation;
using InveonECommerce.Business.Engines.DTOs.AddressInfo;

namespace InveonECommerce.Business.Engines.Validations.AddressInfo
{
    public class AddressInfoValidator : AbstractValidator<AddressInfoDTO>
    {
        public AddressInfoValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name field is required");
            RuleFor(x => x.Surname).NotEmpty().WithMessage("Surname field is required");
            RuleFor(x => x.PhoneNumber).NotEmpty().WithMessage("PhoneNumber field is required");
            RuleFor(x => x.DeliveryAddress).NotEmpty().WithMessage("DeliveryAddress field is required");
        }
    }
}
