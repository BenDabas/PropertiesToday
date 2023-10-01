using Application.Models.Properties;
using Application.PipelineBehaviours.Contracts;
using Application.Repositories;
using AutoMapper;
using Domain;
using MediatR;

namespace Application.Features.Properties.Commands
{
    public class CreatePropertyCommand : IRequest<bool>, IValidatable
    {
        public NewProperty PropertyRequest { get; set; }

        public CreatePropertyCommand(NewProperty newPropertyRequest)
        {
            PropertyRequest = newPropertyRequest;
        }
    }

    public class CreatePropertyRequestHandler : IRequestHandler<CreatePropertyCommand, bool>
    {
        private readonly IPropertyRepo _repositoryRepo;
        private readonly IMapper _mapper;

        public CreatePropertyRequestHandler(IPropertyRepo repositoryRepo, IMapper mapper)
        {
            _repositoryRepo = repositoryRepo;
            _mapper = mapper;
        }

        public async Task<bool> Handle(CreatePropertyCommand request, CancellationToken cancellationToken)
        {
            Property property = _mapper.Map<Property>(request.PropertyRequest);

            property.ListDate = DateTime.Now;

            await _repositoryRepo.AddNewAsync(property);

            return true;
        }
    }
}
