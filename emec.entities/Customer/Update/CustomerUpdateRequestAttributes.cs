using System;

namespace emec.entities.Customer.Update
{
    public class CustomerUpdateRequestAttributes
    {
        public Guid Id { get; set; }
        public string? FName { get; set; }
        public string? LName { get; set; }
        public string? Address { get; set; }
        public string? NIC { get; set; }
        public string? Phone1 { get; set; }
        public string? Phone2 { get; set; }
        public int Type { get; set; }
    }
}
