using FluentValidation;
using IdentityHomework.DTO;
using IdentityHomework.Validators;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityHomework
{
    public static class ServicesExtensions
    {
        public static void AddValidators(this IServiceCollection services)
        {
            services.AddTransient<IValidator<RegisterModel>, RegistrationValidator>();
            services.AddTransient<IValidator<LoginModel>, LoginValidator>();
        }
    }
}
