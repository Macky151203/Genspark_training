namespace BookingSystem.Models;
public class Category
{
    public int Id { get; set; }

    public string Name { get; set; }

    // Navigation property: Category has many events (1:N)
    public ICollection<Event>? Events { get; set; }
}
