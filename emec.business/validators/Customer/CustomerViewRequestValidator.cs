using emec.entities.Customer.View;
using emec.shared.Contracts;
using emec.shared.models;
using static emec.shared.common.Constants;

namespace emec.business.validators.Customer
{
    public class CustomerViewRequestValidator : IValidator<CustomerViewRequest>
    {
        private readonly IErrorMessages _errorMessages;

        public CustomerViewRequestValidator(IErrorMessages errorMessages)
        {
            _errorMessages = errorMessages;
        }

        public bool Validate(CustomerViewRequest request, out ResponseMessage message)
        {
            message = null;

            if (request == null)
            {
                message = _errorMessages.Common_InvalidRequest();
                return false;
            }

            if (request.Action != ApiActions.View)
            {
                message = _errorMessages.Common_ApiActionNotPermitted();
                return false;
            }

            if (request.Attributes == null)
            {
                message = _errorMessages.Common_AttributesRequired();
                return false;
            }

            if (request.Attributes.CustomerId == Guid.Empty)
            {
                message = _errorMessages.Customer_IdRequired();
                return false;
            }

            return true;
        }

        public void Dispose() { /* No resources to dispose */ }
    }
}
