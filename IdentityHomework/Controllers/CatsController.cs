using IdentityHomework.DTO;
using IdentityHomework.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CatsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CatsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Authorize(Roles = "Viewer", Policy = "Over16")]
        public ActionResult<IEnumerable<Cat>> GetCats() => _context.Cats.ToList();

        [HttpGet]
        [Authorize(Policy = "ManOnly")]
        [Route("{breed}")]
        public ActionResult<IEnumerable<Cat>> GetCatsWithBreed(string breed) => _context.Cats.Where(c => c.Breed == breed).ToList();

        [HttpPost]
        [Authorize(Policy = "AdminOnly")]
        public IActionResult CreateCat(dtoCat dtoCat)
        {
            var cat = new Cat
            {
                Name = dtoCat.Name,
                Age = dtoCat.Age,
                Breed = dtoCat.Breed
            };

            _context.Cats.Add(cat);

            _context.SaveChanges();

            return Ok();
        }
    }
}