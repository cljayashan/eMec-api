using emec.contracts.repositories;
using emec.dbcontext.tables.Models;
using emec.entities.Customer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace emec.data.repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly eMecContext _context;
        private readonly ILogger<CustomerRepository> _logger;
        public CustomerRepository(eMecContext context, ILogger<CustomerRepository> logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task<IEnumerable<SearchCustomerDataResponse>> GetCustomersAsync(string? name)
        {
            try
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
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                throw;
            }          

        }
    }
}
