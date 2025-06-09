namespace BookingSystem.Models.DTOs;

public class EventDto
{

    public string Title { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public DateTime Date { get; set; }

    public string CategoryName { get; set; }
}