using Application.Models.Images;
using Application.Repositories;
using AutoMapper;
using MediatR;

namespace Application.Features.Images.Queries
{
    public class GetImageByIdQuery : IRequest<ImageDto?>
    {
        public int ImageId { get; set; }

        public GetImageByIdQuery(int imageId)
        {
            ImageId = imageId;
        }
    }

    public class GetImageByIdQueryHandler : IRequestHandler<GetImageByIdQuery, ImageDto?>
    {
        public readonly IMapper _mapper;
        public readonly IImageRepo _imageRepo;

        public GetImageByIdQueryHandler(IImageRepo imageRepo, IMapper mapper)
        {
            _imageRepo = imageRepo;
            _mapper = mapper;
        }

        public async Task<ImageDto?> Handle(GetImageByIdQuery request, CancellationToken cancellationToken)
        {
            return _mapper.Map<ImageDto>(await _imageRepo.GetByIdAsync(request.ImageId));
        }
    }
}
