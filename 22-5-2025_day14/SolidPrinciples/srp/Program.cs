using Srp.Services;
using Srp.Models;
using Srp.Repositories;

class Program
{
    static void Main(string[] args)
    {
        // Single Responsibility Principle

        IRepository<Student> studentRepository = new Repository<Student>();
        StudentService studentService = new StudentService(studentRepository);

        studentService.AddStudent("Alice", 20);
        studentService.AddStudent("Bob", 22);

        var students = studentService.GetAllStudents();
        foreach (var student in students)
        {
            Console.WriteLine($"{student.Id}: {student.Name}, Age: {student.Age}");
        }
    }
}