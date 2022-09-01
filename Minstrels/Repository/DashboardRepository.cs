using Microsoft.EntityFrameworkCore;
using Minstrels.Data;
using Minstrels.Interfaces;
using Minstrels.Models;

namespace Minstrels.Repository
{
    public class DashboardRepository:IDashboardRepository
    {
        private readonly ApplicationDbContext context;
        private readonly IHttpContextAccessor httpContextAccessor;

        public DashboardRepository(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            this.context = context;
            this.httpContextAccessor = httpContextAccessor;
        }

        public async Task<List<Rehearsal>> GetAllUserRehearsals()
        {
            var curUser = httpContextAccessor.HttpContext?.User.GetUserId();
            var userRehearsals = context.Rehearsals.Where(r => r.AppUser.Id == curUser);
            return userRehearsals.ToList();
        }

        public async Task<List<Show>> GetAllUserShows()
        {
           var curUser = httpContextAccessor.HttpContext?.User.GetUserId();    
            var userShows = context.Shows.Where(r => r.AppUser.Id == curUser);
            return userShows.ToList();

        }

        public async Task<AppUser> GetByIdNoTracking(string id)
        {
            return await context.Users.Where(u => u.Id == id).AsNoTracking().FirstOrDefaultAsync();

        }

        public async Task<AppUser> GetUserById(string id)
        {
            return await context.Users.FindAsync(id);

        }

        public bool Save()
        {
            var saved = context.SaveChanges();
            return saved > 0 ?true : false;
        }

        public bool Update(AppUser user)
        {
          context.Users.Update(user);
            return Save();
        }
        
    }
}
