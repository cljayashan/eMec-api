using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace emec.shared.models
{
    public class ResponseMessage
    {
        public string Code { get; set; }

        public string Message { get; set; }
        
        public string MessageType { get; set; }

    }
}
