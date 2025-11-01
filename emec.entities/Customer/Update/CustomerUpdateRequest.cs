using System;

namespace emec.entities.Customer.Update
{
    public class CustomerUpdateRequest
    {
        public required string Action { get; set; }
        public required string[] Args { get; set; }
        public required CustomerUpdateRequestAttributes Attributes { get; set; }
    }
}
