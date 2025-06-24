namespace Twitter.Contexts;
using Microsoft.EntityFrameworkCore;
using Twitter.Models;
public class TwitterContext : DbContext
{
    public TwitterContext(DbContextOptions<TwitterContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Tweet> Tweets { get; set; }
    public DbSet<Like> Likes { get; set; }
    public DbSet<Follow> Follows { get; set; }
    public DbSet<HashTag> HashTags { get; set; }

}