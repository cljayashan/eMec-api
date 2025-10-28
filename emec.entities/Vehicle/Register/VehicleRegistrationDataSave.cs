namespace emec.entities.Vehicle.Register
{
    public class VehicleRegistrationDataSave
    {
        public Guid Id { get; set; }
        public string Province { get; set; }
        public string Prefix { get; set; }
        public int Number { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string Version { get; set; }
        public int? YoM { get; set; }
        public int? YoR { get; set; }
        public string Remarks { get; set; }
        public Guid OwnerId { get; set; }

        public DateTime CreatedAt { get; set; }
        public int CreatedBy { get; set; }
        public bool Deleted { get; set; }
        public DateTime? DeletedAt { get; set; }
        public int? DeletedBy { get; set; }
    }
}
