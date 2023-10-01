using Application.Models.Images;
using Application.Repositories;
using AutoMapper;
using Domain;
using MediatR;

namespace Application.Features.Images.Commands
{
    public class CreateImageCommand : IRequest<bool>
    {
        public NewImage NewImage { get; set; }

        public CreateImageCommand(NewImage newImage)
        {
            NewImage = newImage;
        }
    }

    public class CreateImageCommandHandler : IRequestHandler<CreateImageCommand, bool>
    {
        private readonly IImageRepo _imageRepo;
        private readonly IMapper _mapper;

        public CreateImageCommandHandler(IImageRepo imageRepo, IMapper mapper)
        {
            _imageRepo = imageRepo;
            _mapper = mapper;
        }

        public async Task<bool> Handle(CreateImageCommand request, CancellationToken cancellationToken)
        {
            Image image = _mapper.Map<Image>(request.NewImage);

            await _imageRepo.AddNewAsync(image);

            return true;
        }
    }
}
