using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using emec.entities.Vehicle.Register;
using emec.shared.Mappers;

namespace emec.business.mappers
{
    public class VehicleRegistrationDataSaveRequestMapper : IMapper<VehicleRegisterDataRequest, VehicleRegistrationDataSave>
    {
        public VehicleRegistrationDataSave Map(VehicleRegisterDataRequest input)
        {
            var vehicleRegistrationDataSave = new VehicleRegistrationDataSave
            {
                Province = input.Attributes.Province,
                Prefix = input.Attributes.Prefix,
                Number = input.Attributes.Number,
                Brand = input.Attributes.Brand,
                Model = input.Attributes.Model,
                Version = input.Attributes.Version,
                YoM = input.Attributes.YoM ?? null,
                YoR = input.Attributes.YoR ?? null,
                Remarks = input.Attributes.Remarks,
                OwnerId = input.Attributes.OwnerId,
                CreatedAt = input.Attributes.CreatedAt,
                CreatedBy = input.Attributes.CreatedBy,
                Deleted = input.Attributes.Deleted,
                DeletedAt = input.Attributes.DeletedAt,
                DeletedBy = input.Attributes.DeletedBy
            };

            return vehicleRegistrationDataSave;
        }
    }
}
