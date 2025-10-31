using System;
using AutoMapper;
using emec.entities.Vehicle.Register;
using emec.entities.Vehicle.List;
using emec.dbcontext.tables.Models;
using emec.shared.Mappers;
using Microsoft.Extensions.Logging.Abstractions;

namespace emec.data.mappers
{
    public class EntityMapper : IEntityMapper
    {
        private readonly IMapper _mapper;

        public EntityMapper()
        {
            var configExpression = new MapperConfigurationExpression();
            configExpression.CreateMap<TblWsVehicle, VehicleRegistrationResponse>()
                .ForMember(dest => dest.OwnerId, opt => opt.MapFrom(src => src.CustomerId.ToString()));

            configExpression.CreateMap<TblWsVehicle, VehicleListResponse>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.ToString()))
                .ForMember(dest => dest.OwnerId, opt => opt.MapFrom(src => src.CustomerId.ToString()));
            // Add other mappings as needed
            var config = new MapperConfiguration(configExpression, NullLoggerFactory.Instance);
            _mapper = config.CreateMapper();
        }

        public TDestination Map<TSource, TDestination>(TSource source)
        {
            return _mapper.Map<TSource, TDestination>(source);
        }
    }
}
