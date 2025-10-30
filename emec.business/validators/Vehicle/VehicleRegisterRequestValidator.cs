using emec.entities.Login;
using emec.entities.Vehicle.Register;
using emec.shared.Contracts;
using emec.shared.Errors;
using emec.shared.models;
using static emec.shared.common.Constants;

namespace emec.business.validators.Vehicle
{
    public class VehicleRegisterRequestValidator : IValidator<VehicleRegisterDataRequest>
    {
        //private string GetError(string key) => Errors.ResourceManager.GetString(key);
        private readonly IErrorMessages _errorMessages;

        public VehicleRegisterRequestValidator(IErrorMessages errorMessages)
        {
            _errorMessages = errorMessages;
        }

        public bool Validate(VehicleRegisterDataRequest request, out ResponseMessage message)
        {
            message = null;


            if (request.Action == ApiActions.Add)
            {
               
                var attr = request.Attributes;



                if (string.IsNullOrWhiteSpace(attr.Province))
                {
                    message = _errorMessages.Vehicle_ProvinceRequired();
                    return false;
                }


                if (string.IsNullOrWhiteSpace(attr.Prefix))
                {
                    message = _errorMessages.Vehicle_PrefixRequired();
                    return false;
                }


                if (attr.Number == 0)
                {
                    message = _errorMessages.Vehicle_NumberRequired();
                    return false;
                }

                if (attr.Number.ToString().Length != 4)
                {
                    message = _errorMessages.Vehicle_NumberMustHave4Digits();
                    return false;
                }


                if (string.IsNullOrWhiteSpace(attr.Brand))
                {
                    message = _errorMessages.Vehicle_BrandRequired();
                    return false;
                }


                if (string.IsNullOrWhiteSpace(attr.Model))
                {
                    message = _errorMessages.Vehicle_ModelRequired();
                    return false;
                } 


                return true;
            }
            return message == null;
        }

        public void Dispose() { /* No resources to dispose */ }
    }
}
