
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
    [HttpPost("logout")]
    [Authorize]
    public async Task<IActionResult> Logout(string email,string token)
    {
        try
        {
            if (string.IsNullOrEmpty(email))
            {
                return BadRequest("User not authenticated");
            }
            await _authenticationService.Logout(email,token);
            return Ok("Logged out successfully");
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return Unauthorized(e.Message);
        }
    }

    [HttpPost("refresh")]
    [Authorize]
    public async Task<ActionResult<UserLoginResponse>> RefreshToken(string email,string refreshToken)
    {
        try
        {
            if (string.IsNullOrEmpty(email))
            {
                return BadRequest("User not authenticated");
            }
            if (string.IsNullOrEmpty(refreshToken))
            {
                return BadRequest("Refresh token is required");
            }
            var result = await _authenticationService.RefreshToken(email, refreshToken);
            return Ok(result);
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return Unauthorized(e.Message);
        }
    }



}