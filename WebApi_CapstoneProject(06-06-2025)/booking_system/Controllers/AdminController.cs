using Microsoft.AspNetCore.Mvc;
using BookingSystem.Models;
using BookingSystem.Models.DTOs;
using BookingSystem.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;

namespace BookingSystem.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;
        private readonly ILogger<AdminController> _logger;

        public AdminController(IAdminService adminService, ILogger<AdminController> logger)
        {
            _adminService = adminService;
            _logger = logger;
        }


        [HttpPost("register")]
        public async Task<ActionResult<Admin>> RegisterAdmin(AdminDto adminDto)
        {
            var admin = await _adminService.RegisterAdmin(adminDto);
            if (admin == null)
            {
                _logger.LogError("Failed to register admin with email {Email}", adminDto.Email);
                return BadRequest("Failed to register admin.");
            }
            return CreatedAtAction(nameof(GetAdminByName), new { name = admin.Name }, admin);
        }


        [HttpGet("{name}")]
        public async Task<ActionResult<Admin>> GetAdminByName(string name)
        {
            try
            {
                var admin = await _adminService.GetAdminByName(name);
                if (admin == null)
                {
                    _logger.LogWarning("Admin with name {Name} not found", name);
                    return NotFound();
                }
                return Ok(admin);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to retrieve admin with name {Name}", name);
                return NotFound(ex.Message);
            }
        }


        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<IEnumerable<Admin>>> GetAllAdmins()
        {
            try
            {
                var admins = await _adminService.GetAllAdmins();
                return Ok(admins);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to retrieve all admins");
                return BadRequest(ex.Message);
            }
        }
    }
}
