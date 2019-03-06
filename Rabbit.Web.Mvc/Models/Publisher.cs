using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using RabbitMQ.Client;
using Rabit.BLL.RabbitMq;

namespace Rabbit.Web.Mvc.Models
{
    public class Publisher
    {
        private readonly RabbitMqService _rabbitMqService;
        private const string DefaultQueue = "wissen1";
        public Publisher(string message, string queueName = null)
        {
            if (string.IsNullOrEmpty(queueName))
                queueName = DefaultQueue;
            _rabbitMqService = new RabbitMqService();

            using (var connection = _rabbitMqService.GetRabbitMqConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queueName, false, false, false, null);
                    channel.BasicPublish(string.Empty, queueName, null, Encoding.UTF8.GetBytes(message));

                    //Console.WriteLine($"{queueName} queue'su üzerine, \"{message}\" mesajı yazıldı.");
                }
            }
        }
    }
}
