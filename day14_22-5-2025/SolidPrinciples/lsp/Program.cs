interface IStudent
{
    void AttendClass();
}

class Student : IStudent
{
    public virtual void AttendClass()
    {
        Console.WriteLine("Attending class");
    }
}

class OnlineStudent : Student
{
    public override void  AttendClass()
    {
        base.AttendClass();
        Console.WriteLine("in online mode");
    }
}

class Program
{
    static void Main(string[] args)
    {
        // LSP
        IStudent student1 = new Student();
        IStudent student2 = new OnlineStudent();

        student1.AttendClass();
        student2.AttendClass();
        
        
    }
}
