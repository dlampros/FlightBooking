using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlightBooking.API.Models;
using FlightBooking.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace FlightBooking.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BookingsController : ControllerBase
{
  private readonly ILogger<BookingsController> _logger;
  private readonly IMessageProducer _messageProducer;

  // In-Memoty db
  public static readonly List<Booking> _bookings = new();


  public BookingsController(ILogger<BookingsController> logger, IMessageProducer messageProducer)
  {
    _logger = logger;
    _messageProducer = messageProducer;
  }

  [HttpPost]
  public IActionResult CreateBooking([FromBody] Booking newBooking)
  {
    if (!ModelState.IsValid) return BadRequest();

    _bookings.Add(newBooking); 
    _messageProducer.SendingMessage<Booking>(newBooking);

    return Ok();
  }
}
