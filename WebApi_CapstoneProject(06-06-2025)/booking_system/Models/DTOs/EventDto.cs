namespace BookingSystem.Models.DTOs;

public class EventDto
{

    public string Title { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public DateTime Date { get; set; }
    public int Price { get; set; }

    public string Address { get; set; }

    public string City { get; set; }

    public string CategoryName { get; set; }
}