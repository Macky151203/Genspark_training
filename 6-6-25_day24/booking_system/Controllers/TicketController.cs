namespace BookingSystem.Controllers;

using Microsoft.AspNetCore.Mvc;
using BookingSystem.Models;
using BookingSystem.Models.DTOs;
using BookingSystem.Interfaces;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;

[Route("api/[controller]")]
[ApiController]

public class TicketController : ControllerBase
{
    private readonly ITicketService _ticketService;

    public TicketController(ITicketService ticketService)
    {
        _ticketService = ticketService;
    }

    [HttpPost("book")]
    [Authorize(Roles = "Customer")]
    public async Task<ActionResult<Ticket>> BookTicket([FromBody] TicketDto ticketDto)
    {
        try
        {
            var ticket = await _ticketService.BookTicket(ticketDto);
            return CreatedAtAction(nameof(GetTicketById), new { id = ticket.Id }, ticket);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    [HttpGet("{id}")]
    [Authorize(Roles = "Customer")]
    public async Task<ActionResult<Ticket>> GetTicketById(int id)
    {
        var ticket = await _ticketService.GetTicketById(id);
        if (ticket == null)
        {
            return NotFound();
        }
        return Ok(ticket);
    }
    [HttpDelete("{id}/cancel")]
    [Authorize(Roles = "Customer")]
    public async Task<ActionResult<Ticket>> CancelTicketById(int id)
    {
        try
        {
            var ticket = await _ticketService.CancelTicketById(id);
            return Ok(ticket);
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }

}