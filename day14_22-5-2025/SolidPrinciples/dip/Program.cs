interface IOperations
{
    void Add(int a, int b);
    void Subtract(int a, int b);
    void Multiply(int a, int b);
    void Divide(int a, int b);
}

class Operationswithvalue : IOperations
{
    public void Add(int a, int b)
    {
        Console.WriteLine($"Addition: {a + b}");
    }

    public void Subtract(int a, int b)
    {
        Console.WriteLine($"Subtraction: {a - b}");
    }

    public void Multiply(int a, int b)
    {
        Console.WriteLine($"Multiplication: {a * b}");
    }

    public void Divide(int a, int b)
    {
        if (b != 0)
            Console.WriteLine($"Division: {a / b}");
        else
            Console.WriteLine("Cannot divide by zero");
    }
}

class Operations : IOperations
{
    public void Add(int a, int b)
    {
        Console.WriteLine($"Addition of {a} and {b}");
    }

    public void Subtract(int a, int b)
    {
        Console.WriteLine($"Subtraction of {a} and {b}");
    }

    public void Multiply(int a, int b)
    {
        Console.WriteLine($"Multiplication of {a} and {b}");
    }

    public void Divide(int a, int b)
    {
        Console.WriteLine($"Division of {a} and {b}");
    }
}

class OperationService
{
    private readonly IOperations _operations;

    public OperationService(IOperations operations)
    {
        _operations = operations;
    }

    public void Addition(int a, int b)
    {
        _operations.Add(a, b);
    }
    public void Subtraction(int a, int b)
    {
        _operations.Subtract(a, b);
    }
    public void Multiplication(int a, int b)
    {
        _operations.Multiply(a, b);
    }
    public void Division(int a, int b)
    {
        _operations.Divide(a, b);
    }
}
class Program
{
    static void Main(string[] args)
    {
        //dip
        //replace the class to be implemented here
        IOperations operations = new Operationswithvalue();
        OperationService operationService = new OperationService(operations);

        operationService.Addition(10, 20);
        operationService.Subtraction(20, 10);
        operationService.Multiplication(10, 20);
        operationService.Division(20, 10);
        operationService.Division(20, 0);
    }
}
