namespace Bank.Services;

using Bank.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.AI;
using OllamaSharp;
using OllamaSharp.Models;
using Bank.Interfaces;

public class ChatService : IChatService
{
    private readonly IChatClient _chatClient;
    private List<ChatMessage> chatHistory = new List<ChatMessage>{
        new ChatMessage (ChatRole.System ," You are an assistant for helping in FAQs for a banking system where user can withdraw,deposit or check the balance in the bank account.Here are some details for answering FAQs. 1.To create a account you need Name, Date of birth, Type of account(Savings or Current), Aadhar number and mobile number. 2.Once you create account, your account number will be sent through sms to your mobile number. 3.To withdraw or deposit, you need to enter your account number along with the amount. 4.If you deactivate your account the any ongoing transactions will be suspended. " ),
    };


    public ChatService()
    {
        _chatClient = new OllamaApiClient(new Uri("http://localhost:11434"), "qwen2.5:1.5b");
    }

    public async Task<string> SendMessageAsync(string prompt)
    {
        try
        {
            chatHistory.Add(new ChatMessage ( ChatRole.User, prompt ));


            Console.WriteLine("AI Response:");
            var response = "";
            await foreach (ChatResponseUpdate item in
                _chatClient.GetStreamingResponseAsync(chatHistory))
            {
                // Console.Write(item.Text);
                response += item.Text;
            }
            chatHistory.Add(new ChatMessage(ChatRole.Assistant, response));
            return response;
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error: {e.Message}");
            return "An error occurred while processing your request.";
        }
    }
}