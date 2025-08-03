using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using emec.shared.models;

namespace emec.entities.Login
{
    public class LoginDataRequest : RequestBase
    {
        public required string Action { get; set; }

        public required LoginDataAttribute Attributes { get; set; }
    }
}
