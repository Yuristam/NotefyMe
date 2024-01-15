using Microsoft.AspNetCore.Mvc;
using NotefyMe.Application.Services;
using NotefyMe.Domain.Users;
using NotefyMe.Infrastructure.Extensions;
using NotefyMe.WebApp.ViewModels;

namespace NotefyMe.WebApp.Controllers
{
    public class DashboardController : Controller
    {
        private readonly IDashboardService _dashboardService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public DashboardController(IDashboardService dashboardService, IHttpContextAccessor httpContextAccessor)
        {
            _dashboardService = dashboardService;
            _httpContextAccessor = httpContextAccessor;
        }

        private void MapUserEdit(WebUser user, EditUserDashboardViewModel editVM)
        {
            user.Id = editVM.Id;
            user.UserName = editVM.UserName;
            user.Name = editVM.Name;
            user.BirthDate = editVM.BirthDate;
        }

        public async Task<IActionResult> Index()
        {
            var userNotes = await _dashboardService.GetAllUserNotes();

            var dashboardViewModel = new DashboardViewModel()
            {
                Notes = userNotes
            };

            return View(dashboardViewModel);
        }

        public async Task<IActionResult> EditUserProfile()
        {
            var currentUserId = _httpContextAccessor.HttpContext.User.GetUserId();
            var user = await _dashboardService.GetUserById(currentUserId);

            if (user == null) return View("Error");

            var editUserViewModel = new EditUserDashboardViewModel()
            {
                Id = currentUserId,
                UserName = user.UserName,
                Name = user.Name,
                BirthDate = user.BirthDate
            };

            return View(editUserViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> EditUserProfile(EditUserDashboardViewModel editVM)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Failed to edit profile");
                return View("EditUserProfile", editVM);
            }

            var user = await _dashboardService.GetUserByIdNoTracking(editVM.Id);
            MapUserEdit(user, editVM);
            _dashboardService.Update(user);

            return RedirectToAction("Index");
        }
    }
}