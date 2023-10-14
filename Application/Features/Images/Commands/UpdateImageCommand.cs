using Application.Models.Images;
using Application.PipelineBehaviours.Contracts;
using Application.Repositories;
using AutoMapper;
using MediatR;

namespace Application.Features.Images.Commands
{
    public class UpdateImageCommand : IRequest<bool>, ICacheRemoval
    {
        public UpdateImage UpdateImage { get; set; }
        public List<string?> CacheKeys { get; set; }

        public UpdateImageCommand(UpdateImage updateImage)
        {
            UpdateImage = updateImage;
            CacheKeys = new() { $"GetImageById:{UpdateImage.Id}", "GetAllImages" }; // For deleting from cache.
        }
    }

    public class UpdateImageCommandHandler : IRequestHandler<UpdateImageCommand, bool>
    {
        private readonly IMapper _mapper;
        private readonly IImageRepo _imageRepo;

        public UpdateImageCommandHandler(IImageRepo imageRepo, IMapper mapper)
        {
            _imageRepo = imageRepo;
            _mapper = mapper;
        }

        public async Task<bool> Handle(UpdateImageCommand request, CancellationToken cancellationToken)
        {
            var image = await _imageRepo.GetByIdAsync(request.UpdateImage.Id);

            if (image == null)
                return false;

            _mapper.Map(request.UpdateImage, image);


            await _imageRepo.UpdateAsync(image);
            return true;
        }
    }
}
