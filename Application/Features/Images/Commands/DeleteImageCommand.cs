using Application.Repositories;
using MediatR;

namespace Application.Features.Images.Commands
{
    public class DeleteImageCommand : IRequest<bool>
    {
        public int ImageId { get; set; }

        public DeleteImageCommand(int imageId)
        {
            ImageId = imageId;
        }
    }

    public class DeleteImageCommandHandler : IRequestHandler<DeleteImageCommand, bool>
    {
        public readonly IImageRepo _imageRepo;

        public DeleteImageCommandHandler(IImageRepo imageRepo)
        {
            _imageRepo = imageRepo;
        }

        public async Task<bool> Handle(DeleteImageCommand request, CancellationToken cancellationToken)
        {
            var deleteImage = await _imageRepo.GetByIdAsync(request.ImageId);

            if (deleteImage == null)
                return false;
            
            await _imageRepo.DeleteAsync(deleteImage);
            return true;
        }
    }
}
