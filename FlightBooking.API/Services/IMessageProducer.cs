using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightBooking.API.Services;

public interface IMessageProducer
{
  public void SendingMessage<T>(T msg);
}
