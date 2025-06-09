namespace BookingSystem.Services;

using BookingSystem.Models;
using BookingSystem.Repositories;
using BookingSystem.Interfaces;
using BookingSystem.Models.DTOs;

public class EventService : IEventService
{
    private readonly IRepository<string, Event> _eventRepository;
    private readonly IRepository<string, Category> _categoryRepository;

    public EventService(IRepository<string, Event> eventRepository, IRepository<string, Category> categoryRepository)
    {
        _eventRepository = eventRepository;
        _categoryRepository = categoryRepository;
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
        //make an event and return
        return await _eventRepository.Add(eventDto);
    }

    public async Task<Event?> UpdateEvent(string eventName, EventDto eventDto)
    {
        //make an event and return
        return await _eventRepository.Update(eventName, eventDto);
    }

    public async Task<Event> DeleteEvent(string eventName)
    {
        return await _eventRepository.Delete(eventName);
    }
    public async Task<IEnumerable<Event>> GetEventsByCategoryAsync(string category)
    {
        return null;
    }
    public async Task<IEnumerable<Event>> GetEventsByDateRangeAsync(DateTime startDate, DateTime endDate)
    {
        return null;
    }
}