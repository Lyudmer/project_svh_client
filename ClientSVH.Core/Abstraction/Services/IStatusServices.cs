namespace ClientSVH.Core.Abstraction.Services
{
    public interface IStatusServices
    {
        Task<int> AddStatus(int Id, string StName, bool RunWf, bool MkRes, bool SendMess);
        Task<bool> DelStatus(int Id);
    }
}