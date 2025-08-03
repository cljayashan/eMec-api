using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace emec.shared.models
{
    public class ResponseBase
    {
        public bool IsSuccess { get; set; }
        public dynamic Result { get; set; }
        public ResponseMessage Error { get; set; }
    }
}
