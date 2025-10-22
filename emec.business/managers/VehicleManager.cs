using System;
using System.Threading.Tasks;
using emec.contracts.managers;
using emec.contracts.repositories;
using emec.entities.HealthCheck;
using emec.entities.Vehicle.Register;
using emec.shared.Contracts;
using emec.shared.Mappers;
using emec.shared.models;

namespace emec.business.managers
{
    public class VehicleManager : IVehicleManager
    {
        private readonly IVehicleRepository _vehicleRepository;
        private readonly IMapper<object, ResponseBase> _serviceResponseMapper;
        private readonly IMapper<ResponseMessage, ResponseBase> _serviceResponseErrorMapper;
        private readonly IValidator<VehicleRegisterDataRequest> _vehicleRegisterDataRequestValidator;

        public VehicleManager(
            IVehicleRepository vehicleRepository,
            IMapper<object, ResponseBase> serviceResponseMapper,
            IMapper<ResponseMessage, ResponseBase> serviceResponseErrorMapper,
            IValidator<VehicleRegisterDataRequest> vehicleRegisterDataRequestValidator)
        {
            _vehicleRepository = vehicleRepository ?? throw new ArgumentNullException(nameof(vehicleRepository));
            _serviceResponseMapper = serviceResponseMapper ?? throw new ArgumentNullException(nameof(serviceResponseMapper));
            _serviceResponseErrorMapper = serviceResponseErrorMapper ?? throw new ArgumentNullException(nameof(serviceResponseErrorMapper));
            _vehicleRegisterDataRequestValidator = vehicleRegisterDataRequestValidator ?? throw new ArgumentNullException(nameof(vehicleRegisterDataRequestValidator));
        }

        public async Task<ResponseBase> RegisterVehicle(VehicleRegisterDataRequest request)
        {
            if (!_vehicleRegisterDataRequestValidator.Validate(request, out ResponseMessage message))
            {
                return _serviceResponseErrorMapper.Map(message);
            }
            else
            {
                var success = await _vehicleRepository.RegisterVehicleAsync(request.Attributes);
                if (success)
                {
                    return _serviceResponseMapper.Map(new { Message = "Vehicle registration successful" });
                }
                else
                {
                    var error = new ResponseMessage { Message = "Vehicle registration failed" };
                    return _serviceResponseErrorMapper.Map(error);
                }
            }               
        }
    }
}
