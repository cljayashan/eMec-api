using System.Collections.Generic;
using System.Threading.Tasks;

namespace emec.contracts.repositories
{
    public interface IUserRepository
    {
        Task<IList<string>> GetUserRolesAsync(string userName);
        Task<bool> ValidateUserAsync(string userName, string password);
        Task StoreRefreshTokenAsync(string userName, string refreshToken, DateTime expiry);
        Task<bool> ValidateRefreshTokenAsync(string userName, string refreshToken);
    }
}
