namespace Proxy.Interfaces;

public interface IFile
{
    void read();
    void write(string content);
}