using System.Threading.Tasks;
using emec.contracts.repositories;
using emec.dbcontext.tables.Models;
using emec.entities.Vehicle.Register;
using emec.shared.Mappers;
using Microsoft.Extensions.Logging;

namespace emec.data.repositories
{
    public class VehicleRepository : IVehicleRepository
    {
        private readonly eMecContext _context;
        private readonly ILogger<VehicleRepository> _logger;
        private readonly IEntityMapper _entityMapper;

        public VehicleRepository(eMecContext context, IEntityMapper entityMapper, ILogger<VehicleRepository> logger)
        {
            _context = context;
            _entityMapper = entityMapper;
            _logger = logger;
        }

        public async Task<VehicleRegistrationResponse> RegisterVehicleAsync(VehicleRegistrationDataSave vehicleReg)
        {
            var entity = new TblWsVehicle
            {
                Id = Guid.NewGuid(),
                Province = vehicleReg.Province,
                Prefix = vehicleReg.Prefix,
                Number = vehicleReg.Number.ToString(),
                Brand = vehicleReg.Brand,
                Model = vehicleReg.Model,
                Version = vehicleReg.Version,
                YoM = vehicleReg.YoM.ToString(),
                YoR = vehicleReg.YoR.ToString(),
                Remarks = vehicleReg.Remarks,
                CustomerId = vehicleReg.OwnerId,
                CreatedAt = vehicleReg.CreatedAt,
                CreatedBy = vehicleReg.CreatedBy,
                Deleted = false,
                DeletedAt = null,
                DeletedBy = null
            };

            _context.TblWsVehicles.Add(entity);
            var result = await _context.SaveChangesAsync();

            TblWsVehicle insertedVehicle = null;
            if (result > 0)
            {
                insertedVehicle = entity;
            }
            return _entityMapper.Map<TblWsVehicle, VehicleRegistrationResponse>(insertedVehicle);
        }
    }
}
