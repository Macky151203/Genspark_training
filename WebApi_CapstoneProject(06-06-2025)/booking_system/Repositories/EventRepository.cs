namespace BookingSystem.Repositories;
using Microsoft.EntityFrameworkCore;
using BookingSystem.Models;
using BookingSystem.Contexts;

public class EventRepository : Repository<string, Event>
{
    private readonly BookingDbContext _bookingdbcontext;

    public EventRepository(BookingDbContext context) : base(context)
    {
        _bookingdbcontext = context;
    }

    public override async Task<Event> Get(string key)
    {
        var events = await _bookingdbcontext.Events.SingleOrDefaultAsync(c => c.Title == key);
        if (events == null)
            throw new Exception("No Event with the given name");

        return events ;
    }

    public override async Task<IEnumerable<Event>> GetAll()
    {
        var events = await _bookingdbcontext.Events.Include(e => e.Category).ToListAsync();
        if (!events.Any())
            throw new Exception("No Events in the database");

        return events;
    }
}