using AdminWebPlatform.Contexts;
using AdminWebPlatform.DTO;
using AdminWebPlatform.Models;
using Microsoft.EntityFrameworkCore;

namespace AdminWebPlatform.Repositories
{
    public class UserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context) =>
            _context = context ?? throw new ArgumentNullException(nameof(context));

        public bool HasEmail(string email)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(email, nameof(email));

            return _context.Users.Any(user => user.Email == email);
        }

        public async Task Add(User user)
        {
            if (user is null)
                throw new ArgumentNullException(nameof(user));

            AccessLevel userAccessLevel = AccessLevel.None;

            if (_context.Users.Any() == false)
            {
                userAccessLevel = _context.Roles.Max(role => role.UserAccessLevel);
            }

            user.Role = _context.Roles.First(role => role.UserAccessLevel == userAccessLevel);

            _context.Users.Add(user);

            await _context.SaveChangesAsync();
        }

        internal Task<User?> GetByEmail(string email)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(email, nameof(email));
            User user = _context.Users
                .Include(user => user.Role)
                .FirstOrDefault(user => user.Email == email);

            return Task.FromResult(user);
        }

        internal async Task<IQueryable<UserDTO>> GetAllAsync()
        {
            return _context.Users.Select(user => new UserDTO
            {
                Id = user.Id,
                Username = user.Username,
                Email = user.Email,
                Role = user.Role.Name
            });
        }
    }
}