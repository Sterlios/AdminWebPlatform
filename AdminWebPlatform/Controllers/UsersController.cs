using AdminWebPlatform.Attributes;
using AdminWebPlatform.Models;
using AdminWebPlatform.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace AdminWebPlatform.Controllers
{
    public class UsersController : Controller
    {
        private readonly UserRepository _userRepository;

        public UsersController(UserRepository userRepository) =>
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));

        [AccessToUsers(AccessLevel.Read)]
        [HttpGet("users")]
        public async Task<IActionResult> ShowUsers()
        {
            var users = await _userRepository.GetAllAsync();

            var usersList = users.ToList();

            ViewBag.UserId = int.Parse(User.FindFirst("UserId")?.Value ?? "0");

            ViewBag.CanEdit = ((AccessLevel)int.Parse(User.FindFirst("UserAccessLevel")?.Value ?? "0") & AccessLevel.Edit) == AccessLevel.Edit;

            ViewBag.CanDelete = ((AccessLevel)int.Parse(User.FindFirst("UserAccessLevel")?.Value ?? "0") & AccessLevel.Delete) == AccessLevel.Delete;

            return View(usersList);
        }
    }
}
