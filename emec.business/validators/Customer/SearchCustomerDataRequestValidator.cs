using emec.entities.Customer;
using emec.shared.models;
using emec.shared.Contracts;

namespace emec.business.validators.Customer
{
    public class SearchCustomerDataRequestValidator : IValidator<SearchCustomerDataRequest>
    {
        public bool Validate(SearchCustomerDataRequest obj, out ResponseMessage responseMessage)
        {
            responseMessage = new ResponseMessage();
            if (obj == null || string.IsNullOrWhiteSpace(obj.Action))
            {
                responseMessage.Message = "Invalid request.";
                return false;
            }
            return true;
        }
        public void Dispose() { }
    }
}
