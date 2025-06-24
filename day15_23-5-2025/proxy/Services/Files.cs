namespace Proxy.Services;
using Proxy.Interfaces;
public class Files : IFile
{
    public string filePath { get; set; } = "";
    public Files(string path)
    {
        this.filePath = path;
    }
    public void read()
    {
        using (var reader = new StreamReader(filePath))
        {
            string? line;
            while ((line = reader.ReadLine()) != null)
            {
                Console.WriteLine(line);
            }
        }
    }
    public void write(string content)
    {
        Console.WriteLine("Writing to file...");
        using (var writer = new StreamWriter(filePath, append: true))
        {
            writer.WriteLine(content);
        }
    }
}