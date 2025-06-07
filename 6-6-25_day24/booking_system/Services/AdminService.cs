namespace BookingSystem.Services;

using BookingSystem.Models;
using BookingSystem.Repositories;
using BookingSystem.Interfaces;
using BookingSystem.Models.DTOs;

public class AdminService : IAdminService
{
    private readonly IRepository<string, Admin> _adminRepository;
    private readonly IRepository<string, User> _userRepository;

    private readonly IEncryptionService _encryptionService;

    public AdminService(IRepository<string, Admin> adminRepository, IRepository<string, User> userRepository, IEncryptionService encryptionService)
    {
        _adminRepository = adminRepository;
        _userRepository = userRepository;
        _encryptionService = encryptionService;
    }

    public async Task<Admin> RegisterAdmin(AdminDto adminDto)
    {
        try
        {
            var encrypteddata = await _encryptionService.EncryptData(new EncryptModel
            {
                Data = adminDto.Password
            });
            var user = new User()
            {
                Name = adminDto.Name,
                Email = adminDto.Email,
                Password = encrypteddata.EncryptedData,
                Role = "Admin",
                HashKey = encrypteddata.HashKey
            };

            user = await _userRepository.Add(user);

            var admin = new Admin()
            {
                Name = adminDto.Name,
                Email = adminDto.Email
            };
            //add to user before admin
            var newadmin= await _adminRepository.Add(admin);
        

            return newadmin;
        }
        catch (Exception e)
        {
            Console.WriteLine("error-",e);
            return null;
        }
    }

    public async Task<Admin> GetAdminByEmail(string email)
    {
        return await _adminRepository.Get(email);
    }

    public async Task<IEnumerable<Admin>> GetAllAdmins()
    {
        return await _adminRepository.GetAll();
    }
}
