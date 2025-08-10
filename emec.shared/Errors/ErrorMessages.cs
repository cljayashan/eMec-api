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
        public ResponseMessage Common_InvalidRequest()
        {
            throw new NotImplementedException();
        }

        public ResponseMessage GetMessageServerError() => new ResponseMessage
        {
            Code = "E0001",
            Message = Errors.E0001
        };
    }
}
