using Microsoft.AspNetCore.Mvc;
using BookingSystem.Models;
using BookingSystem.Models.DTOs;
using BookingSystem.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;

namespace BookingSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;

        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }

      
        [HttpPost("register")]
        public async Task<ActionResult<Admin>> RegisterAdmin(AdminDto adminDto)
        {
            var admin = await _adminService.RegisterAdmin(adminDto);
            if (admin == null)
            {
                return BadRequest("Failed to register admin.");
            }
            return CreatedAtAction(nameof(GetAdminByName), new { email = admin.Email }, admin);
        }

       
        [HttpGet("{email}")]
        public async Task<ActionResult<Admin>> GetAdminByName(string name)
        {
            try
            {
                var admin = await _adminService.GetAdminByName(name);
                return Ok(admin);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }


        [HttpGet]
        [Authorize(Roles="Admin")]
        public async Task<ActionResult<IEnumerable<Admin>>> GetAllAdmins()
        {
            try
            {
                var admins = await _adminService.GetAllAdmins();
                return Ok(admins);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
