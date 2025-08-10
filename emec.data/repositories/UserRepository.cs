using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using emec.contracts.repositories;
using emec.dbcontext.tables.Models;
using Microsoft.EntityFrameworkCore;

namespace emec.data.repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly EMecContext _context;
        public UserRepository(EMecContext context)
        {
            _context = context;
        }

        public async Task<IList<string>> GetUserRolesAsync(string userName)
        {
            var user = await _context.TblUsers.FirstOrDefaultAsync(u => u.UserName == userName);
            if (user == null) return new List<string>();
            var roles = await (from ur in _context.TblUserRoles
                               join r in _context.TblRoles on ur.RoleId equals r.RoleId
                               where ur.UserId == user.UserId
                               select r.RoleName).ToListAsync();
            return roles;
        }

        public async Task<bool> ValidateUserAsync(string userName, string password)
        {
            var user = await _context.TblUsers.FirstOrDefaultAsync(u => u.UserName == userName);
            if (user == null) return false;
            // For demo, compare plain text. Replace with hash check in production.
            return user.PasswordHash == password;
        }
    }
}
