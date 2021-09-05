using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace IdentityHomework.Requirements
{
    public class AgeRequirementHandler : AuthorizationHandler<AgeRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, AgeRequirement requirement)
        {
            if (context.User.HasClaim(u => u.Type == OwnClaims.Age))
            {
                int.TryParse(context.User.FindFirstValue(OwnClaims.Age), out int age);

                if (age >= requirement.Age)
                {
                    context.Succeed(requirement);
                }
            }

            return Task.CompletedTask;
        }
    }
}
