using Microsoft.AspNetCore.SignalR;
namespace BookingSystem.Misc;

public class EventHub : Hub
{
    public async Task SendMessage(string user, string message)
    {
        await Clients.All.SendAsync("ReceiveMessage", user, message);
    }
}
