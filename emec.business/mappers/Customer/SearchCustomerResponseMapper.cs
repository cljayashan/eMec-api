using System.Collections.Generic;
using emec.entities.Customer;

namespace emec.business.mappers.Customer
{
    public class SearchCustomerResponseMapper : emec.shared.Mappers.IMapper<object, emec.shared.models.ResponseBase>
    {
        public emec.shared.models.ResponseBase Map(object input)
        {
            var customers = input as List<SearchCustomerDataResponse>;
            return new emec.shared.models.ResponseBase
            {
                IsSuccess = true,
                Result = customers,
                Error = null
            };
        }
    }
}
