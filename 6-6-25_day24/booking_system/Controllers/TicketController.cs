namespace BookingSystem.Controllers;

using Microsoft.AspNetCore.Mvc;
using BookingSystem.Models;
using BookingSystem.Models.DTOs;
using BookingSystem.Interfaces;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using PdfSharpCore.Pdf;
using PdfSharpCore.Drawing;
using System.IO;

[Route("api/[controller]")]
[ApiController]

public class TicketController : ControllerBase
{
    private readonly ITicketService _ticketService;
    private readonly ILogger<AuthenticationController> _logger;

    public TicketController(ITicketService ticketService, ILogger<AuthenticationController> logger)
    {
        _ticketService = ticketService;
        _logger = logger;
    }

    [HttpPost("book")]
    [Authorize(Roles = "Customer")]
    public async Task<ActionResult<Ticket>> BookTicket([FromBody] TicketDto ticketDto)
    {
        try
        {
            var ticket = await _ticketService.BookTicket(ticketDto);

            // 1. Prepare ticket details text
            var ticketContent = $"Ticket ID: {ticket.Id}\n" +
                                $"Event Id: {ticket.EventId}\n" +
                                $"Qty: {ticket.Quantity}\n" +
                                $"total: {ticket.Total}\n" +
                                $"CustomerEmail: {ticket.CustomerEmail}\n" +
                                $"Booked At: {DateTime.UtcNow}";

            // 2. Create a PDF document
            using var stream = new MemoryStream();
            var document = new PdfDocument();
            var page = document.AddPage();
            var gfx = XGraphics.FromPdfPage(page);
            var font = new XFont("Verdana", 12, XFontStyle.Regular);
            gfx.DrawString(ticketContent, font, XBrushes.Black,
    new XRect(40, 40, page.Width - 80, page.Height - 80),
    XStringFormats.TopLeft);


            document.Save(stream, false); // false = leave stream open
            stream.Position = 0;

            var fileName = $"Ticket_{ticket.Id}.pdf";

            // Optional: Save to Desktop
            var desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            var desktopFilePath = Path.Combine(desktopPath, fileName);
            await System.IO.File.WriteAllBytesAsync(desktopFilePath, stream.ToArray());

            // Return file to client
            return File(stream.ToArray(), "application/pdf", fileName);
            // 2. File path - save to desktop
            // var desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            // var fileName = $"Ticket_{ticket.Id}_{DateTime.UtcNow.Ticks}.txt";
            // var filePath = Path.Combine(desktopPath, fileName);

            // await System.IO.File.WriteAllTextAsync(filePath, ticketContent);

            // var fileBytes = await System.IO.File.ReadAllBytesAsync(filePath);

            // // 3. Return the file as a download response
            // return File(fileBytes, "text/plain", fileName);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error booking ticket for event {EventName}", ticketDto.EventName);
            return BadRequest(ex.Message);
        }
    }
    [HttpGet("{id}")]
    [Authorize(Roles = "Customer")]
    public async Task<ActionResult<Ticket>> GetTicketById(int id)
    {
        try
        {
            var ticket = await _ticketService.GetTicketById(id);
            if (ticket == null)
            {
                _logger.LogWarning("Ticket with ID {Id} not found", id);
                return NotFound();
            }
            return Ok(ticket);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving ticket with ID {Id}", id);
            return NotFound(ex.Message);
        }
    }
    [HttpDelete("{id}/cancel")]
    [Authorize(Roles = "Customer")]
    public async Task<ActionResult<Ticket>> CancelTicketById(int id)
    {
        try
        {
            var ticket = await _ticketService.CancelTicketById(id);
            return Ok(ticket);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error canceling ticket with ID {Id}", id);
            return NotFound(ex.Message);
        }
    }

}