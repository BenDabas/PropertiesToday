using Application.Features.Properties.Commands;
using Application.Features.Properties.Queries;
using Application.Models.Properties;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PropertiesController : ControllerBase
    {
        private readonly ISender _mediatrSender;

        public PropertiesController(ISender mediatrSender)
        {
            _mediatrSender = mediatrSender;
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddNewProperty([FromBody] NewProperty newPropertyRequest)
        {
            bool isSuccessful = await _mediatrSender.Send(new CreatePropertyCommand(newPropertyRequest));

            if (isSuccessful)
                return Ok("Property created succesfuly.");
            return BadRequest("Failed to create property.");
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateProperty([FromBody] UpdateProperty updateProperty)
        {
            bool isSuccessful = await _mediatrSender.Send(new UpdatePropertyCommand(updateProperty));

            if (isSuccessful)
                return Ok("Property updated succesfully.");
            return NotFound("Property does not exists.");
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PropertyDto?>> GetPropertyById(int id)
        {
            var propertyDto = await _mediatrSender.Send(new GetPropertyByIdQuery(id));

            if (propertyDto == null)
                return NotFound("Property does not exists.");

            return Ok(propertyDto);
        }

        [HttpGet("all")]
        public async Task<ActionResult<List<PropertyDto>>> GetAllProperties()
        {
            var properties = await _mediatrSender.Send(new GetAllPropertiesQuery());

            if (properties.Any())
                return Ok(properties);
            return NotFound("Properties does not exists.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProperty(int id)
        {
            var isPropertyDeleted = await _mediatrSender.Send(new DeletePropertyCommand(id));

            if (isPropertyDeleted)
                return Ok("Property deleted succesfully.");
            return NotFound("Properties does not exists.");

        }
    }
}
