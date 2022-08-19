using Microsoft.EntityFrameworkCore;
using Minstrels.Data;
using Minstrels.Interfaces;
using Minstrels.Models;

namespace Minstrels.Repository
{
    public class ShowRepository:IShowRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public ShowRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public bool Add(Show show)
        {
            _applicationDbContext.Shows.Add(show);
                return Save();  
        }

        public bool Delete(Show show)
        {
            _applicationDbContext.Shows.Remove(show);
            return Save();
        }

        public Task<List<Show>> GetAll()
        {
            return _applicationDbContext.Shows.ToListAsync();
        }

        public async Task<Show> GetByIdAsync(int id)
        {
            return await _applicationDbContext.Shows.Include(a => a.Address).FirstOrDefaultAsync(x => x.Id == id);    
        }

        public async Task<Show> GetByIdAsyncNoTracking(int id)
        {
            return await _applicationDbContext.Shows.Include(a => a.Address).AsNoTracking().FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<IEnumerable<Show>> GetShows(string city)
        {
            return await _applicationDbContext.Shows.Where(c => c.Address.City.Contains(city)).ToListAsync();
        }

        public bool Save()
        {
            var saved = _applicationDbContext.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool Update(Show show)
        {
            _applicationDbContext.Update(show);
            return Save();
        }
    }
}
