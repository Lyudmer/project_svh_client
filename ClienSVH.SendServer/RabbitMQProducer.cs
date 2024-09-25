using Microsoft.Extensions.Configuration;
using ClientSVH.SendServer.Settings;
using RabbitMQ.Client;
using System.Text;


namespace ClientSVH.SendServer
{
    public class RabbitMQProducer : IMessageProducer
    {
        public void SendMessage<T>(T xPkg, string CodeCMN)
        {
            IConfiguration configuration = new ConfigurationBuilder()
                 .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                 .Build();

            var connection = GetRabbitConnection(configuration);
            using var channel = connection.CreateModel();

            channel.QueueDeclare(CodeCMN, exclusive: false);
            var strPkg = xPkg?.ToString();
            if (strPkg != null)
            {
                var body = Encoding.UTF8.GetBytes(strPkg);

                channel.BasicPublish(exchange: "", routingKey: CodeCMN, body: body);
            }
        }
        private static IConnection GetRabbitConnection(IConfiguration configuration)
        {

            var rmqSettings = configuration.Get<ApplicationSettings>()?.RmqSettings;
            ConnectionFactory factory = new()
            {
                HostName = rmqSettings?.Host,
                VirtualHost = rmqSettings?.VHost,
                UserName = rmqSettings?.Login,
                Password = rmqSettings?.Password,
            };

            return factory.CreateConnection();
        }
    }
}
