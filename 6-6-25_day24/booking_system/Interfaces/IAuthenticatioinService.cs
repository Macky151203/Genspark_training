namespace BookingSystem.Interfaces;

using BookingSystem.Models;
using BookingSystem.Models.DTOs;

public interface IAuthenticationService
{
    public Task<UserLoginResponse> Login(UserLoginRequest user);
}