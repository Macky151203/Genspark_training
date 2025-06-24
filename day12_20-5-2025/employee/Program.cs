using System;
using System.Collections.Generic;
using System.Linq;

class Employee : IComparable<Employee>
{
    int id, age;
    string name;
    double salary;
    
    public Employee()
    {
    }
    
    public Employee(int id, int age, string name, double salary)
    {
        this.id = id;
        this.age = age;
        this.name = name;
        this.salary = salary;
    }
    
    public void TakeEmployeeDetailsFromUser()
    {
        Console.WriteLine("Please enter the employee ID");
        id = Convert.ToInt32(Console.ReadLine());
        Console.WriteLine("Please enter the employee name");
        name = Console.ReadLine();
        Console.WriteLine("Please enter the employee age");
        age = Convert.ToInt32(Console.ReadLine());
        Console.WriteLine("Please enter the employee salary");
        salary = Convert.ToDouble(Console.ReadLine());
    }
    
    public override string ToString()
    {
        return "Employee ID : " + id + "\nName : " + name + "\nAge : " + age + "\nSalary : " + salary;
    }
    
    public int CompareTo(Employee other)
    {
        if (other == null)
            return 1;
        return this.salary.CompareTo(other.salary);
    }
    
    public int Id { get => id; set => id = value; }
    public int Age { get => age; set => age = value; }
    public string Name { get => name; set => name = value; }
    public double Salary { get => salary; set => salary = value; }
}

class Program
{
    public static void Main(string[] args)
    {
        Dictionary<int, Employee> employeeDict = new Dictionary<int, Employee>();
        int choice;
        do
        {
            Console.WriteLine("Enter 1 for adding employee, 2 for sorting in salary order, 3 for find by id, 4 for find by name, 5 for elder employees by age and 6 for exit ");
            choice = Convert.ToInt32(Console.ReadLine());
            switch (choice)
            {
                case 1:
                    Console.WriteLine("Enter number of employees to add-");
                    int n = Convert.ToInt32(Console.ReadLine());
                    for (int i = 0; i < n; i++)
                    {
                        Console.WriteLine($"Enter details for employee {i + 1}:");
                        Employee emp = new Employee();
                        emp.TakeEmployeeDetailsFromUser();
                        if (employeeDict.ContainsKey(emp.Id))
                        {
                            Console.WriteLine("Employee ID already exists.");
                            i--;
                        }
                        else
                        {
                            employeeDict.Add(emp.Id, emp);
                        }
                    }
                    break;
                case 2:
                    {
                        List<Employee> employeeList = employeeDict.Values.ToList();
                        employeeList.Sort();
                        Console.WriteLine("Employees sorted by salary:");
                        foreach (var emp in employeeList)
                        {
                            Console.WriteLine(emp.ToString());
                        }
                    }
                    break;
                case 3:
                    {
                        Console.WriteLine("Enter the employee ID to search:");
                        int searchId = Convert.ToInt32(Console.ReadLine());
                        List<Employee> employeeList = employeeDict.Values.ToList();
                        var result = employeeList.Where(e => e.Id == searchId).FirstOrDefault();
                        if (result != null)
                        {
                            Console.WriteLine("Employee found:");
                            Console.WriteLine(result.ToString());
                        }
                        else
                        {
                            Console.WriteLine("Employee not found.");
                        }
                    }
                    break;
                case 4:
                    {
                        Console.WriteLine("Enter the employee name to search:");
                        string name = Console.ReadLine();
                        List<Employee> employeeList = employeeDict.Values.ToList();
                        var result1 = employeeList.Where(e => e.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
                        if (result1.Any())
                        {
                            Console.WriteLine("Employees found:");
                            foreach (var emp in result1)
                            {
                                Console.WriteLine(emp.ToString());
                            }
                        }
                        else
                        {
                            Console.WriteLine("Employee not found.");
                        }
                    }
                    break;
                case 5:
                    {
                        Console.WriteLine("Enter the age to find elder employees:");
                        int age = Convert.ToInt32(Console.ReadLine());
                        List<Employee> employeeList = employeeDict.Values.ToList();
                        var elderEmployees = employeeList.Where(e => e.Age > age);
                        if (elderEmployees.Any())
                        {
                            Console.WriteLine("Elder employees found:");
                            foreach (var emp in elderEmployees)
                            {
                                Console.WriteLine(emp.ToString());
                            }
                        }
                        else
                        {
                            Console.WriteLine("No elder employees found.");
                        }
                    }
                    break;
                case 6:
                    Console.WriteLine("Exiting the program");
                    choice = 0;
                    break;
                default:
                    Console.WriteLine("Invalid choice");
                    break;
            }
        } while (choice != 0);
    }
}