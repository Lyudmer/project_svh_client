using ClientSVH.Core.Abstaction.Repositories;
using ClientSVH.Core.Abstaction.Services;
using ClientSVH.Core.Abstraction.Repositories;
using ClientSVH.Core.Models;


namespace ClientSVH.Application.Services
{
    public class PackagesServices(IPackagesServices pkgService,
        ILoadFromFile loadFromFile,
        IPackagesRepository pkgRepository) : IPackagesServices
    {
        private readonly IPackagesServices _pkgService = pkgService;
        private readonly ILoadFromFile _loadFromFile = loadFromFile;
        private readonly IPackagesRepository _pkgRepository = pkgRepository;
        public Task<int> LoadFile(Guid UserId, string FileName)
        {
            return _loadFromFile.LoadFileXml(UserId, FileName);
        }
        public async Task<int> Add(Guid UserId, Package Pkg)
        {
            return await _pkgRepository.Add(UserId, Pkg);
        }
        public async Task<int> GetLastPkgId()
        {
            return await _pkgRepository.GetLastPkgId();
        }
    }
}
