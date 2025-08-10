using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using emec.shared.Contracts;
using emec.shared.models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace emec.shared.Errors
{
    public class ErrorMessages : IErrorMessages
    {
        
        public ResponseMessage Common_InvalidRequest() => new ResponseMessage
        {
            Code = "E0001",
            Message = Errors.E0001
        };

        public ResponseMessage Common_ApiActionNotPermitted() => new ResponseMessage
        {
            Code = "E0002",
            Message = Errors.E0002
        };

    }
}
