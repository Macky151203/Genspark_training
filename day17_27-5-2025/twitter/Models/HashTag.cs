namespace Twitter.Models;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class HashTag
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    

}