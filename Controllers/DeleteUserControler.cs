using System.Threading.Tasks;
using hospital_back.Data;
using Microsoft.AspNetCore.Mvc;

namespace hospital_back.Controllers
{

    [ApiController]
    [Route("v1/delete-user/")]
    public class DeleteUserById : ControllerBase
    {
        private readonly AppDbContext context;

        public DeleteUserById(AppDbContext dbContext)
        {
            context = dbContext;
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            context.Users.Remove(user);
            await context.SaveChangesAsync();

            return NoContent();
        }
    }
}