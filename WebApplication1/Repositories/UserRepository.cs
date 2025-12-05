using Microsoft.EntityFrameworkCore;
using WebApplication1.Entities;

namespace WebApplication1.Repositories
{
    public class UserRepository(AppDbContext Context) : IUserRepository
    {
        public async Task<ApplicationUser> GetUserById(int id)
        {
            var user = await Context.Users.FirstOrDefaultAsync(_ => _.UserNumber == id);
            return user;
        }
    }
}
