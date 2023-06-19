using hospital_back.Controllers;
using hospital_back.Data;
using hospital_back.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace hospital_back.Tests.Controllers
{
    public class SaveUserControllerTests
    {
        [Fact]
        public async Task Create_ValidUser_ReturnsOkResult()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            using (var context = new AppDbContext(options))
            {
                var controller = new SaveUserController(context);
                var userDto = new User
                {
                    Name = "John Doe",
                    Email = "john.doe@example.com",
                    Password = "password",
                    Role = Enums.RoleType.Doctor,
                    CPF = 12345678901,
                    CellPhone = 1234567890,
                    CRM = 1234
                };

                var result = await controller.Create(userDto);

                Assert.IsType<OkObjectResult>(result);
                var okResult = (OkObjectResult)result;
                Assert.Equal("Usuário cadastrado com sucesso.", okResult.Value);
                Assert.Equal(1, context.Users.Count());
                var savedUser = context.Users.Single();
                Assert.Equal("John Doe", savedUser.Name);
                Assert.Equal("john.doe@example.com", savedUser.Email);
                Assert.Equal(Enums.RoleType.Doctor, savedUser.Role);
                Assert.Equal(12345678901, savedUser.CPF);
                Assert.Equal(1234567890, savedUser.CellPhone);
                Assert.Equal(1234, savedUser.CRM);
            }
        }

        [Fact]
        public async Task Create_ExistingUser_ReturnsBadRequest()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            using (var context = new AppDbContext(options))
            {
                context.Users.Add(new User
                {
                    Name = "Jane Doe",
                    Email = "jane.doe@example.com",
                    Password = "password",
                    Role = Enums.RoleType.Doctor,
                    CPF = 98765432101,
                    CellPhone = 9876543210,
                    CRM = 5678
                });
                context.SaveChanges();
            }

            using (var context = new AppDbContext(options))
            {
                var controller = new SaveUserController(context);
                var userDto = new User
                {
                    Name = "John Doe",
                    Email = "jane.doe@example.com",
                    Password = "password",
                    Role = Enums.RoleType.Patient,
                    CPF = 12345678901,
                    CellPhone = 1234567890,
                    CRM = 1234
                };

                var result = await controller.Create(userDto);

                Assert.IsType<BadRequestObjectResult>(result);
                var badRequestResult = (BadRequestObjectResult)result;
                Assert.Equal("O usuário já existe.", badRequestResult.Value);
                Assert.Equal(1, context.Users.Count());
            }
        }
    }
}
