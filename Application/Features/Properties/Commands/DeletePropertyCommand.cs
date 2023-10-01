using Application.Repositories;
using MediatR;

namespace Application.Features.Properties.Commands
{
    public class DeletePropertyCommand : IRequest<bool>
    {
        public int PropertyId {  get; set; }

        public DeletePropertyCommand(int propertyId)
        {
            PropertyId = propertyId;
        }
    }

    public class DeletePropertyRequestHandler : IRequestHandler<DeletePropertyCommand, bool>
    {
        private readonly IPropertyRepo _propertyRepo;

        public DeletePropertyRequestHandler(IPropertyRepo propertyRepo)
        {
            _propertyRepo = propertyRepo;
        }

        public async Task<bool> Handle(DeletePropertyCommand request, CancellationToken cancellationToken)
        {
            var propertyToDelete = await _propertyRepo.GetByIdAsync(request.PropertyId);

            if (propertyToDelete == null)
                return false;

            await _propertyRepo.DeleteAsync(propertyToDelete);

            return true;
        }
    }
}
