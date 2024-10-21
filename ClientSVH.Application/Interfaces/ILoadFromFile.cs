using System.Xml.Linq;

namespace ClientSVH.Application.Interfaces
{
    public interface ILoadFromFile
    {
        Task<string> LoadFileXml(Guid userId, string InFile);
    }
}
