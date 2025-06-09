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
    private readonly ILogger<AuthenticationController> _logger;


    public EventsController(IEventService eventService, ILogger<AuthenticationController> logger)
    {
        _eventService = eventService;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Event>>> GetAllEvents()
    {
        try
        {
            var events = await _eventService.GetAllEvents();
            return Ok(events);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving events");
            return BadRequest("An error occurred while retrieving events.");
        }
    }

    [HttpGet("{eventName}")]
    public async Task<ActionResult<Event>> GetEventByName(string eventName)
    {
        try
        {
            var ev = await _eventService.GetEventByName(eventName);
            if (ev == null)
            {
                return NotFound();
            }
            return Ok(ev);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving event with name {EventName}", eventName);
            return BadRequest("An error occurred while retrieving the event.");
        }
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<Event>> CreateEvent([FromBody] EventDto eventDto)
    {
        try
        {
            var createdEvent = await _eventService.CreateEvent(eventDto);
            _logger.LogInformation("Event {EventName} created successfully", createdEvent.Title);
            return CreatedAtAction(nameof(GetEventByName), new { eventName = createdEvent.Title }, createdEvent);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating event");
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
            if (updatedEvent == null)
            {
                _logger.LogWarning("Event with name {EventName} not found for update", eventName);
                return NotFound();
            }
            return Ok(updatedEvent);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating event with name {EventName}", eventName);
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
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting event with name {EventName}", eventName);
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
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving events by category {Category}", category);
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
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving events by date range from {StartDate} to {EndDate}", startDate, endDate);
            return BadRequest(ex.Message);
        }
    }
}