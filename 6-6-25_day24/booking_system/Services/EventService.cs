namespace BookingSystem.Services;

using BookingSystem.Models;
using BookingSystem.Repositories;
using BookingSystem.Interfaces;
using BookingSystem.Models.DTOs;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

public class EventService : IEventService
{
    private readonly IRepository<string, Event> _eventRepository;
    private readonly IRepository<string, Category> _categoryRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public EventService(IRepository<string, Event> eventRepository, IRepository<string, Category> categoryRepository, IHttpContextAccessor httpContextAccessor)
    {
        _eventRepository = eventRepository;
        _categoryRepository = categoryRepository;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<IEnumerable<Event>> GetAllEvents()
    {
        return await _eventRepository.GetAll();
    }

    public async Task<Event?> GetEventByName(string EventName)
    {
        return await _eventRepository.Get(EventName);
    }

    public async Task<Event> CreateEvent(EventDto eventDto)
    {
        var existingCategory = await _categoryRepository.Get(eventDto.CategoryName);
        string? username = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (existingCategory == null)
        {
            var newCategory = new Category { Name = eventDto.CategoryName };
            newCategory = await _categoryRepository.Add(newCategory);
            var newEvent = new Event
            {
                Title = eventDto.Title,
                Description = eventDto.Description,
                Date = eventDto.Date,
                CategoryId = newCategory.Id,
                CreatorEmail = username ?? string.Empty,
                Price = eventDto.Price
            };
            return await _eventRepository.Add(newEvent);
        }

        var newEvent2 = new Event
        {
            Title = eventDto.Title,
            Description = eventDto.Description,
            Date = eventDto.Date,
            CategoryId = existingCategory.Id,
            CreatorEmail = username ?? string.Empty,
            Price = eventDto.Price

        };
        return await _eventRepository.Add(newEvent2);


    }

    public async Task<Event?> UpdateEvent(string eventName, EventDto eventDto)
    {
        var existingEvent = await _eventRepository.Get(eventName);
        if (existingEvent == null)
        {
            throw new NotImplementedException($"Event '{eventName}' does not exist.");
        }

        var existingCategory = await _categoryRepository.Get(eventDto.CategoryName);
        if (existingCategory == null)
        {
            throw new ArgumentException($"Category '{eventDto.CategoryName}' does not exist.");
        }

        existingEvent.Title = eventDto.Title;
        existingEvent.Description = eventDto.Description;
        existingEvent.Date = eventDto.Date;
        existingEvent.CategoryId = existingCategory.Id;
        existingEvent.Price = eventDto.Price;
        existingEvent.CreatorEmail = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty;


        return await _eventRepository.Update(eventName,existingEvent);
    }

    public async Task<Event> DeleteEvent(string eventName)
    {
        return await _eventRepository.Delete(eventName);
    }
    public async Task<IEnumerable<Event>> GetEventsByCategoryAsync(string category)
    {
        var existingCategory = await _categoryRepository.Get(category);
        if (existingCategory == null)
        {
            throw new ArgumentException($"Category '{category}' does not exist.");
        }
        var categoryid= existingCategory.Id;
        var allEvents = await _eventRepository.GetAll();
        return allEvents.Where(e => e.CategoryId == categoryid);
    }
    public async Task<IEnumerable<Event>> GetEventsByDateRangeAsync(DateTime startDate, DateTime endDate)
    {
        var allEvents = await _eventRepository.GetAll();
        return allEvents.Where(e => e.Date >= startDate && e.Date <= endDate);
    }
}