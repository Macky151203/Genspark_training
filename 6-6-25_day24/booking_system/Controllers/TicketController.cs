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
    private readonly ILogger<AuthenticationController> _logger;

    public TicketController(ITicketService ticketService, ILogger<AuthenticationController> logger)
    {
        _ticketService = ticketService;
        _logger = logger;
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
            _logger.LogError(ex, "Error booking ticket for event {EventName}", ticketDto.EventName);
            return BadRequest(ex.Message);
        }
    }
    [HttpGet("{id}")]
    [Authorize(Roles = "Customer")]
    public async Task<ActionResult<Ticket>> GetTicketById(int id)
    {
        try
        {
            var ticket = await _ticketService.GetTicketById(id);
            if (ticket == null)
            {
                _logger.LogWarning("Ticket with ID {Id} not found", id);
                return NotFound();
            }
            return Ok(ticket);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving ticket with ID {Id}", id);
            return NotFound(ex.Message);
        }
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
            _logger.LogError(ex, "Error canceling ticket with ID {Id}", id);
            return NotFound(ex.Message);
        }
    }

}