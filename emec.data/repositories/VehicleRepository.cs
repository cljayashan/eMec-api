using System.Threading.Tasks;
using emec.contracts.repositories;
using emec.entities.Vehicle.Register;
using emec.dbcontext.tables.Models;

namespace emec.data.repositories
{
    public class VehicleRepository : IVehicleRepository
    {
        private readonly eMecContext _context;

        public VehicleRepository(eMecContext context)
        {
            _context = context;
        }

        public async Task<bool> RegisterVehicleAsync(VehicleRegisterDataAttributes attributes)
        {
            var entity = new TblWsVehicle
            {
                Id = Guid.TryParse(attributes.Id, out var guid) ? guid : Guid.NewGuid(),
                Province = attributes.Province,
                Prefix = attributes.Prefix,
                Number = attributes.Number.ToString(),
                Brand = attributes.Brand,
                Model = attributes.Model,
                Version = attributes.Version,
                YoM = attributes.YoM.ToString(),
                YoR = attributes.YoR.ToString()
            };

            _context.TblWsVehicles.Add(entity);
            var result = await _context.SaveChangesAsync();
            return result > 0;
        }
    }
}
