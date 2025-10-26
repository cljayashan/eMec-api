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

        ResponseMessage Common_InvalidOrExpiredRefreshToken();

        // Vehicle validation errors
        ResponseMessage Vehicle_Null();

        ResponseMessage Vehicle_IdRequired();

        ResponseMessage Vehicle_ProvinceRequired();

        ResponseMessage Vehicle_PrefixRequired();

        ResponseMessage Vehicle_NumberRequired();

        ResponseMessage Vehicle_BrandRequired();

        ResponseMessage Vehicle_ModelRequired();

        ResponseMessage Vehicle_VersionRequired();

        ResponseMessage Vehicle_YoMRequired();

        ResponseMessage Vehicle_YoRRequired();

        ResponseMessage Vehicle_NumberMustHave4Digits();
    }
}
