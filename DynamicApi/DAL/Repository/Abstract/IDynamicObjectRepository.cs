using DynamicAPI.Entity.DynamicObjectDb;

namespace DynamicAPI.DAL.Repository.Abstract
{
    public interface IDynamicObjectRepository
    {
        Task AddAsync(Objects objects);
        Task UpdateAsync(Objects objects);
        Task DeleteAsync(Objects objects);
        Task<List<Objects>> GetByType(string type);
        Task<Objects?> GetById(int id);
        Task<Objects?> GetByTypeAndId(string type, int id);
    }
}
