using WebApplication1.Entities;

namespace WebApplication1.Repositories
{
    public interface IUserRepository
    {
        Task<ApplicationUser> GetUserById(int id); 
    }
}
