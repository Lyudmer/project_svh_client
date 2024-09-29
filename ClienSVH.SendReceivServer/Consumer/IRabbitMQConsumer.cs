namespace ClientSVH.SendReceivServer.Consumer
{
    public interface IRabbitMQConsumer
    {
        string LoadMessage(string CodeCMN);
    }
}