using System;
using System.IO;

class FileManager
{
    private static FileManager? instance;
    private string filePath = "";
    private bool isOpen = false;

    private FileManager() { }

    public static FileManager GetInstance()
    {
        if (instance == null)
        {
            instance = new FileManager();
        }
        return instance;
    }

    public void Open(string path)
    {
        if (!isOpen)
        {
            filePath = path;
            isOpen = true;
        }
    }

    public void Write(string content)
    {
        if (isOpen && !string.IsNullOrEmpty(filePath))
        {
            using (var writer = new StreamWriter(filePath, append: true))
            {
                writer.WriteLine(content);
            }
        }
    }

    public void ReadAll()
    {
        if (isOpen && !string.IsNullOrEmpty(filePath) && File.Exists(filePath))
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
    }

    public void Close()
    {
        isOpen = false;
    }
}

class Program
{
    static void Main(string[] args)
    {
        string path = "data.txt";
        var fileManager = FileManager.GetInstance();

        fileManager.Open(path);

        fileManager.Write("Hello, Singleton File!");
        fileManager.Write("Writing another line.");

        Console.WriteLine("File contents:");
        fileManager.ReadAll();

        fileManager.Close();
    }
}