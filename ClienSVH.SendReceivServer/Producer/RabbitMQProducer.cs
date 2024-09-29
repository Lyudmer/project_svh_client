using ClientSVH.Application.Interfaces;
using RabbitMQ.Client;
using System.Text;


namespace ClientSVH.SendReceivServer.Producer
{
    public class RabbitMQProducer(IRabbitMQBase rabbitMQBase) : IMessagePublisher
    {
        private readonly IRabbitMQBase _rabbitMQBase = rabbitMQBase;
        public int SendMessage<T>(T xPkg, string CodeCMN, int inStatus)
        {
            using IModel channel = _rabbitMQBase.GetConfigureRabbitMQ();
            int resStatus = inStatus;
            channel.QueueDeclare(CodeCMN, exclusive: false);
            var strPkg = xPkg?.ToString();
            if (strPkg != null)
            {
                var body = Encoding.UTF8.GetBytes(strPkg);

                channel.BasicPublish(exchange: "package", routingKey: CodeCMN, body: body);
                if (resStatus == 0) resStatus = 1;
            }

            return resStatus;
        }
    }
}
