using DMReservation.Domain.DTOs;
using DMReservation.Domain.Entities;
using DMReservation.Domain.Interfaces.Infra;
using DMReservation.Domain.Settings;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace DMReservation.Consumer.Worker
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;

        private readonly IConnection _connection;
        private readonly IModel _chanel;
        private readonly IServiceProvider _serviceProvider;

        private readonly INotifyOrderRepository _notifyOrderRepository;
        private readonly IDeliveryManRepository _deliveryMan;
        private readonly IOrderRepository _orderRepository;


        public Worker(ILogger<Worker> logger, IServiceProvider serviceProvider)
        {
            _logger = logger;
            _serviceProvider = serviceProvider; 

            ConnectionFactory factory = new ConnectionFactory
            {
                HostName = GeneralSetting.HostRabbit,
                UserName = GeneralSetting.UserName,
                Password = GeneralSetting.Password,
                Port = GeneralSetting.Port
            };

            _connection = factory.CreateConnection();
            _chanel = _connection.CreateModel();
            _chanel.QueueDeclare(
                queue: GeneralSetting.ChannelConsumer,
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null);
        }

        /// <summary>
        /// Inicia o processo de consumo de mensagens
        /// </summary>
        /// <param name="stoppingToken"></param>
        /// <returns></returns>
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            EventingBasicConsumer consumer = new EventingBasicConsumer(_chanel);

            consumer.Received += async (sender, eventArgs) =>
            {
                byte[] arrContent = eventArgs.Body.ToArray();
                string content = Encoding.UTF8.GetString(arrContent);
                SendMessageRabbitDto rabbitMessage = JsonConvert.DeserializeObject<SendMessageRabbitDto>(content);

                await SaveNotifyAsync(rabbitMessage);

                _chanel.BasicAck(eventArgs.DeliveryTag, false);
            };

            _chanel.BasicConsume(GeneralSetting.ChannelConsumer, false, consumer);

            return Task.CompletedTask;
        }

        /// <summary>
        /// Grava a notificação recebida
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        private async Task SaveNotifyAsync(SendMessageRabbitDto message)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
        
                INotifyOrderRepository _notifyOrderRepository = scope.ServiceProvider.GetRequiredService<INotifyOrderRepository>();
                IDeliveryManRepository _deliveryManRepository = scope.ServiceProvider.GetRequiredService<IDeliveryManRepository>();
                IOrderRepository _orderRepository = scope.ServiceProvider.GetRequiredService<IOrderRepository>();


                DeliveryMan delivery = await _deliveryManRepository.FindIdAsync(message.IdDeliveryMan);
                Order order = await _orderRepository.FindIdAsync(message.IdOrder);

                if (delivery is not DeliveryMan || order is not Order)
                {
                    _logger.LogError("Unable to create notification");
                    return;
                }

                NotifyOrder notify = new NotifyOrder(delivery, order);

                await _notifyOrderRepository.AddAsync(notify);
                await _notifyOrderRepository.CommitAsync();
            }
        }
    }
}
