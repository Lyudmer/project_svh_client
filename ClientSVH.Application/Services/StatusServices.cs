
using ClientSVH.Core.Abstraction.Repositories;
using ClientSVH.Core.Abstraction.Services;
using ClientSVH.Core.Models;


namespace ClientSVH.Application.Services
{
    public class StatusServices(IStatusRepositoty statusRepository) : IStatusServices
    {
        private readonly IStatusRepositoty _statusRepository = statusRepository;
        public async Task<string> AddStatus(Status status)
        {
             var resId = await _statusRepository.Add(status);
            if (resId>0)
                return resId.ToString();
            else return "статус не добавлен";
        }

        public async Task<string> DelStatus(int Id)
        {
            var status = await _statusRepository.GetById(Id) ?? throw new Exception("Invalid Id Status");
            if (status != null)
            {
                 await _statusRepository.Delete(Id);
                status = await _statusRepository.GetById(Id);
                return (status == null)?"Статус удален": "Ошибка удаления статуса";
            }
            else return  "Ошибка удаления статуса"; 

        }

      
    }
}
