using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using emec.entities.HealthCheck;
using emec.entities.Login;
using emec.shared.Contracts;
using emec.shared.models;
using static emec.shared.common.Constants;

namespace emec.business.validators.Login
{
    public class LoginRequestValidator : IValidator<LoginDataRequest>
    {

        private readonly IErrorMessages _errorMessages;

        public LoginRequestValidator(IErrorMessages errorMessages)
        {
            _errorMessages = errorMessages ?? throw new ArgumentNullException(nameof(errorMessages));
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing) { }

        public bool Validate(LoginDataRequest loginDataRequest, out ResponseMessage responseMessage)
        {
            responseMessage = null;

            if (loginDataRequest.Action == ApiActions.Authenticate)
            {
                if (string.IsNullOrEmpty(loginDataRequest.Attributes.UserName))
                {
                    responseMessage = _errorMessages.Common_InvalidRequest();
                    return false;
                }

                if (string.IsNullOrEmpty(loginDataRequest.Attributes.Password))
                {
                    responseMessage = _errorMessages.Common_InvalidRequest();
                    return false;
                }
            }
            return responseMessage == null;           
        }
    }
}
