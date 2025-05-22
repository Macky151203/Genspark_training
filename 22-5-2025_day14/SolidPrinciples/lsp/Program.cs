interface IStudent
{
    void Attendclass();
}

interface IOnlineStudent : IStudent
{
    void AttendOnlineClass();
}

class Student : IStudent
{
    public void Attendclass()
    {
        Console.WriteLine("Attending class");
    }
}

class OnlineStudent : IOnlineStudent
{
    public void Attendclass()
    {
        Console.WriteLine("Attending class");
    }
    public void AttendOnlineClass()
    {
        Console.WriteLine("Attending online class");
    }
}

class Program
{
    static void Main(string[] args)
    {
        // LSP
        IStudent student = new Student();
        student.Attendclass();

        // IOnlineStudent onlineStudent = new OnlineStudent();
        // onlineStudent.Attendclass();
        // onlineStudent.AttendOnlineClass();
    }
}