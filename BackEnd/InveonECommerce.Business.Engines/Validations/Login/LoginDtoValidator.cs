using FluentValidation;
using InveonECommerce.Business.Engines.DTOs.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InveonECommerce.Business.Engines.Validations.Login
{
    public class LoginDtoValidator : AbstractValidator<LoginDTO>
    {
        public LoginDtoValidator()
        {
            RuleFor(x => x.Email).NotEmpty().WithMessage("Email field is required").EmailAddress().WithMessage("Please enter a valid email");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Please enter a password");
        }
    }
}
