using ClientSVH.Application.Interfaces;
using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using System.Text;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;


namespace ClientSVH.SendReceivServer.Producer
{
    public class RabbitMQProducer(IRabbitMQBase rabbitMQBase) : IMessagePublisher
    {
        private readonly IRabbitMQBase _rabbitMQBase = rabbitMQBase;
        public int SendMessage<T>(T xPkg, string CodeCMN, int inStatus)
        {
            using IModel channel = _rabbitMQBase.GetConfigureRabbitMQ();

            int resStatus = inStatus;

            var producer = new Producer("direct", "exchange.direct", CodeCMN, channel);
            if (producer.Produce(xPkg.ToString()))
            {
                if (resStatus == 0) resStatus = 1;
            }
           // _rabbitMQBase.CloseModelRabbitMQ(channel);
            return resStatus;
        }

    }

}
