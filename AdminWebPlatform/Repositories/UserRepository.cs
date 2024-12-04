using AdminWebPlatform.Contexts;
using AdminWebPlatform.Models;

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

            AccessLevel accessLevel = AccessLevel.None;

            if (_context.Users.Any() == false)
                accessLevel = _context.Roles.Max(role => role.AccessLevel);

            user.Role = _context.Roles.First(role => role.AccessLevel == accessLevel);

            _context.Users.Add(user);

            await _context.SaveChangesAsync();
        }

        internal Task<User?> GetByEmail(string email)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(email, nameof(email));

            return Task.FromResult(_context.Users.FirstOrDefault(user => user.Email == email));
        }
    }
}