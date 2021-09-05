using IdentityHomework.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityHomework.Interfaces
{
    public interface IJwtGenerator
    {
        string GenerateToken(AppUser user, IEnumerable<string> roles);
    }
}
