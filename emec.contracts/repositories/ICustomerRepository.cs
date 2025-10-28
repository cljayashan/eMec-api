using System.Collections.Generic;
using System.Threading.Tasks;
using emec.entities.Customer;

namespace emec.contracts.repositories
{
    public interface ICustomerRepository
    {
        Task<IEnumerable<SearchCustomerDataResponse>> GetCustomersAsync(string? name);
    }
}
