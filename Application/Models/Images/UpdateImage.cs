namespace Application.Models.Images
{
    public class UpdateImage
    {
        public int Id { get; set; }
        public int PropertyId { get; set; }
        public string? Name { get; set; }
        public string? ImagePath { get; set; }
    }
}
