using emec.contracts.managers;
using emec.entities.Customer;
using emec.entities.Customer.Delete;
using emec.entities.Customer.Update;
using emec.entities.Customer.View;
using emec.shared.common;
using emec.shared.Contracts;
using emec.shared.Mappers;
using emec.shared.models;
using Microsoft.AspNetCore.Mvc;

namespace emec.api.Controllers.common
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerManager _customerManager;
        private readonly IMapper<ResponseMessage, ResponseBase> _serviceResponseErrorMapper;
        private readonly IErrorMessages _errormessages;
        private readonly IMapper<object, ResponseBase> _serviceResponseMapper;
        private readonly ILogger<CustomerController> _logger;

        public CustomerController(
            ICustomerManager customerManager,
            IMapper<ResponseMessage, ResponseBase> serviceResponseErrorMapper,
            IErrorMessages errorMessages,
            IMapper<object, ResponseBase> serviceResponseMapper,
            ILogger<CustomerController> logger
        )
        {
            _customerManager = customerManager;
            _errormessages = errorMessages;
            _serviceResponseErrorMapper = serviceResponseErrorMapper;
            _serviceResponseMapper = serviceResponseMapper ?? throw new ArgumentNullException(nameof(serviceResponseMapper));
            _logger = logger;
        }

        [HttpPost("search")]
        public async Task<ActionResult<ResponseBase>> Customers([FromBody] CustomerDataRequest request)
        {
            try
            {
                if (request.Action.Equals(Constants.ApiActions.List))
                {
                    if(request.Args == null || request.Args.Length == 0)
                    {
                        var response = await _customerManager.GetCustomersAsync(request);
                        return response;
                    }
                    else
                    {
                        if (request.Args[0] == "dropdowndata" )
                        {
                            var response = await _customerManager.GetCustomerIdAndNameAsync(request);
                            return response;
                        }
                    }
                    
                }
                return _serviceResponseErrorMapper.Map(_errormessages.Common_ApiActionNotPermitted());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return _serviceResponseErrorMapper.Map(_errormessages.Common_InvalidRequest());
            }
        }

        [HttpPost("view")]
        public async Task<ActionResult<ResponseBase>> GetCustomerById([FromBody] CustomerViewRequest request)
        {
            try
            {
                if (request.Action.Equals(Constants.ApiActions.View))
                {
                    return await _customerManager.GetCustomerByIdAsync(request);
                }

                return _serviceResponseErrorMapper.Map(_errormessages.Common_ApiActionNotPermitted());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return _serviceResponseErrorMapper.Map(_errormessages.Common_InvalidRequest());
            }
        }

        [HttpPost("delete")]
        public async Task<ActionResult<ResponseBase>> DeleteCustomer([FromBody] CustomerDeleteRequest request)
        {
            try
            {
                if (request.Action.Equals(Constants.ApiActions.Delete))
                {
                    return await _customerManager.DeleteCustomerAsync(request);
                }

                return _serviceResponseErrorMapper.Map(_errormessages.Common_ApiActionNotPermitted());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return _serviceResponseErrorMapper.Map(_errormessages.Common_InvalidRequest());
            }
        }

        [HttpPost("update")]
        public async Task<ActionResult<ResponseBase>> UpdateCustomer([FromBody] CustomerUpdateRequest request)
        {
            try
            {
                if (request.Action.Equals(Constants.ApiActions.Edit))
                {
                    return await _customerManager.UpdateCustomerAsync(request);
                }

                return _serviceResponseErrorMapper.Map(_errormessages.Common_ApiActionNotPermitted());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return _serviceResponseErrorMapper.Map(_errormessages.Common_InvalidRequest());
            }
        }
    }
}
