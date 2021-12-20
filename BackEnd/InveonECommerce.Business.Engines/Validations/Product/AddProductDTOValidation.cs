using FluentValidation;
using InveonECommerce.Business.Engines.DTOs.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InveonECommerce.API.Validations.Product
{
    public class AddProductDTOValidation : AbstractValidator<AddProductDTO>
    {
        public AddProductDTOValidation()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name field is required");
            RuleFor(x => x.ProductImage).NotEmpty().WithMessage("Image field is required");
            RuleFor(x => x.UnitsInStock).NotEmpty().WithMessage("Quantity field is required");
            RuleFor(x => x.Price).NotEmpty().WithMessage("Price field is required");
            RuleFor(x => x.CategoryId).NotEmpty().WithMessage("Category field is required");
        }
    }
}
