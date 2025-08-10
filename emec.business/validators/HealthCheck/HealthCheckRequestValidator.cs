using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using emec.entities.HealthCheck;
using emec.shared.Contracts;
using emec.shared.models;
using static emec.shared.common.Constants;

namespace emec.business.validators.HealthCheck
{
    public class HealthCheckRequestValidator : IValidator<HealthCheckDataRequest>
    {

        private readonly IErrorMessages _errorMessages;

        //private readonly ISiteRepository _siteRepository;


        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public bool Validate(HealthCheckDataRequest obj, out ResponseMessage responseMessage)
        {
            responseMessage = null;

            if (obj.Action == ApiActions.List || obj.Action == ApiActions.Add)
            {
                if (string.IsNullOrEmpty(obj.Action))
                {
                    responseMessage = _errorMessages.Common_InvalidRequest();
                    return false;
                }

            }

            return responseMessage == null;
        }

        protected virtual void Dispose(bool disposing) { }

    }
}
