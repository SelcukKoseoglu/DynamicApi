using DynamicAPI.DAL.Context;
using DynamicAPI.DAL.Repository.Abstract;
using DynamicAPI.Entity.DynamicObjectDb;
using Microsoft.EntityFrameworkCore;

namespace DynamicAPI.DAL.Repository.Concrete
{
    public class DynamicObjectRepository : IDynamicObjectRepository
    {
        private readonly IDbContextFactory<DynamicApiDbContext> _dbContextFactory;

        public DynamicObjectRepository(IDbContextFactory<DynamicApiDbContext> dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }
        public async Task AddAsync(Objects objects)
        {
            using var _context = await _dbContextFactory.CreateDbContextAsync().ConfigureAwait(false);
            await _context.AddAsync(objects).ConfigureAwait(false);
            await _context.SaveChangesAsync().ConfigureAwait(false);
        }
        public async Task UpdateAsync(Objects objects)
        {
            using var _context = await _dbContextFactory.CreateDbContextAsync().ConfigureAwait(false);
            _context.Update(objects);
            await _context.SaveChangesAsync().ConfigureAwait(false);
        }
        public async Task DeleteAsync(Objects objects)
        {
            using var _context = await _dbContextFactory.CreateDbContextAsync().ConfigureAwait(false);
            _context.Remove(objects);
            await _context.SaveChangesAsync().ConfigureAwait(false);
        }
        public async Task<List<Objects>> GetByType(string type)
        {
            using var _context = await _dbContextFactory.CreateDbContextAsync().ConfigureAwait(false);
            return await _context.Objects.Where(x=>x.Type == type).ToListAsync().ConfigureAwait(false);
        }
        public async Task<Objects?> GetById(int id)
        {
            using var _context = await _dbContextFactory.CreateDbContextAsync().ConfigureAwait(false);
            return  _context.Objects.FirstOrDefault(x => x.Id == id);
        }
        public async Task<Objects?> GetByTypeAndId(string type,int id)
        {
            using var _context = await _dbContextFactory.CreateDbContextAsync().ConfigureAwait(false);
            return  _context.Objects.FirstOrDefault(x => x.Type == type && x.Id == id);
        }
    }
}
