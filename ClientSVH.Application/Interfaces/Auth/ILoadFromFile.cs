using System.Xml.Linq;

namespace ClientSVH.Application.Interfaces.Auth
{
    public interface ILoadFromFile
    {
        Task<int> LoadFileXml(Guid userId, string InFile);
    }
}
