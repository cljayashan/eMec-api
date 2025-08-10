using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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

            // When storing a new password, use:
            // user.PasswordHash = PasswordHelper.HashPassword(password, user.UserName);


            //string hash2;
            //string salt2;
            //PasswordHelper.HashPassword("u", out hash2, out salt2);

            var user = await _context.TblUsers.FirstOrDefaultAsync(u => u.UserName == userName);
            if (user == null) return false;
            // Use username as salt for demonstration. Ideally, store a unique salt per user.
            return PasswordHelper.VerifyPassword(password, user.Salt, user.PasswordHash);
        }



        internal static class PasswordHelper
        {
            private const int SaltSize = 16; // 128-bit salt
            private const int HashSize = 32; // 256-bit hash
            private const int Iterations = 100_000; // Increase if performance allows

            // Generates a salt and hashes the password using PBKDF2
            public static void HashPassword(string password, out string hash, out string salt)
            {
                // Generate random salt
                byte[] saltBytes = new byte[SaltSize];
                using var rng = RandomNumberGenerator.Create();
                rng.GetBytes(saltBytes);
                salt = Convert.ToBase64String(saltBytes);

                // Hash the password with the salt
                using var pbkdf2 = new Rfc2898DeriveBytes(password, saltBytes, Iterations, HashAlgorithmName.SHA256);
                byte[] hashBytes = pbkdf2.GetBytes(HashSize);
                hash = Convert.ToBase64String(hashBytes);
            }

            // Verifies a password against the stored hash and salt
            public static bool VerifyPassword(string password, string storedSalt, string storedHash)
            {
                byte[] saltBytes = Convert.FromBase64String(storedSalt);
                using var pbkdf2 = new Rfc2898DeriveBytes(password, saltBytes, Iterations, HashAlgorithmName.SHA256);
                byte[] hashBytes = pbkdf2.GetBytes(HashSize);
                string computedHash = Convert.ToBase64String(hashBytes);
                return computedHash == storedHash;
            }
        }
    }
}
