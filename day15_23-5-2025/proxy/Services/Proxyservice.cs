namespace Proxy.Services;

using Proxy.Interfaces;
using Proxy.Models;
using Proxy.Services;

public class ProxyFile : IFile
{
    private User user;
    private Files file;
    public ProxyFile(User user, string path)
    {
        this.user = user;
        file = new Files(path);
    }
    public void read()
    {
        switch (user.role)
        {
            case "admin":
                Console.WriteLine("Admin access granted. Reading file...");
                file.read();
                break;
            case "user":
                Console.WriteLine("User access, Reading file metadata");
                var fileInfo = new System.IO.FileInfo(file.filePath); 
                Console.WriteLine($"File: {fileInfo.Name}");
                Console.WriteLine($"Size: {fileInfo.Length} bytes");
                Console.WriteLine($"Last Modified: {fileInfo.LastWriteTime}");
                break;
            case "guest":
                Console.WriteLine("Guest access denied for now.");
                break;
            default:
                Console.WriteLine("Role not recognized.");
                break;
        }
    }
    public void write(string content)
    {
        switch (user.role)
        {
            case "admin":
                Console.WriteLine("Admin access granted. Writing to file...");
                file.write(content);
                break;
            case "user":
                Console.WriteLine("Write access denied for user.");
                break;
            case "guest":
                Console.WriteLine("Guest access denied for now.");
                break;
            default:
                Console.WriteLine("Role not recognized.");
                break;
        }
    }
}