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

        public async Task<CustomerDataResponse?> GetCustomerByIdAsync(Guid customerId)
        {
            try
            {
                return await (from customer in _context.TblWsCustomers
                              where customer.Id == customerId && customer.Deleted == false
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
                              }).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                throw;
            }
        }

        public async Task<bool> DeleteCustomerAsync(Guid customerId, int deletedBy)
        {
            try
            {
                var customer = await _context.TblWsCustomers
                    .FirstOrDefaultAsync(c => c.Id == customerId && c.Deleted == false);

                if (customer == null)
                {
                    return false;
                }

                customer.Deleted = true;
                customer.DeletedAt = DateTime.Now;
                customer.DeletedBy = deletedBy;

                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                throw;
            }
        }

        public async Task<CustomerDataResponse?> UpdateCustomerAsync(Guid customerId, string? fName, string? lName, string? address, string? nic, string? phone1, string? phone2, int type, int updatedBy)
        {
            try
            {
                var customer = await _context.TblWsCustomers
                    .FirstOrDefaultAsync(c => c.Id == customerId && c.Deleted == false);

                if (customer == null)
                {
                    return null;
                }

                // Update fields
                customer.Fname = fName ?? customer.Fname;
                customer.Lname = lName ?? customer.Lname;
                customer.Address = address ?? customer.Address;
                customer.Nic = nic ?? customer.Nic;
                customer.Phone1 = phone1 ?? customer.Phone1;
                customer.Phone2 = phone2 ?? customer.Phone2;
                customer.Type = type;

                await _context.SaveChangesAsync();

                // Return updated customer
                return new CustomerDataResponse
                {
                    Id = customer.Id,
                    FName = customer.Fname,
                    LName = customer.Lname,
                    Address = customer.Address,
                    NIC = customer.Nic,
                    Phone1 = customer.Phone1,
                    Phone2 = customer.Phone2,
                    Type = customer.Type
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                throw;
            }
        }
    }
}
