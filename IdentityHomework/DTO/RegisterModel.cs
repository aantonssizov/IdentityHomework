using IdentityHomework.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityHomework.DTO
{
    public class RegisterModel
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public int Age { get; set; }
        public Sex Sex { get; set; }

        public List<string> Roles { get; set; }
    }
}
