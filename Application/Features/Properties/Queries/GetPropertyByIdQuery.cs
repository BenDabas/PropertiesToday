using Application.Models.Properties;
using Application.PipelineBehaviours.Contracts;
using Application.Repositories;
using AutoMapper;
using Domain;
using MediatR;

namespace Application.Features.Properties.Queries
{
    public class GetPropertyByIdQuery : IRequest<PropertyDto?>, ICacheable
    {
        public int PropertyId {  get; set; }
        public string? CacheKey { get; set; }
        public bool BypassCache { get; set; }
        public TimeSpan? SlidingExpiration { get; set; }

        public GetPropertyByIdQuery(int propertyId)
        {
            PropertyId = propertyId;
            CacheKey = $"GetPropertyById:{propertyId}";
        }
    }

    public class GetPropertyByIdRequestHandler : IRequestHandler<GetPropertyByIdQuery, PropertyDto?>
    {
        private readonly IPropertyRepo _propertyRepo;
        private readonly IMapper _mapper;

        public GetPropertyByIdRequestHandler(IPropertyRepo propertyRepo, IMapper mapper)
        {
            _propertyRepo = propertyRepo;
            _mapper = mapper;
        }

        public async Task<PropertyDto?> Handle(GetPropertyByIdQuery request, CancellationToken cancellationToken)
        {
            var property = await _propertyRepo.GetByIdAsync(request.PropertyId);

            if (property == null)
                return null;

            var propertyDto = _mapper.Map<PropertyDto>(property);
            return propertyDto;
        }
    }


}
