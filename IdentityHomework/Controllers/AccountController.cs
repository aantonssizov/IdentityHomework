using IdentityHomework.DTO;
using IdentityHomework.Interfaces;
using IdentityHomework.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityHomework.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IJwtGenerator _jwtGenerator;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IJwtGenerator jwtGenerator, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtGenerator = jwtGenerator;
            _roleManager = roleManager;
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Register(RegisterModel registerModel)
        {
           var user = new AppUser
            {
                UserName = registerModel.Username,
                Email = registerModel.Email,
                Age = registerModel.Age,
                Sex = registerModel.Sex,
                Created = DateTime.Now
            };

            var result = await _userManager.CreateAsync(user, registerModel.Password);

            if (result.Succeeded)
            {
                await checkRolesAsync();

                var rolesResult = await _userManager.AddToRolesAsync(user, registerModel.Roles);

                if (rolesResult.Succeeded)
                { 
                    return Ok(new User
                    {
                        UserName = user.UserName,
                        Token = _jwtGenerator.GenerateToken(user, registerModel.Roles)
                    });
                }
            }

            foreach (var identityError in result.Errors)
            {
                ModelState.AddModelError("Register", identityError.Description);
            }

            return BadRequest(ModelState);
        }

        private async Task checkRolesAsync()
        {
            if (!_roleManager.Roles.Any())
            {
                await _roleManager.CreateAsync(new IdentityRole { Name = "Admin" });
                await _roleManager.CreateAsync(new IdentityRole { Name = "Viewer" });
                await _roleManager.CreateAsync(new IdentityRole { Name = "Blocked" });
            }
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Login(LoginModel loginModel)
        {
            var user = await _userManager.FindByEmailAsync(loginModel.Email);

            if (user == null)
            {
                ModelState.AddModelError("Login", "There is no user with this email");
                return BadRequest(ModelState);
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginModel.Password, false);

            if (result.Succeeded)
            {
                var userRoles = await _userManager.GetRolesAsync(user);

                return Ok(new User
                {
                    UserName = user.UserName,
                    Token = _jwtGenerator.GenerateToken(user, userRoles)
                });
            }

            ModelState.AddModelError("Login", "Incorrect password");

            return BadRequest(ModelState);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Ok();
        }
    }
}
