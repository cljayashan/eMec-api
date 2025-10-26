using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace emec.entities.Vehicle.Register
{
    public class VehicleRegistrationResponse
    {

        public required string Id { get; set; }
        public required string Province { get; set; }
        public required string Prefix { get; set; }
        public required int Number { get; set; }
        public required string Brand { get; set; }
        public required string Model { get; set; }
        public string Version { get; set; }
        public int YoM { get; set; }
        public int YoR { get; set; }
        public string Remarks { get; set; }
        public required string OwnerId { get; set; }



        public DateTime CreatedAt { get; set; }
        public int CreatedBy { get; set; }
        public bool Deleted { get; set; }
        public DateTime DeletedAt { get; set; }
        public int DeletedBy { get; set; }
    }
}
