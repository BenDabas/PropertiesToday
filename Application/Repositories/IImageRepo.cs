using Domain;

namespace Application.Repositories
{
    public interface IImageRepo
    {
        Task AddNewAsync(Image property);
        Task DeleteAsync(Image property);
        Task<List<Image>> GetAllAsync();
        Task UpdateAsync(Image property);
        Task<Image?> GetByIdAsync(int id);
    }
}
