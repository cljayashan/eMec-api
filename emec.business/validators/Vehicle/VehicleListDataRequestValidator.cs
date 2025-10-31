using emec.entities.Vehicle.List;
using emec.shared.Contracts;
using emec.shared.models;
using static emec.shared.common.Constants;

namespace emec.business.validators.Vehicle
{
    public class VehicleListDataRequestValidator : IValidator<VehicleListDataRequest>
    {
        private readonly IErrorMessages _errorMessages;

        public VehicleListDataRequestValidator(IErrorMessages errorMessages)
        {
            _errorMessages = errorMessages;
        }

        public bool Validate(VehicleListDataRequest request, out ResponseMessage message)
        {
            message = null;

            if (request == null)
            {
                message = _errorMessages.Common_InvalidRequest();
                return false;
            }

            if (request.Action != ApiActions.List)
            {
                message = _errorMessages.Common_ApiActionNotPermitted();
                return false;
            }

            return true;
        }

        public void Dispose() { /* No resources to dispose */ }
    }
}
