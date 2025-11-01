using emec.entities.Customer.Update;
using emec.shared.Contracts;
using emec.shared.models;
using static emec.shared.common.Constants;

namespace emec.business.validators.Customer
{
    public class CustomerUpdateRequestValidator : IValidator<CustomerUpdateRequest>
    {
        private readonly IErrorMessages _errorMessages;

        public CustomerUpdateRequestValidator(IErrorMessages errorMessages)
        {
            _errorMessages = errorMessages;
        }

        public bool Validate(CustomerUpdateRequest request, out ResponseMessage message)
        {
            message = null;

            if (request == null)
            {
                message = _errorMessages.Common_InvalidRequest();
                return false;
            }

            if (request.Action != ApiActions.Edit)
            {
                message = _errorMessages.Common_ApiActionNotPermitted();
                return false;
            }

            if (request.Attributes == null)
            {
                message = _errorMessages.Common_AttributesRequired();
                return false;
            }

            var attr = request.Attributes;

            if (attr.Id == Guid.Empty)
            {
                message = _errorMessages.Customer_IdRequired();
                return false;
            }

            if (string.IsNullOrWhiteSpace(attr.FName))
            {
                message = _errorMessages.Customer_FNameRequired();
                return false;
            }

            if (string.IsNullOrWhiteSpace(attr.LName))
            {
                message = _errorMessages.Customer_LNameRequired();
                return false;
            }

            if (string.IsNullOrWhiteSpace(attr.Phone1))
            {
                message = _errorMessages.Customer_Phone1Required();
                return false;
            }

            return true;
        }

        public void Dispose() { /* No resources to dispose */ }
    }
}
