namespace Twitter.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
public class Like
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int TweetId { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [ForeignKey("UserId")]
    public User? User { get; set; }
    [ForeignKey("TweetId")]
    public Tweet? Tweet { get; set; }
}