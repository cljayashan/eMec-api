using System.Collections.Generic;
using System.Threading.Tasks;
using emec.entities.Customer;

namespace emec.contracts.repositories
{
    public interface ICustomerRepository
    {
        Task<IEnumerable<CustomerDataResponse>> GetCustomerIdAndNameAsync(string? name);

        Task<IEnumerable<CustomerDataResponse>> GetCustomersAsync();

        Task<CustomerDataResponse?> GetCustomerByIdAsync(Guid customerId);

        Task<bool> DeleteCustomerAsync(Guid customerId, int deletedBy);
    }
}
