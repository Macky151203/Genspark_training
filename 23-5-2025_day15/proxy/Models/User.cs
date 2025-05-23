namespace Proxy.Models;
public class User
{
    private string name { get; set; } = "";
    public string role { get; set; } = "";
    public User(string name, string role)
    {
        this.name = name;
        this.role = role;
    }
}