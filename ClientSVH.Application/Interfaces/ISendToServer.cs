namespace ClientSVH.Application.Interfaces
{
    public interface ISendToServer
    {
        Task<int> SendPaskageToServer(int Pid);
        Task<bool> SendDelPkgToServer(int Pit);
        Task<int> PkgFLK(int Pit);
    }
}