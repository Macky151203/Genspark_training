namespace Srp.Services;
using Srp.Models;
using Srp.Repositories;
public class StudentService
{
    private readonly IRepository<Student> _studentRepository;

    public StudentService(IRepository<Student> studentRepository)
    {
        _studentRepository = studentRepository;
    }

    public void AddStudent(string name, int age)
    {
        var student = new Student(_studentRepository.GetAll().Count + 1, name, age);
        _studentRepository.Add(student);
    }

    public List<Student> GetAllStudents()
    {
        return _studentRepository.GetAll();
    }
}
