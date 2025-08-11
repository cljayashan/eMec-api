using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace emec.entities.Login
{
    public class TokenRefreshAttribute
    {
        public required string UserName { get; set; }
        public required string RefreshToken { get; set; }
    }
}
