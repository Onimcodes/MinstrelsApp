using Microsoft.EntityFrameworkCore;
using Minstrels.Data;
using Minstrels.Interfaces;
using Minstrels.Models;

namespace Minstrels.Repository
{
    public class RehearsalRepository:IRehearsalRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public RehearsalRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public bool Add(Rehearsal rehearsal)
        {
            _applicationDbContext.Rehearsals.Add(rehearsal);
            return Save();
        }

        public bool Delete(Rehearsal rehearsal)
        {
            _applicationDbContext.Rehearsals.Remove(rehearsal);
            return Save();

        }

        public async Task<List<Rehearsal>> GetAll()
        {
            return await _applicationDbContext.Rehearsals.ToListAsync();
        }

        public async Task<Rehearsal> GetByIdAsync(int id)
        {
           return await _applicationDbContext.Rehearsals.Include(i => i.Address).FirstOrDefaultAsync(c => c.Id == id);        


        }

        public async Task<Rehearsal> GetByIdAsyncNoTracking(int id)
        {
            return await _applicationDbContext.Rehearsals.Include(i => i.Address).AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<IEnumerable<Rehearsal>> GetRehearsalByCity(string city)
        {
           return await _applicationDbContext.Rehearsals.Where(c=> c.Address.City.Contains(city)).ToListAsync();
        }

        public bool Save()
        {
          var saved = _applicationDbContext.SaveChanges();
            return saved > 0 ?  true : false;
        }

        public bool Update(Rehearsal rehearsal)
        {
            _applicationDbContext.Update(rehearsal);
                return Save();
        }
    }
}
