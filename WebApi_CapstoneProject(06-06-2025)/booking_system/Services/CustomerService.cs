namespace BookingSystem.Services;

using BookingSystem.Models;
using BookingSystem.Repositories;
using BookingSystem.Interfaces;
using BookingSystem.Models.DTOs;

public class CustomerService : ICustomerService
{
    private readonly IRepository<string, Customer> _customerRepository;
    private readonly IRepository<string, User> _userRepository;
    private readonly IEncryptionService _encryptionService;

    public CustomerService(IRepository<string, Customer> customerRepository, IRepository<string, User> userRepository, IEncryptionService encryptionService)
    {
        _customerRepository = customerRepository;
        _userRepository = userRepository;
        _encryptionService = encryptionService;
    }

    public async Task<Customer> RegisterCustomer(CustomerDto customerDto)
    {
        try
        {
            var encrypteddata = await _encryptionService.EncryptData(new EncryptModel
            {
                Data = customerDto.Password
            });

            var user = new User
            {
                Name = customerDto.Name,
                Email = customerDto.Email,
                Password = encrypteddata.EncryptedData??"",
                Role = "Customer"
            };

            user = await _userRepository.Add(user);

            var customer = new Customer
            {
                Name = customerDto.Name,
                Email = customerDto.Email,
                PhoneNumber = customerDto.PhoneNumber,
                Address = customerDto.Address

            };

            var newCustomer = await _customerRepository.Add(customer);
            return newCustomer;
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error - " + ex.Message);
            return null;
        }
    }

    public async Task<Customer> GetCustomerByName(string name)
    {
        return await _customerRepository.Get(name);
    }

    public async Task<IEnumerable<Customer>> GetAllCustomers()
    {
        return await _customerRepository.GetAll();
    }
}
