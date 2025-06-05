using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using FirstApi.Misc;

[ApiController]
[Route("api/[controller]")]
public class FileController : ControllerBase
{
    private readonly IWebHostEnvironment _env;
    private readonly IHubContext<NotificationHub> _hubContext;

    public FileController(IWebHostEnvironment env, IHubContext<NotificationHub> hubContext)
    {
        _env = env;
        _hubContext = hubContext;
    }

    [HttpPost("upload")]
    [Authorize(Roles = "Doctor")]
    public async Task<IActionResult> UploadDocument(IFormFile file)
    {
        if (file == null || file.Length == 0)
            return BadRequest("File not selected");

        var uploadPath = Path.Combine(_env.WebRootPath ?? Path.Combine(Directory.GetCurrentDirectory(), "wwwroot"), "uploads");
        Directory.CreateDirectory(uploadPath);

        var filePath = Path.Combine(uploadPath, file.FileName);

        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }

        
        await _hubContext.Clients.All.SendAsync("Doctor", "Uploaded new document");
        return Ok(new { Message = "File uploaded", FileName = file.FileName });
    }

    [HttpGet("download/{fileName}")]
    [Authorize(Roles="Doctor,Patient")]
    public IActionResult DownloadDocument(string fileName)
    {
        var uploadPath = Path.Combine(_env.WebRootPath ?? Path.Combine(Directory.GetCurrentDirectory(), "wwwroot"), "uploads");
        var filePath = Path.Combine(uploadPath, fileName);

        if (!System.IO.File.Exists(filePath))
            return NotFound("File not found");

        var bytes = System.IO.File.ReadAllBytes(filePath);
        return File(bytes, "application/octet-stream", fileName);
    }
}
