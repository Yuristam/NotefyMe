using Microsoft.AspNetCore.Mvc;
using NotefyMe.Application.Interfaces;
using NotefyMe.WebApp.ViewModels;

namespace NotefyMe.WebApp.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUsersRepository _usersRepository;

        public UsersController(IUsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }

        [HttpGet("users")]
        public async Task<IActionResult> Index()
        {
            var users = await _usersRepository.GetAllUsers();
            
            List<UserViewModel> result = new List<UserViewModel>();
            
            foreach (var user in users)
            {
                var userViewModel = new UserViewModel()
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    Name = user.Name,
                    BirthDate = user.BirthDate
                };
            
                result.Add(userViewModel);
            }

            return View(result);
        }

        public async Task<IActionResult> Details(string id)
        {
            var user = await _usersRepository.GetUserId(id);
            
            var userDetailsViewModel = new UserDetailsViewModel()
            {
                Id = user.Id,
                UserName = user.UserName,
                Name = user.Name,
                BirthDate = user.BirthDate
            };

            return View(userDetailsViewModel);
        }
    }
}
