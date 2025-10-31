using emec.entities.Customer;
using emec.shared.Contracts;
using emec.shared.Errors;
using emec.shared.models;
using static emec.shared.common.Constants;

namespace emec.business.validators.Customer
{
    public class SearchCustomerDataRequestValidator : IValidator<CustomerDataRequest>
    {
        private readonly IErrorMessages _errorMessages;

        public SearchCustomerDataRequestValidator(IErrorMessages errorMessages)
        {
            _errorMessages = errorMessages;
        }

        public bool Validate(CustomerDataRequest request, out ResponseMessage message)
        {
            message = new ResponseMessage();

            if (request == null || string.IsNullOrWhiteSpace(request.Action))
            {
                message.Message = "Invalid request.";
                return false;
            }

            if (request.Action == ApiActions.List)
            {

                if (request.Args == null || request.Args.Length == 0)
                {
                    //TODO: additional validations can be added here in future if needed  
                }
                else if(request.Args[0].Equals("dropdowndata",StringComparison.OrdinalIgnoreCase))
                {
                    //if (string.IsNullOrWhiteSpace(request.Attributes.FName))
                    //{
                    //    message = _errorMessages.Common_SearchKeywordIsRequired();
                    //    return false;
                    //}
                }
            }


            return true;
        }
        public void Dispose() { }
    }
}
