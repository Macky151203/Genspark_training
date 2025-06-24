namespace Twitter.Models;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
public class User
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;


    public ICollection<Tweet>? Tweets { get; set; }
    public ICollection<Like>? Likes { get; set; }

    [InverseProperty("Following")]
    public ICollection<Follow>? Followers { get; set; }

    [InverseProperty("Followers")]
    public ICollection<Follow>? Following { get; set; }
}