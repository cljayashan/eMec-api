using System.Collections.Generic;
using System.Threading.Tasks;
using emec.entities.Login;
using emec.shared.models;

namespace emec.contracts.managers
{
    public interface IUserManager
    {
        Task<IList<string>> GetUserRolesAsync(string userName);
        Task<ResponseBase> Authenticate(LoginDataRequest loginDataRequest);
        Task<ResponseBase> RefreshToken(string userName, string password);

    }
}
