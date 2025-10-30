using System;

namespace emec.entities.Customer
{
    public class CustomerDataResponse
    {
        public Guid Id { get; set; }
        public string FName { get; set; }
        public string LName { get; set; }
        public string Address { get; set; }
        public string NIC { get; set; }
        public string Phone1 { get; set; }
        public string Phone2 { get; set; }
        public int Type { get; set; }



        //public DateTime CreatedAt { get; set; }
        //public int CreatedBy { get; set; }
        //public bool Deleted { get; set; }
        //public DateTime? DeletedAt { get; set; }
        //public int? DeletedBy { get; set; }
    }
}
