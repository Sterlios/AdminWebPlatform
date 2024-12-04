using AdminWebPlatform.Contracts;
using AdminWebPlatform.Models;
using AdminWebPlatform.Repositories;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AdminWebPlatform.Controllers
{
    public class AccountController : Controller
    {
        private readonly IHasher _hashService;
        private readonly UserRepository _userRepository;

        public AccountController(IHasher hashService, UserRepository userRepository)
        {
            _hashService = hashService ?? throw new ArgumentNullException(nameof(hashService));
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (_userRepository.HasEmail(model.Email))
                {
                    ModelState.AddModelError("", "Email is already registered.");
                    return View(model);
                }

                string hashedPassword = _hashService.GetHash(model.Password);

                var user = new User
                {
                    Username = model.Username,
                    Email = model.Email,
                    PasswordHash = hashedPassword
                };

                await _userRepository.Add(user);

                return RedirectToAction("Login");
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                User? user = await _userRepository.GetByEmail(model.Email);

                if (user == null || _hashService.Verify(user.PasswordHash, model.Password) == false)
                {
                    ModelState.AddModelError("", "Invalid email or password.");
                    return View(model);
                }

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.Username),
                };

                var identity = new ClaimsIdentity(claims, "Cookie");
                var principal = new ClaimsPrincipal(identity);
                await HttpContext.SignInAsync(principal);

                return RedirectToAction("Index");
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Login");
        }
    }
}
