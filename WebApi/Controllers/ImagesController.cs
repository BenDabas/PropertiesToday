using Application.Features.Images.Commands;
using Application.Features.Images.Queries;
using Application.Models.Images;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly ISender _mediatrSender;

        public ImagesController(ISender mediatrSender)
        {
            _mediatrSender = mediatrSender;
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddNewImage([FromBody] NewImage newImage)
        {
            var isImageCreated = await _mediatrSender.Send(new CreateImageCommand(newImage));

            if (isImageCreated)
                return Ok("Image created succesfuly.");

            return BadRequest("Faild to create image.");
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateImage([FromBody] UpdateImage updateImage)
        {
            var isImageUpdated = await _mediatrSender.Send(new UpdateImageCommand(updateImage));

            if (isImageUpdated)
                return Ok("Image updated succesfuly.");

            return BadRequest("Faild to update image.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteImage(int id)
        {
            var isImageDeleted = await _mediatrSender.Send(new DeleteImageCommand(id));

            if (isImageDeleted)
                return Ok("Image deleted succesfuly.");

            return BadRequest("Faild to delete image.");
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ImageDto>> GetImageById(int id)
        {
            var imageDto = await _mediatrSender.Send(new GetImageByIdQuery(id));

            if (imageDto == null)
                return NotFound("Image not found");

            return Ok(imageDto);
        }

        [HttpGet("all")]
        public async Task<ActionResult<ImageDto>> GetAllImages()
        {
            var AllimagesDto = await _mediatrSender.Send(new GetAllImagesQuery());

            return Ok(AllimagesDto);
        }
    }
}
