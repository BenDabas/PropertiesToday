
using Application.Models.Properties;
using Domain;

namespace Application.Models.Images
{
    public class ImageDto
    {
        public int Id { get; set; }
        public int PropertyId { get; set; }
        public string? Name { get; set; }
        public string? ImagePath { get; set; }
        public bool IsUploaded { get; set; }

        //public PropertyDto? Property { get; set; }
    }
}
