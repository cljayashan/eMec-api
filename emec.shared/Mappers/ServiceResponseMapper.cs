using emec.shared.models;

namespace emec.shared.Mappers
{
    public class ServiceResponseMapper : IMapper<object, ResponseBase>
    {
        public ResponseBase Map(object input)
        {
            return new ResponseBase
            {
                Result = input,
                IsSuccess = true,
                Error = null,
            };
        }
    }
}
