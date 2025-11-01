using System;

namespace emec.entities.Customer.View
{
    public class CustomerViewRequest
    {
        public required string Action { get; set; }
        public required string[] Args { get; set; }
        public required CustomerViewRequestAttributes Attributes { get; set; }
    }
}
