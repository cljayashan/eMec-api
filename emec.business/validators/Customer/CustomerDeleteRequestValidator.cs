using emec.entities.Customer.Delete;
using emec.shared.Contracts;
using emec.shared.models;
using static emec.shared.common.Constants;

namespace emec.business.validators.Customer
{
    public class CustomerDeleteRequestValidator : IValidator<CustomerDeleteRequest>
    {
        private readonly IErrorMessages _errorMessages;

        public CustomerDeleteRequestValidator(IErrorMessages errorMessages)
        {
            _errorMessages = errorMessages;
        }

        public bool Validate(CustomerDeleteRequest request, out ResponseMessage message)
        {
            message = null;

            if (request == null)
            {
                message = _errorMessages.Common_InvalidRequest();
                return false;
            }

            if (request.Action != ApiActions.Delete)
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
