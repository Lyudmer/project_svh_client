using ClientSVH.Core.Abstraction.Services;
using ClientSVH.Core.Abstraction.Repositories;
using ClientSVH.Core.Models;


namespace ClientSVH.Application.Services
{
    public class PackagesServices(ILoadFromFile loadFromFile,
        IPackagesRepository pkgRepository) : IPackagesServices
    {
        
        private readonly ILoadFromFile _loadFromFile = loadFromFile;
        private readonly IPackagesRepository _pkgRepository = pkgRepository;
        public Task<int> LoadFile(Guid UserId, string FileName)
        {
            return _loadFromFile.LoadFileXml(UserId, FileName);
        }
        public async Task<Package> Add(Package Pkg)
        {
            return await _pkgRepository.Add(Pkg);
        }
        public async Task<int> GetLastPkgId()
        {
            return await _pkgRepository.GetLastPkgId();
        }
    }
}
