namespace AdminWebPlatform.DTO
{
    public class RegisterUserDTO
    {
        public required string Username { get; set; }

        public required string Email { get; set; }

        public required string PasswordHash { get; set; }
    }
}