using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityHomework.Models
{
    public class AppUser : IdentityUser
    {
        public int Age { get; set; }
        public Sex Sex { get; set; }
        public DateTime Created { get; set; }
    }

    public enum Sex
    {
        Male,
        Female,
        Unknown
    }
}
