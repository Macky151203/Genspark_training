namespace BookingSystem.Interfaces;
//start building event service crud and then craete category creating  in it as well
using BookingSystem.Models;
using BookingSystem.Models.DTOs;
public interface IEventService
{
    Task<Event> CreateEvent(EventDto eventDto);
    Task<Event> GetEventByName(string eventName);
    Task<IEnumerable<Event>> GetAllEvents();
    Task<Event> UpdateEvent(string eventName, EventDto eventDto);
    Task<Event> DeleteEvent(string eventName);
    Task<IEnumerable<Event>> GetEventsByCategoryAsync(string category);
    Task<IEnumerable<Event>> GetEventsByDateRangeAsync(DateTime startDate, DateTime endDate);
}