using emec.shared.common;

namespace emec.entities.Customer
{
    public class CustomerDataRequest
    {
        public string Action { get; set; } = Constants.ApiActions.List;
        public string[] Args { get; set; }
        public required CustomerDataRequestAttributes Attributes { get; set; }
    }
}
