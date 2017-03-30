using PollService.Data.Model;
using System.Threading.Tasks;
using System.Security.Principal;
using PollService.Data;
using System.Data.Entity;

namespace PollService.Security
{
    public interface IUserManager
    {
        Task<User> GetUserAsync(IPrincipal user);
    }

    public class UserManager : IUserManager
    {
        public UserManager(IPollServiceContext context)
        {
            _context = context;
        }

        public async Task<User> GetUserAsync(IPrincipal user) => await _context
            .Users
            .Include(x=>x.Tenant)
            .SingleAsync(x => x.Username == user.Identity.Name);

        protected readonly IPollServiceContext _context;
    }
}
