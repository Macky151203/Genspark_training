namespace Twitter.Models;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
public class Follow
{
    public int Id { get; set; }
    public int FollowerId { get; set; }
    public int FollowingId { get; set; }

    [ForeignKey("FollowingId")]  
    public User? Followers { get; set; } 

    [ForeignKey("FollowerId")]
    public User? Following { get; set; }    

}