using System.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using RabbitMQ.Client;

namespace FlightBooking.API.Services;

public class MessageProducer : IMessageProducer
{
  public void SendingMessage<T>(T msg)
  {
    var factory = new ConnectionFactory()
    {
      HostName = "localhost",
      UserName = "guest",
      Password = "guest",
      VirtualHost = "/"
    };

    var conn = factory.CreateConnection();
    using var channel = conn.CreateModel();

    var isOk = channel.QueueDeclare("bookings", durable: true, exclusive: false);

    var jsonStr = JsonSerializer.Serialize(msg);
    var body = Encoding.UTF8.GetBytes(jsonStr);

    channel.BasicPublish("", "bookings", body: body);
  }
}
