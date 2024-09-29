namespace ClientSVH.SendReceivServer.Producer
{
    public interface IMessagePublisher
    {
        int SendMessage<T>(T message, string CodeCMN, int inStatus);
    }
}
