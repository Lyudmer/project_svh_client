namespace ClientSVH.Application.Interfaces.Auth
{
    public interface ISendToServer
    {
         Task<int> SendPaskageToServer(int Pid);
    }
}