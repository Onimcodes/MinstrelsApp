using Minstrels.Models;

namespace Minstrels.Interfaces
{
    public interface IDashboardRepository
    {
        Task<List<Show>> GetAllUserShows();
        Task<List<Rehearsal>> GetAllUserRehearsals();
        Task<AppUser> GetUserById(string id);
        Task<AppUser> GetByIdNoTracking(string id);
        bool Update(AppUser user);
        bool Save();
    }
}
