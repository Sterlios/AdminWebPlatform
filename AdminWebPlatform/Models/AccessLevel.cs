namespace AdminWebPlatform.Models
{
    [Flags]
    public enum AccessLevel
    {
        None = 0,
        Read = 1 << 0,
        Edit = 1 << 1,
        Create = 1 << 2,
        Delete = 1 << 3
    }
}
