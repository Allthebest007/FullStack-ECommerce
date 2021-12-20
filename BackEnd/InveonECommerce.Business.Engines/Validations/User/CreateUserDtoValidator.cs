using FluentValidation;
using InveonECommerce.Business.Engines.DTOs.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InveonECommerce.Business.Engines.Validations.User
{
    public class CreateUserDtoValidator : AbstractValidator<CreateUserDTO>
    {
        public CreateUserDtoValidator()
        {
            RuleFor(x => x.Email).NotEmpty().WithMessage("Please enter a email").EmailAddress().WithMessage("Please enter a valid email");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Please enter a password");
            RuleFor(x => x.UserName).NotEmpty().WithMessage("Please enter a userName");
           
        }
    }
}
