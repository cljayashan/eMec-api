using System;
using System.Threading.Tasks;
using emec.contracts.managers;
using emec.contracts.repositories;
using emec.entities.Customer;
using emec.shared.Contracts;
using emec.shared.Mappers;
using emec.shared.models;
using Microsoft.Extensions.Logging;

namespace emec.business.managers
{
    public class CustomerManager : ICustomerManager
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper<object, ResponseBase> _serviceResponseMapper;
        private readonly IMapper<ResponseMessage, ResponseBase> _serviceResponseErrorMapper;
        private readonly IValidator<CustomerDataRequest> _customerDataRequestValidator;
        private readonly ILogger<CustomerManager> _logger;

        public CustomerManager(
            ICustomerRepository customerRepository,
            IMapper<object, ResponseBase> serviceResponseMapper,
            IMapper<ResponseMessage, ResponseBase> serviceResponseErrorMapper,
            IValidator<CustomerDataRequest> customerDataRequestValidator,
             ILogger<CustomerManager> logger)
        {
            _customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
            _serviceResponseMapper = serviceResponseMapper ?? throw new ArgumentNullException(nameof(serviceResponseMapper));
            _serviceResponseErrorMapper = serviceResponseErrorMapper ?? throw new ArgumentNullException(nameof(serviceResponseErrorMapper));
            _customerDataRequestValidator = customerDataRequestValidator ?? throw new ArgumentNullException(nameof(customerDataRequestValidator));
            _logger = logger; ;
        }

        public async Task<ResponseBase> GetCustomersAsync(CustomerDataRequest request)
        {
            try
            {
                if (!_customerDataRequestValidator.Validate(request, out ResponseMessage message))
                    return _serviceResponseErrorMapper.Map(message);

                var customers = await _customerRepository.GetCustomersAsync();
                return _serviceResponseMapper.Map(customers);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                throw;
            }
           
        }

        public async Task<ResponseBase> GetCustomerIdAndNameAsync(CustomerDataRequest request)
        {
            try
            {
                if (!_customerDataRequestValidator.Validate(request, out ResponseMessage message))
                    return _serviceResponseErrorMapper.Map(message);

                var customers = await _customerRepository.GetCustomerIdAndNameAsync(request.Attributes?.FName);
                return _serviceResponseMapper.Map(customers);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                throw;
            }

        }
    }
}
