using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace emec.entities.Vehicle.Register
{
    public class VehicleRegisterDataAttributes
    {
        public string Id { get; set; }
        public string Province { get; set; }
        public string Prefix { get; set; }
        public int Number { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string Version { get; set; }
        public int YoM { get; set; }
        public int YoR { get; set; }
    }
}
