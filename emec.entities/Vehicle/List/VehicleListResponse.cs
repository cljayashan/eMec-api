using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace emec.entities.Vehicle.List
{
    public class VehicleListResponse
    {
        public required string Id { get; set; }
        public required string Province { get; set; }
        public required string Prefix { get; set; }
        public required string Number { get; set; }
        public required string Brand { get; set; }
        public required string Model { get; set; }
        public string? Version { get; set; }
        public int? YoM { get; set; }
        public int? YoR { get; set; }
        public string? Remarks { get; set; }
        public required string OwnerId { get; set; }
        //public DateTime CreatedAt { get; set; }
        //public int CreatedBy { get; set; }
    }
}
