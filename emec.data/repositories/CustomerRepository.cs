using emec.contracts.repositories;
using emec.dbcontext.tables.Models;
using emec.entities.Customer;
using Microsoft.EntityFrameworkCore;

namespace emec.data.repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly eMecContext _context;
        public CustomerRepository(eMecContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<SearchCustomerDataResponse>> GetCustomersAsync(string? name)
        {
            return await (from customer in _context.TblWsCustomers
                                      where customer.Deleted == false
                                      orderby customer.Fname, customer.Lname
                                      where string.IsNullOrWhiteSpace(name) || (customer.Fname + " " + customer.Lname).Contains(name)
                                      select new SearchCustomerDataResponse
                                      {
                                          Id = customer.Id,
                                          Name = customer.Fname + " " + customer.Lname
                                      }).ToListAsync();

        }
    }
}
