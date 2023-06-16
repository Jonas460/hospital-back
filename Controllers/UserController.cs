using System;
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
    [Route(template:"v1/users")]
    public class UserController : ControllerBase
    {
    [HttpGet(template: "load")]
    public async Task<IActionResult> LoadAsync([FromServices] AppDbContext context)
    {
        using (SHA256 sha256 = SHA256.Create())
        {
            for (var i = 0; i < 10; i++)
            {
                var users = new User()
                {
                    Id = i + 1,
                    Name = $"User_{i}",
                    Email = $"email.Teste{i}@gmail.com",
                    CRM = (i + 123),
                    Role = i % 2 != 0 ? RoleType.Doctor : RoleType.Patient,
                    CPF = (i + 1234567890)
                };

                var passwordBytes = Encoding.UTF8.GetBytes("senhaPadrao");
                var hashedPasswordBytes = sha256.ComputeHash(passwordBytes);
                users.Password = BitConverter.ToString(hashedPasswordBytes).Replace("-", "");

                await context.Users.AddAsync(users);
                await context.SaveChangesAsync();
            }
        }

        return Ok();
    }
    
    [HttpGet(template: "")]
    public async Task<IActionResult> GetAsync([FromServices] AppDbContext context)
    {
        var users = await context.Users.AsNoTracking().ToListAsync();
        return Ok(users);
    }
        
    }



}