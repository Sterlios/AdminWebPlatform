namespace AdminWebPlatform.Contracts
{
    public interface IHasher
    {
        string GetHash(string data);

        bool Verify(string hashedDate, string data);
    }
}
