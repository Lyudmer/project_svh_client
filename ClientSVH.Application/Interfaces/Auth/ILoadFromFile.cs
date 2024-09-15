namespace ClientSVH.Core.Abstraction.Repositories
{
    public interface ILoadFromFile
    {
        Task<int> LoadFileXml(Guid userId, string FileName);
    }
}
