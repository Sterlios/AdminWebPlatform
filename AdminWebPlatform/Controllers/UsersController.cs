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

            ViewBag.CanEdit = ((AccessLevel)int.Parse(User.FindFirst("UserAccessLevel")?.Value ?? "0") & AccessLevel.Edit) == AccessLevel.Edit;

            return View(usersList);
        }
    }
}
