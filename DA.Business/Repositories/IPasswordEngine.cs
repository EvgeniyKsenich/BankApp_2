namespace BA.Business.Repositories
{
    public interface IPasswordEngine
    {
        string GetHash(string text);
    }
}
