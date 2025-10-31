using System.Threading.Tasks;
using emec.entities.Customer;
using emec.entities.Customer.View;
using emec.shared.models;

namespace emec.contracts.managers
{
    public interface ICustomerManager
    {
        Task<ResponseBase> GetCustomersAsync(CustomerDataRequest request);

        Task<ResponseBase> GetCustomerIdAndNameAsync(CustomerDataRequest request);

        Task<ResponseBase> GetCustomerByIdAsync(CustomerViewRequest request);
    }
}
