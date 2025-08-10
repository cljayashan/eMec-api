using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using emec.shared.models;

namespace emec.shared.Contracts
{
    public interface IErrorMessages
    {
        //1
        ResponseMessage Common_InvalidRequest(); 

        //2
        ResponseMessage Common_ApiActionNotPermitted();
    }
}
