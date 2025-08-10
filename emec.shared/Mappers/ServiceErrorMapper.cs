using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using emec.shared.models;

namespace emec.shared.Mappers
{
    public class ServiceErrorMapper : IMapper<ResponseMessage, ResponseBase>
    {
        public ResponseBase Map(ResponseMessage input) => new ResponseBase
        {
            IsSuccess = false,
            Result = null,
            Error = input
            //ValidTill = DateTime.Now
        };
    }
}
