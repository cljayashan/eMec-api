using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using emec.shared.Contracts;
using emec.shared.models;
using static System.Runtime.InteropServices.JavaScript.JSType;
using emec.shared.Errors; // Use the resource class

namespace emec.shared.Errors
{
    public class ErrorMessages : IErrorMessages
    {
        public ResponseMessage Common_InvalidRequest() => new ResponseMessage
        {
            Code = "E0001",
            Message = ErrorResource.E0001
        };

        public ResponseMessage Common_ApiActionNotPermitted() => new ResponseMessage
        {
            Code = "E0002",
            Message = ErrorResource.E0002
        };

        public ResponseMessage Common_InvalidOrExpiredRefreshToken() => new ResponseMessage
        {
            Code = "E0003",
            Message = ErrorResource.E0003
        };

        public ResponseMessage Vehicle_Null() => new ResponseMessage
        {
            Code = "E0004",
            Message = ErrorResource.E0004
        };
        public ResponseMessage Vehicle_IdRequired() => new ResponseMessage
        {
            Code = "E0005",
            Message = ErrorResource.E0005
        };
        public ResponseMessage Vehicle_ProvinceRequired() => new ResponseMessage
        {
            Code = "E0006",
            Message = ErrorResource.E0006
        };
        public ResponseMessage Vehicle_PrefixRequired() => new ResponseMessage
        {
            Code = "E0007",
            Message = ErrorResource.E0007
        };
        public ResponseMessage Vehicle_NumberRequired() => new ResponseMessage
        {
            Code = "E0008",
            Message = ErrorResource.E0008
        };
        public ResponseMessage Vehicle_BrandRequired() => new ResponseMessage
        {
            Code = "E0009",
            Message = ErrorResource.E0009
        };
        public ResponseMessage Vehicle_ModelRequired() => new ResponseMessage
        {
            Code = "E0010",
            Message = ErrorResource.E0010
        };
        public ResponseMessage Vehicle_VersionRequired() => new ResponseMessage
        {
            Code = "E0011",
            Message = ErrorResource.E0011
        };
        public ResponseMessage Vehicle_YoMRequired() => new ResponseMessage
        {
            Code = "E0012",
            Message = ErrorResource.E0012
        };
        public ResponseMessage Vehicle_YoRRequired() => new ResponseMessage
        {
            Code = "E0013",
            Message = ErrorResource.E0013
        };

        public ResponseMessage Vehicle_NumberMustHave4Digits() => new ResponseMessage
        {
            Code = "E0014",
            Message = ErrorResource.E0014
        };

        public ResponseMessage Common_SearchKeywordIsRequired() => new ResponseMessage
        {
            Code = "E0015",
            Message = ErrorResource.E0015
        };
    }
}
