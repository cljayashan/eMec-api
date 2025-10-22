using emec.entities.Login;
using emec.entities.Vehicle.Register;
using emec.shared.Contracts;
using emec.shared.Errors;
using emec.shared.models;

namespace emec.business.validators.Vehicle
{
    public class VehicleRegisterRequestValidator : IValidator<VehicleRegisterDataRequest>
    {
        //private string GetError(string key) => Errors.ResourceManager.GetString(key);
        private readonly IErrorMessages _errorMessages;

        public bool Validate(VehicleRegisterDataRequest request, out ResponseMessage message)
        {
            message = null;

            if (request == null || request.Attributes == null)
            {
                message = _errorMessages.Vehicle_Null();
            ;
                return false;
            }

            var attr = request.Attributes;
            if (string.IsNullOrWhiteSpace(attr.Id))
            {
                message = _errorMessages.Vehicle_IdRequired();
                return false;
            }


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
                message = _errorMessages.Vehicle_IdRequired();
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


            if (string.IsNullOrWhiteSpace(attr.Version))
            {
                message = _errorMessages.Vehicle_VersionRequired();
                return false;
            }


            if (attr.YoM == 0)
            {
                message = _errorMessages.Vehicle_YoMRequired();
                return false;
            }


            if (attr.YoR == 0)
            {
                message = _errorMessages.Vehicle_YoRRequired();
                return false;
            }


            return true;
        }

        public void Dispose() { /* No resources to dispose */ }
    }
}
