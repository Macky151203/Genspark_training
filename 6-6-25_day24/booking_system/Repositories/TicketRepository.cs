namespace BookingSystem.Repositories;
using Microsoft.EntityFrameworkCore;
using BookingSystem.Models;
using BookingSystem.Contexts;

public class TicketRepository : Repository<int, Ticket>
{
    private readonly BookingDbContext _bookingdbcontext;

    public TicketRepository(BookingDbContext context) : base(context)
    {
        _bookingdbcontext = context;
    }

    public override async Task<Ticket> Get(int key)
    {
        var ticket = await _bookingdbcontext.Tickets.SingleOrDefaultAsync(a => a.Id == key);

        return ticket ;
    }

    public override async Task<IEnumerable<Ticket>> GetAll()
    {
        var  tickets=  _bookingdbcontext.Tickets;
        if (tickets.Count()==0)
            throw new Exception("No Admin in the database");

        return await tickets.ToListAsync();
    }
}
