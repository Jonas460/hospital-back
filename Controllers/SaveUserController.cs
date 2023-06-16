using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using hospital_back.Models;
using hospital_back.Data;
using System.Security.Cryptography;
using System.Text;
using System;
using System.Linq;

namespace hospital_back.Controllers
{
    [Route("v1/save/user")]
    [ApiController]
    public class SaveUserController : ControllerBase
    {
        private readonly AppDbContext _context;

        public SaveUserController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] User userDto)
        {
            var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == userDto.Email);
            var lastUser = await _context.Users
            .OrderByDescending(u => u.Id)
            .FirstOrDefaultAsync();

            if (existingUser != null)
            {
                return BadRequest("O usuário já existe.");
            }

            userDto.Id = (lastUser?.Id ?? 0) + 1;

            using (var sha256 = SHA256.Create())
            {
                byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(userDto.Password));
                string hashedPassword = BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();

                userDto.Password = hashedPassword;
            }

            var user = new User
            {
                Id = userDto.Id,
                Name = userDto.Name,
                Email = userDto.Email,
                Password = userDto.Password,
                Role = userDto.Role
                // Outros campos do usuário
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
