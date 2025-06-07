namespace BookingSystem.Interfaces;
using BookingSystem.Models;
using BookingSystem.Models.DTOs ;

public interface IAdminService
{
    Task<Admin> RegisterAdmin(AdminDto adminDto);
    Task<Admin> GetAdminByEmail(string email);
    Task<IEnumerable<Admin>> GetAllAdmins();
}
