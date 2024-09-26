using DynamicAPI.Entity.DynamicObjectDb;

namespace DynamicAPI.Business.DynamicObjectMethods
{
    public interface IObjectMethods
    {
        Task AddObject(Objects objects);
        Task<Objects?> GetObject(string type,int id);
        Task<Objects?> GetObjectById(int id);
        Task UpdateObject(Objects objects);
        Task DeleteObject(Objects objects);
    }
}
