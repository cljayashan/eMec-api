using System;

namespace emec.entities.Customer.Delete
{
    public class CustomerDeleteRequest
    {
        public required string Action { get; set; }
        public required string[] Args { get; set; }
        public required CustomerDeleteRequestAttributes Attributes { get; set; }
    }
}
