using Minstrels.Models;

namespace Minstrels.Interfaces
{
    public interface IShowRepository
    {
        Task<List<Show>> GetAll();
        Task<Show> GetByIdAsync(int id);
        Task<Show> GetByIdAsyncNoTracking(int id); 
        Task<IEnumerable<Show>> GetShows(string city);
        bool Add(Show show);
        bool Update(Show show);
        bool Delete(Show show);
        bool Save();
    }
}
