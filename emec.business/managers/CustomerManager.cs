using System;
using System.Threading.Tasks;
using emec.contracts.managers;
using emec.contracts.repositories;
using emec.entities.Customer;
using emec.shared.Contracts;
using emec.shared.Mappers;
using emec.shared.models;

namespace emec.business.managers
{
    public class CustomerManager : ICustomerManager
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper<object, ResponseBase> _serviceResponseMapper;
        private readonly IMapper<ResponseMessage, ResponseBase> _serviceResponseErrorMapper;
        private readonly IValidator<SearchCustomerDataRequest> _customerDataRequestValidator;

        public CustomerManager(
            ICustomerRepository customerRepository,
            IMapper<object, ResponseBase> serviceResponseMapper,
            IMapper<ResponseMessage, ResponseBase> serviceResponseErrorMapper,
            IValidator<SearchCustomerDataRequest> customerDataRequestValidator)
        {
            _customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
            _serviceResponseMapper = serviceResponseMapper ?? throw new ArgumentNullException(nameof(serviceResponseMapper));
            _serviceResponseErrorMapper = serviceResponseErrorMapper ?? throw new ArgumentNullException(nameof(serviceResponseErrorMapper));
            _customerDataRequestValidator = customerDataRequestValidator ?? throw new ArgumentNullException(nameof(customerDataRequestValidator));
        }

        public async Task<ResponseBase> GetCustomersAsync(SearchCustomerDataRequest request)
        {
            if (!_customerDataRequestValidator.Validate(request, out ResponseMessage message))
                return _serviceResponseErrorMapper.Map(message);

            var customers = await _customerRepository.GetCustomersAsync(request.Attributes?.Name);
            return _serviceResponseMapper.Map(customers);
        }
    }
}
