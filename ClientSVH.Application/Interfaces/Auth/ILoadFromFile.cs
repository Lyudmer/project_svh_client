namespace ClientSVH.Application.Interfaces.Auth
{
    public interface ILoadFromFile
    {
        Task<int> LoadFile(Guid userId, string InFile);
    }
}
