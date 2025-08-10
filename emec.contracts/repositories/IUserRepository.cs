using System.Collections.Generic;
using System.Threading.Tasks;

namespace emec.contracts.repositories
{
    public interface IUserRepository
    {
        Task<IList<string>> GetUserRolesAsync(string userName);
        Task<bool> ValidateUserAsync(string userName, string password);
    }
}
