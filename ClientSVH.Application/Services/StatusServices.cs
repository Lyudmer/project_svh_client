
using ClientSVH.Core.Abstraction.Repositories;
using ClientSVH.Core.Abstraction.Services;


namespace ClientSVH.Application.Services
{
    public class StatusServices(IStatusRepositoty statusRepository) : IStatusServices
    {
        private readonly IStatusRepositoty _statusRepository = statusRepository;
        public async Task<int> AddStatus(int Id, string StName, bool RunWf, bool MkRes, bool SendMess)
        {
            return await _statusRepository.Add(Id, StName, RunWf, MkRes, SendMess);
        }

        public async Task<bool> DelStatus(int Id)
        {
            var status = await _statusRepository.GetById(Id) ?? throw new Exception("Invalid Id Status");
            if (status != null)
            {
                 await _statusRepository.Delete(Id);
                status = await _statusRepository.GetById(Id);
                return status == null;
            }
            throw new Exception("Invalid Del Status");

        }

      
    }
}
