using Microsoft.AspNetCore.Mvc;
using Minstrels.Interfaces;
using Minstrels.ViewModels;

namespace Minstrels.Controllers
{
    public class UserController:Controller
    {
        private readonly IUserRepository userRepository;

        public UserController(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        [HttpGet("users")]
        public async Task<IActionResult> Index()
        {
            var users = await userRepository.GetAllUsers();
            List<UserViewModel> result = new List<UserViewModel>();
            foreach (var user in users)
            {
                var userViewModel = new UserViewModel()
                {
                    UserName = user.UserName,
                    Id = user.Id,
                    MinstrelType = user.MinstrelType.ToString(),
                    Description = user.UserDescription
                };
                result.Add(userViewModel);  
            }
            return View(result);
        }

        public async Task<IActionResult> Detail(string id)
        {
            var user = await userRepository.GetUserById(id);
            var userDetailViewModel = new UserDetailViewModel()
            {
                Id = user.Id,
                UserName = user.UserName,
                MinstrelType = user.MinstrelType.ToString(),
                Description = user.UserDescription

            };
            return View(userDetailViewModel);
        }
       
    }
}
