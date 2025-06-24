using Microsoft.AspNetCore.Mvc;
using Bank.Models;
using Bank.Interfaces;

[ApiController]
[Route("api/[controller]")]
public class ChatController : ControllerBase
{
    private readonly IChatService _chatService;

    public ChatController(IChatService chatService)
    {
        _chatService = chatService;
    }

    [HttpPost("SendMessage")]
    public async Task<IActionResult> SendMessage([FromBody] Message message)
    {
        try
        {
            var response = await _chatService.SendMessageAsync(message.prompt);
            Console.WriteLine($"Response from model: {response}");
            return Ok(response);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    
}