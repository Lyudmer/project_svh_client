using System.Text;
using ClientSVH.Application.Interfaces;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace ClientSVH.SendReceivServer.Consumer
{
    public class RabbitMQConsumer(IRabbitMQBase rabbitMQBase) : IRabbitMQConsumer
    {
        private readonly IRabbitMQBase _rabbitMQBase = rabbitMQBase;

        public string LoadMessage(string CodeCMN)
        {
            string resLoadMessage = "";
            using IModel channel = _rabbitMQBase.GetConfigureRabbitMQ();

            channel.QueueDeclare(CodeCMN, false, true, false, null);

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (sender, e) =>
            {
                var body = e.Body;
                resLoadMessage = Encoding.UTF8.GetString(body.ToArray());
                channel.BasicAck(e.DeliveryTag, false);

            };

            channel.BasicConsume(CodeCMN, false, consumer);

            return resLoadMessage;
        }
    }
}