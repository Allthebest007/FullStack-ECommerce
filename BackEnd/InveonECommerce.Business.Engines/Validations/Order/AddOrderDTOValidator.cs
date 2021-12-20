using FluentValidation;
using InveonECommerce.Business.Engines.DTOs.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InveonECommerce.Business.Engines.Validations.Order
{
    public class AddOrderDTOValidator : AbstractValidator<AddOrderDTO>
    {
        public AddOrderDTOValidator()
        {   
            RuleFor(x => x.AddressInfo.Name).NotEmpty().WithMessage("Name field is required");
            RuleFor(x => x.AddressInfo.Surname).NotEmpty().WithMessage("Surname field is required");
            RuleFor(x => x.AddressInfo.PhoneNumber).NotEmpty().WithMessage("PhoneNumber field is required");
            RuleFor(x => x.AddressInfo.DeliveryAddress).NotEmpty().WithMessage("DeliveryAddress field is required");
            RuleFor(x => x.ItemsIds).NotEmpty().WithMessage("There is no item in basket");
        }
    
    }
}
