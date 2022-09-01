using Microsoft.AspNetCore.Mvc;
using Minstrels.Interfaces;
using Minstrels.ViewModels;

namespace Minstrels.Controllers
{
    public class DashboardController:Controller
    {
        private readonly IDashboardRepository dashboardRepository;
        private readonly IHttpContextAccessor httpContextAccessor;

        public DashboardController(IDashboardRepository dashboardRepository, IHttpContextAccessor httpContextAccessor)
        {
            this.dashboardRepository = dashboardRepository;
            this.httpContextAccessor = httpContextAccessor;
        }
       public async Task<IActionResult> Index()
        {
            var userShows = await dashboardRepository.GetAllUserShows();    
            var userRehearsals = await dashboardRepository.GetAllUserRehearsals();
            var dashboardViewModel = new DashboardViewModel
            {
                Shows = userShows,
                Rehearsals = userRehearsals,
            };
            return View(dashboardViewModel);
        }

    }
}
