namespace ClientSVH.Application.Interfaces
{
    public interface ISendToServer
    {
        Task<int> SendPaskageToServer(int Pid);
    }
}