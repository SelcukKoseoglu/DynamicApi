using DynamicAPI.DAL.Repository.Abstract;
using DynamicAPI.Entity.DynamicObjectDb;

namespace DynamicAPI.Business.DynamicObjectMethods
{
    public class ObjectMethods : IObjectMethods
    {
        private readonly IDynamicObjectRepository _dynamicObjectRepository;

        public ObjectMethods(IDynamicObjectRepository dynamicObjectRepository)
        {
            _dynamicObjectRepository = dynamicObjectRepository;
        }
        public async Task AddObject(Objects objects)
        {
            try
            {
                await _dynamicObjectRepository.AddAsync(objects).ConfigureAwait(false);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
            
        }
        public async Task<Objects?> GetObject(string type,int id)
        {
            try
            {
                var result = await _dynamicObjectRepository.GetByTypeAndId(type,id).ConfigureAwait(false);
                return result;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }

        }
        public async Task<Objects?> GetObjectById(int id)
        {
            try
            {
                var result = await _dynamicObjectRepository.GetById(id).ConfigureAwait(false);
                return result;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }

        }
        public async Task UpdateObject(Objects objects)
        {
            try
            {
                await _dynamicObjectRepository.UpdateAsync(objects).ConfigureAwait(false);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }

        }
        public async Task DeleteObject(Objects objects)
        {
            try
            {
                await _dynamicObjectRepository.DeleteAsync(objects).ConfigureAwait(false);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }

        }

    }
}
