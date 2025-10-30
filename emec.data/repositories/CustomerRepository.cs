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
        public async Task<IEnumerable<CustomerDataResponse>> GetCustomerIdAndNameAsync(string? name)
        {
            try
            {
                return await (from customer in _context.TblWsCustomers
                              where customer.Deleted == false
                              orderby customer.Fname, customer.Lname
                              where string.IsNullOrWhiteSpace(name) || (customer.Fname + " " + customer.Lname).Contains(name)
                              select new CustomerDataResponse
                              {
                                  Id = customer.Id,
                                  FName = customer.Fname,
                                  LName = customer.Lname
                              }).ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                throw;
            }

        }

        public async Task<IEnumerable<CustomerDataResponse>> GetCustomersAsync()
        {
            try
            {
                return await (from customer in _context.TblWsCustomers
                              where customer.Deleted == false
                              orderby customer.Fname, customer.Lname
                              select new CustomerDataResponse
                              {
                                  Id = customer.Id,
                                  FName = customer.Fname,
                                  LName = customer.Lname,
                                  Address = customer.Address,
                                  NIC = customer.Nic,
                                  Phone1 = customer.Phone1,
                                  Phone2 = customer.Phone2,
                                  Type = customer.Type

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
