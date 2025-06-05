namespace FirstApi.Models;


public class Hr
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Department { get; set; }
}