using Application.Models.Images;
using Application.PipelineBehaviours.Contracts;
using Application.Repositories;
using AutoMapper;
using MediatR;

namespace Application.Features.Images.Queries
{
    public class GetAllImagesQuery : IRequest<List<ImageDto>>, ICacheable, ILogable
    {
        public string? CacheKey { get; set; }
        public bool BypassCache { get; set; }
        public TimeSpan? SlidingExpiration { get; set; }

        public GetAllImagesQuery()
        {
            CacheKey = "GetAllImages";
        }
    }

    public class GetAllImagesQueryHandler : IRequestHandler<GetAllImagesQuery, List<ImageDto>>
    {
        public readonly IMapper _mapper;
        public readonly IImageRepo _imageRepo;

        public GetAllImagesQueryHandler(IMapper mapper, IImageRepo imageRepo)
        {
            _mapper = mapper;
            _imageRepo = imageRepo;
        }

        public async Task<List<ImageDto>> Handle(GetAllImagesQuery request, CancellationToken cancellationToken)
        {
            var allImages = await _imageRepo.GetAllAsync();

            return _mapper.Map<List<ImageDto>>(allImages);
        }
    }
}
