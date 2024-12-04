namespace AdminWebPlatform.Models
{
    public class Role
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public AccessLevel ContentAccessLevel { get; set; }
        public AccessLevel UserAccessLevel { get; set; }
    }
}
