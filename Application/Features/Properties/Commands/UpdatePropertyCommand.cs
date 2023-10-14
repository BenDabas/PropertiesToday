using Application.Models.Properties;
using Application.PipelineBehaviours.Contracts;
using Application.Repositories;
using AutoMapper;
using Domain;
using MediatR;

namespace Application.Features.Properties.Commands
{
    public class UpdatePropertyCommand : IRequest<bool>, ICacheRemoval
    {
        public UpdateProperty UpdateProperty { get; set; }
        public List<string?> CacheKeys { get; set; }

        public UpdatePropertyCommand(UpdateProperty updateProperty)
        {
            UpdateProperty = updateProperty;
            CacheKeys = new() { $"GetPropertyById:{UpdateProperty.Id}" , "GetProperties" }; // For deleting from cache.
        }
    }

    public class UpdatePropertyRequestHandler : IRequestHandler<UpdatePropertyCommand, bool>
    {
        private readonly IPropertyRepo _propertyRepo;
        private readonly IMapper _mapper;

        public UpdatePropertyRequestHandler(IMapper mapper, IPropertyRepo propertyRepo)
        {
            _mapper = mapper;
            _propertyRepo = propertyRepo;
        }

        public async Task<bool> Handle(UpdatePropertyCommand request, CancellationToken cancellationToken)
        {
            Property? propertyInDb = await _propertyRepo.GetByIdAsync(request.UpdateProperty.Id);

            if (propertyInDb == null) 
            {
                return false;
            }

            _mapper.Map(request.UpdateProperty, propertyInDb);
            
            await _propertyRepo.UpdateAsync(propertyInDb);
            
            return true;
        }

    }
}
