namespace BookingSystem.Repositories;
using Microsoft.EntityFrameworkCore;
using BookingSystem.Models;
using BookingSystem.Contexts;

public class AdminRepository : Repository<string, Admin>
{
    private readonly BookingDbContext _bookingdbcontext;

    public AdminRepository(BookingDbContext context) : base(context)
    {
        _bookingdbcontext = context;
    }

    public override async Task<Admin> Get(string key)
    {
        var admin = await _bookingdbcontext.Admins.SingleOrDefaultAsync(a => a.Name == key);

        return admin ?? throw new Exception("No Admin with the given ID");
    }

    public override async Task<IEnumerable<Admin>> GetAll()
    {
        var admins =  _bookingdbcontext.Admins;
        if (admins.Count()==0)
            throw new Exception("No Admin in the database");

        return await admins.ToListAsync();
    }
}
