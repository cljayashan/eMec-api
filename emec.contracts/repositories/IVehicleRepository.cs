using System.Threading.Tasks;
using emec.entities.Vehicle.Register;
using emec.shared.models;

namespace emec.contracts.repositories
{
    public interface IVehicleRepository
    {
        Task<VehicleRegistrationResponse> RegisterVehicleAsync(VehicleRegistrationDataSave vehicleReg);
    }
}
