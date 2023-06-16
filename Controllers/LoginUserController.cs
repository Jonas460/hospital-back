
using System;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using hospital_back.Data;
using hospital_back.Enums;
using hospital_back.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace hospital_back.Controllers
{
    [ApiController]
    [Route("v1/login")]
    public class LoginController : ControllerBase
    {
        private readonly AppDbContext context;

        public LoginController(AppDbContext dbContext)
        {
            context = dbContext;
        }

        [HttpPost]
        public async Task<IActionResult> Login(Login loginDto)
        {
            if (string.IsNullOrEmpty(loginDto.Email))
            {
                return BadRequest("O campo Email é obrigatório");
            }

            if (string.IsNullOrEmpty(loginDto.Password))
            {
                return BadRequest("O campo Senha é obrigatório");
            }

            var user = await context.Users.FirstOrDefaultAsync(u => u.Email == loginDto.Email);
            loginDto.Role = user.Role;

            if (user == null)
            {
                return NotFound("Usuário não encontrado");
            }

            if (!VerifyPassword(loginDto.Password, user.Password))
            {
                return BadRequest("Senha incorreta");
            }
            if (loginDto.Role == RoleType.Doctor)
            {
                var patients = await context.Users
                    .Where(u => u.Role == RoleType.Patient)
                    .ToListAsync();

                return Ok(patients);
            }
            else if (user.Role == RoleType.Patient)
            {
                return Ok(user);
            }

            return Ok("Login bem-sucedido");
        }

        private bool VerifyPassword(string enteredPassword, string storedPassword)
        {
            using (var sha256 = SHA256.Create())
            {
                byte[] enteredPasswordBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(enteredPassword));
                string hashedPassword = BitConverter.ToString(enteredPasswordBytes).Replace("-", "").ToLower();

                if (hashedPassword == storedPassword)
                {
                    return true; 
                }
                else
                {
                    return false; 
                }
            }
        }

    }
}