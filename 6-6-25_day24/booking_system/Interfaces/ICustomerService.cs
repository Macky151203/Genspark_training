namespace BookingSystem.Interfaces;
using BookingSystem.Models;
using BookingSystem.Models.DTOs ;

public interface ICustomerService
{
    Task<Customer> RegisterCustomer(CustomerDto customerDto);
    Task<Customer> GetCustomerByEmail(string email);
    Task<IEnumerable<Customer>> GetAllCustomers();
}
