using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client;

namespace Rabit.BLL.RabbitMq
{
    public class RabbitMqService
    {
        private readonly string _hostName = "localhost",
            _userName = "guest",
            _password = "guest";

        public IConnection GetRabitMqConnection()
        {
            ConnectionFactory connectionFactory = new ConnectionFactory
            {
                HostName = _hostName,
                VirtualHost = "/",
                UserName = _userName,
                Password = _password,
                Uri=new Uri($"amqp://{_userName}:{_password}@{_hostName}")
            };
            return connectionFactory.CreateConnection();
        }
 


   }
}
