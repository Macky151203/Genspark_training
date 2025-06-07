
using BookingSystem.Interfaces;
using BookingSystem.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
using BookingSystem.Models;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;


[ApiController]
[Route("/api/[controller]")]
public class AuthenticationController : ControllerBase
{
    private readonly BookingSystem.Interfaces.IAuthenticationService _authenticationService;
    private readonly ILogger<AuthenticationController> _logger;

    public AuthenticationController(BookingSystem.Interfaces.IAuthenticationService authenticationService, ILogger<AuthenticationController> logger)
    {
        _authenticationService = authenticationService;
        _logger = logger;
    }
    [HttpPost]
    public async Task<ActionResult<UserLoginResponse>> UserLogin(UserLoginRequest loginRequest)
    {

        try
        {
            var result = await _authenticationService.Login(loginRequest);
            return Ok(result);
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return Unauthorized(e.Message);
        }

    }



}