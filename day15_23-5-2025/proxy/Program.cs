using Proxy.Interfaces;
using Proxy.Models;
using Proxy.Services;



class Program
{
    static void Main(string[] args)
    {
        string filePath = "data.txt";
        while (true)
        {
            Console.WriteLine("\n--- Proxy File System ---");
            Console.Write("Enter username (or 'exit' to quit): ");
            string username = Console.ReadLine() ?? "";
            if (username.ToLower() == "exit") break;

            Console.Write("Enter role (admin/user/guest): ");
            string role = Console.ReadLine() ?? "";

            User user = new User(username, role);
            ProxyFile proxyFile = new ProxyFile(user, filePath);

            Console.WriteLine("Choose operation: 1. Read  2. Write  3. Exit");
            string choice = Console.ReadLine() ?? "";

            if (choice == "1")
            {
                proxyFile.read();
            }
            else if (choice == "2")
            {
                Console.Write("Enter content to write: ");
                string content = Console.ReadLine() ?? "";
                proxyFile.write(content);
            }
            else if (choice == "3")
            {
                break;
            }
            else
            {
                Console.WriteLine("Invalid option.");
            }
        }
        Console.WriteLine("Exiting program.");
    }
}