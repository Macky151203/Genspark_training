namespace Twitter.Models;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
public class Tweet
{
    public int Id { get; set; }
    public string Content { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public int UserId { get; set; }

    [ForeignKey("UserId")]
    public User? User { get; set; }

    public ICollection<Like>? Likes { get; set; }
    public ICollection<HashTag>? Hashtags { get; set; }
}