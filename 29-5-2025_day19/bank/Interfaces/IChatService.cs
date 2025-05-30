namespace Bank.Interfaces;

public interface IChatService
{
    public Task<string> SendMessageAsync(string prompt);
}