using System.Threading.Tasks;
using emec.entities.Vehicle.Register;
using emec.shared.models;

namespace emec.contracts.managers
{
    public interface IVehicleManager
    {
        Task<ResponseBase> RegisterVehicle(VehicleRegisterDataRequest request);
    }
}
