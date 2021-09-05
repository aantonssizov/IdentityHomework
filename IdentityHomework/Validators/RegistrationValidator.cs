using FluentValidation;
using IdentityHomework.DTO;
using IdentityHomework.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityHomework.Validators
{
    public class RegistrationValidator : AbstractValidator<RegisterModel>
    {
        public RegistrationValidator(IConfiguration configuration)
        {
            int.TryParse(configuration["Validation:Password:MinimumLength"], out int passMinLength);

            RuleFor(u => u.Username).NotEmpty();
            RuleFor(u => u.Email).EmailAddress();
            RuleFor(u => u.Password).MinimumLength(passMinLength);
            RuleFor(u => u.Age).GreaterThanOrEqualTo(0).LessThanOrEqualTo(120);
            RuleFor(u => u.Sex).IsInEnum();
        }
    }
}
