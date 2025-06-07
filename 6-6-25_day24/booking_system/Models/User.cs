namespace BookingSystem.Models;

public class User
{
    public string Email { get; set; } = string.Empty; // Primary Key

    public string Name { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;  // "Admin" or "Customer"

    public byte[]? Password { get; set; }
    public byte[]? HashKey { get; set; }

    // Navigational properties
    public Admin? Admin { get; set; }
    public Customer? Customer { get; set; }
}

