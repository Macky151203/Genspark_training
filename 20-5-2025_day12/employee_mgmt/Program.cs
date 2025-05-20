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

public class Program
{
    static void Main(string[] args)
    {
        Dictionary<int, Employee> employees = new Dictionary<int, Employee>();
        bool exit = false;
        while (!exit)
        {
            Console.WriteLine("\n--- Employee Management Menu ---");
            Console.WriteLine("1. Print all employee details");
            Console.WriteLine("2. Add an employee");
            Console.WriteLine("3. Modify employee details (all except ID)");
            Console.WriteLine("4. Print an employee's details by ID");
            Console.WriteLine("5. Delete an employee by ID");
            Console.WriteLine("6. Exit");
            Console.Write("Please select an option (1-6): ");
            
            string choice = Console.ReadLine() ?? string.Empty;
            switch (choice)
            {
                case "1":
                    PrintAllEmployees(employees);
                    break;
                case "2":
                    AddEmployee(employees);
                    break;
                case "3":
                    ModifyEmployee(employees);
                    break;
                case "4":
                    PrintEmployeeById(employees);
                    break;
                case "5":
                    DeleteEmployee(employees);
                    break;
                case "6":
                    exit = true;
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please select a valid option.");
                    break;
            }
        }
        Console.WriteLine("Exiting application. Goodbye!");
    }

    static void PrintAllEmployees(Dictionary<int, Employee> employees)
    {
        if (employees.Count == 0)
        {
            Console.WriteLine("No employees found.");
            return;
        }
        Console.WriteLine("\n--- All Employee Details ---");
        foreach (var emp in employees.Values)
        {
            Console.WriteLine(emp);
            Console.WriteLine("----------------------");
        }
    }

    static void AddEmployee(Dictionary<int, Employee> employees)
    {
        Console.WriteLine("\nEnter details for new employee:");
        Employee emp = new Employee();
        emp.TakeEmployeeDetailsFromUser();
        if (employees.ContainsKey(emp.Id))
        {
            Console.WriteLine("Employee with this ID already exists. Cannot add duplicate.");
        }
        else
        {
            employees.Add(emp.Id, emp);
            Console.WriteLine("Employee added successfully.");
        }
    }

    static void ModifyEmployee(Dictionary<int, Employee> employees)
    {
        Console.Write("\nEnter the employee ID to modify: ");
        int id = int.Parse(Console.ReadLine() ?? "0");
        if (employees.ContainsKey(id))
        {
            Employee emp = employees[id];
            Console.WriteLine("Current details: ");
            Console.WriteLine(emp);
            Console.WriteLine("Enter new details (press Enter to keep existing value):");

            Console.Write("Enter new name (current: {0}): ", emp.Name);
            string newName = Console.ReadLine() ?? string.Empty;
            if (!string.IsNullOrEmpty(newName))
            {
                emp.Name = newName;
            }

            Console.Write("Enter new age (current: {0}): ", emp.Age);
            string ageInput = Console.ReadLine() ?? string.Empty;
            if (!string.IsNullOrEmpty(ageInput) && int.TryParse(ageInput, out int newAge))
            {
                emp.Age = newAge;
            }

            Console.Write("Enter new salary (current: {0}): ", emp.Salary);
            string salaryInput = Console.ReadLine() ?? string.Empty;
            if (!string.IsNullOrEmpty(salaryInput) && double.TryParse(salaryInput, out double newSalary))
            {
                emp.Salary = newSalary;
            }

            Console.WriteLine("Employee details updated.");
        }
        else
        {
            Console.WriteLine("Employee with ID " + id + " does not exist.");
        }
    }

    static void PrintEmployeeById(Dictionary<int, Employee> employees)
    {
        Console.Write("\nEnter the employee ID to view details: ");
        int id = int.Parse(Console.ReadLine() ?? "0");
        if (employees.ContainsKey(id))
        {
            Console.WriteLine("\nEmployee details:");
            Console.WriteLine(employees[id]);
        }
        else
        {
            Console.WriteLine("Employee with ID " + id + " does not exist.");
        }
    }

    static void DeleteEmployee(Dictionary<int, Employee> employees)
    {
        Console.Write("\nEnter the employee ID to delete: ");
        int id = int.Parse(Console.ReadLine() ?? "0");
        if (employees.ContainsKey(id))
        {
            employees.Remove(id);
            Console.WriteLine("Employee with ID " + id + " has been deleted.");
        }
        else
        {
            Console.WriteLine("Employee with ID " + id + " does not exist.");
        }
    }
}