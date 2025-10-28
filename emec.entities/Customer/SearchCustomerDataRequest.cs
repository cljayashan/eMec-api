using emec.shared.common;

namespace emec.entities.Customer
{
    public class SearchCustomerDataRequest
    {
        public string Action { get; set; } = Constants.ApiActions.List;
        public required SearchCustomerDataRequestAttributes Attributes { get; set; }
    }
}
