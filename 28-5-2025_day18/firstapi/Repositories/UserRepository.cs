namespace FirstApi.Repositories;

using FirstApi.Interfaces;
using FirstApi.Contexts;
using FirstApi.Models;
using Microsoft.EntityFrameworkCore;
public class UserRepository : Repository<int, User>
{
    public UserRepository(ClinicContext clinicContext) : base(clinicContext)
    {
    }

    public override async Task<User> Get(int key)
    {
        var doctorspec = await _clinicContext.Users.SingleOrDefaultAsync(p => p.UserId == key);

        return doctorspec ?? throw new Exception("No User with the given ID");
    }

    public override async Task<IEnumerable<User>> GetAll()
    {
        var doctorsspec = _clinicContext.Users;
        if (doctorsspec.Count() == 0)
            throw new Exception("No User in the database");
        return (await doctorsspec.ToListAsync());
    }

    
}