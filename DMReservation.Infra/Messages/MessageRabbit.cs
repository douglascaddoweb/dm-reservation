using DMReservation.Domain.Interfaces.Infra;
using DMReservation.Domain.Settings;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;

namespace DMReservation.Infra.Messages
{
    public class MessageRabbit : IMessageRabbit
    {
        private readonly ConnectionFactory _connectionFactory;

        public MessageRabbit()
        {
            _connectionFactory = new ConnectionFactory
            {
                HostName = GeneralSetting.HostRabbit,
                UserName = GeneralSetting.UserName,
                Password = GeneralSetting.Password,
                Port = GeneralSetting.Port
            };
        }

        public async Task ExecuteAsync(int idorder, int iddeliveryman)
        {

            using(var connection = _connectionFactory.CreateConnection())
            {
                using(var chanel = connection.CreateModel())
                {
                    var bytes = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(new { idorder, iddeliveryman }));

                    chanel.QueueDeclare(
                        queue: $"DMReservation.Message.DeliveryMan.{iddeliveryman}",
                        durable: true,
                        exclusive:false,
                        autoDelete: false,
                        arguments: null);

                    chanel.BasicPublish(
                        exchange: "",
                        routingKey: $"DMReservation.Message.DeliveryMan.{iddeliveryman}",
                        basicProperties: null,
                        body: bytes);
                }
            }
        }
    }
}
