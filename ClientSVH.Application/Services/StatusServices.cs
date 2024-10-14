
using ClientSVH.Core.Abstraction.Repositories;
using ClientSVH.Core.Abstraction.Services;
using ClientSVH.Core.Models;


namespace ClientSVH.Application.Services
{
    public class StatusServices(IStatusRepositoty statusRepository) : IStatusServices
    {
        private readonly IStatusRepositoty _statusRepository = statusRepository;
        public async Task<int> AddStatus(int Id, string StName, bool RunWf, bool MkRes, bool SendMess)
        {
            var status = Status.Create(Id, StName, RunWf, MkRes, SendMess);
            return await _statusRepository.Add(status);
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
