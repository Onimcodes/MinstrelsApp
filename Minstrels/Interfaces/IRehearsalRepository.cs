using Minstrels.Data;
using Minstrels.Models;

namespace Minstrels.Interfaces
{
    public interface IRehearsalRepository
    {

        Task<List<Rehearsal>> GetAll();
        Task<Rehearsal> GetByIdAsync(int id);
        Task<Rehearsal> GetByIdAsyncNoTracking(int id);
        Task<IEnumerable<Rehearsal>> GetRehearsalByCity(string city);    
       bool Add(Rehearsal rehearsal);
        bool Update(Rehearsal rehearsal);   
        bool Delete(Rehearsal rehearsal);
        bool Save();

    }
}
