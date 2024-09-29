namespace ClientSVH.Application.Interfaces
{
    public interface IReceivFromServer
    {
        Task<int> LoadMessage();
    }
}
