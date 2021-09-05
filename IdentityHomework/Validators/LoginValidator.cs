using FluentValidation;
using IdentityHomework.DTO;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityHomework.Validators
{
    public class LoginValidator : AbstractValidator<LoginModel>
    {
        public LoginValidator(IConfiguration configuration)
        {
            int.TryParse(configuration["Validation:Password:MinimumLength"], out int passMinLength);

            RuleFor(u => u.Email).EmailAddress();
            RuleFor(u => u.Password).MinimumLength(passMinLength);
        }
    }
}
