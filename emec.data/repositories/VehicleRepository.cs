using emec.contracts.repositories;
using emec.dbcontext.tables.Models;
using emec.entities.Vehicle.List;
using emec.entities.Vehicle.Register;
using emec.shared.Mappers;
using Microsoft.EntityFrameworkCore;
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
            try
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
                    YoM = vehicleReg.YoM,
                    YoR = vehicleReg.YoR,
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

                if (result > 0)
                {
                    return _entityMapper.Map<TblWsVehicle, VehicleRegistrationResponse>(entity);
                }
                else
                {
                    return _entityMapper.Map<TblWsVehicle, VehicleRegistrationResponse>(new TblWsVehicle());
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                throw;
            }

        }

        public async Task<List<VehicleListResponse>> GetAllVehiclesAsync()
        {
            try
            {
                var vehicles = await _context.TblWsVehicles
                .Where(v => v.Deleted == false)
                .ToListAsync();

                var vehicleList = new List<VehicleListResponse>();
                foreach (var vehicle in vehicles)
                {
                    vehicleList.Add(_entityMapper.Map<TblWsVehicle, VehicleListResponse>(vehicle));
                }

                return vehicleList;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                throw;
            }

        }
    }
}
