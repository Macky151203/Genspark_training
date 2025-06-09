namespace BookingSystem.Controllers;

using Microsoft.AspNetCore.Mvc;
using BookingSystem.Models;
using BookingSystem.Models.DTOs;
using BookingSystem.Interfaces;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;

[ApiController]
[Route("api/[controller]")]
public class EventsController : ControllerBase
{
    private readonly IEventService _eventService;

    public EventsController(IEventService eventService)
    {
        _eventService = eventService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Event>>> GetAllEvents()
    {
        var events = await _eventService.GetAllEvents();
        return Ok(events);
    }

    [HttpGet("{eventName}")]
    public async Task<ActionResult<Event>> GetEventByName(string eventName)
    {
        var ev = await _eventService.GetEventByName(eventName);
        if(ev == null)
        {
            return NotFound();
        }
        return Ok(ev);
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<Event>> CreateEvent([FromBody] EventDto eventDto)
    {
        try
        {
            var createdEvent = await _eventService.CreateEvent(eventDto);
            return CreatedAtAction(nameof(GetEventByName), new { eventName = createdEvent.Title }, createdEvent);
        }
        catch(Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{eventName}")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<Event>> UpdateEvent(string eventName, [FromBody] EventDto eventDto)
    {
        try
        {
            var updatedEvent = await _eventService.UpdateEvent(eventName, eventDto);
            if(updatedEvent == null)
            {
                return NotFound();
            }
            return Ok(updatedEvent);
        }
        catch(NotImplementedException nie)
        {
            return StatusCode(501, nie.Message);
        }
        catch(Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{eventName}")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<Event>> DeleteEvent(string eventName)
    {
        try
        {
            var deletedEvent = await _eventService.DeleteEvent(eventName);
            return Ok(deletedEvent);
        }
        catch(Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("category/{category}")]
    public async Task<ActionResult<IEnumerable<Event>>> GetEventsByCategory(string category)
    {
        try
        {
            var events = await _eventService.GetEventsByCategoryAsync(category);
            return Ok(events);
        }
        catch(ArgumentException ae)
        {
            return BadRequest(ae.Message);
        }
        catch(Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("dateRange")]
    public async Task<ActionResult<IEnumerable<Event>>> GetEventsByDateRange([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
    {
        try
        {
            var events = await _eventService.GetEventsByDateRangeAsync(startDate, endDate);
            return Ok(events);
        }
        catch(Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}