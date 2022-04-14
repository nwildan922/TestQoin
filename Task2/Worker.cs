using System;
using System.Data.SqlClient;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.Exceptions;
using Task2.Model;

namespace Task2
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private ConnectionFactory _connectionFactory;
        private IConnection _connection;
        private IModel _channel;
        private const string QueueName = "qtest1";

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
        }
        public override Task StartAsync(CancellationToken cancellationToken)
        {
            var rabbitHostName = "localhost";
            _connectionFactory = new ConnectionFactory
            {
                HostName = rabbitHostName,
                Port = 5672,
                UserName = "guest",
                Password = "guest",
                DispatchConsumersAsync = true
            };
            _connection = _connectionFactory.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.QueueDeclarePassive(QueueName);
            _channel.BasicQos(0, 1, false);
            _logger.LogInformation($"Queue [{QueueName}] is waiting for messages.");

            return base.StartAsync(cancellationToken);
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();

            var consumer = new AsyncEventingBasicConsumer(_channel);
            consumer.Received += async (bc, ea) =>
            {
                var message = Encoding.UTF8.GetString(ea.Body.ToArray());
                _logger.LogInformation($"Processing msg: '{message}'.");
                try
                {
                    var data = JsonConvert.DeserializeObject<MqModel>(message);
                    MessageHandling(data.Command, data.Data);
                    _channel.BasicAck(ea.DeliveryTag, false);

                }
                catch (AlreadyClosedException)
                {
                    _logger.LogInformation("RabbitMQ is closed!");
                }
                catch (Exception e)
                {
                    _logger.LogError(default, e, e.Message);
                }
            };

            _channel.BasicConsume(queue: QueueName, autoAck: false, consumer: consumer);

            await Task.CompletedTask;
        }
        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            await base.StopAsync(cancellationToken);
            _connection.Close();
            _logger.LogInformation("RabbitMQ connection is closed.");
        }

        private void MessageHandling(string command, Test01 data) 
        {
            var context = new EFContext();
            if (command == "create") 
            {
                data.Created = DateTime.Now;
                context.Test01.Add(data);
            } 
            else if (command == "update")
            {
                var existingData = context.Test01.Find(data.Id);
                if (existingData != null) 
                {
                    existingData.Nama = data.Nama;
                    existingData.Status = data.Status;
                    existingData.Updated = DateTime.Now;
                }
            }
            else if (command == "delete")
            {
                var existingData = context.Test01.Find(data.Id);
                if (existingData != null)
                    context.Test01.Remove(existingData);
            }
            context.SaveChanges();
        }
    }
}
