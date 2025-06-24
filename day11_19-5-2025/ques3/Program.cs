class Program
{
    public static string calculate(int num1, int num2, string operation)
    {
        int result = 0;
        switch (operation)
        {
            case "+":
                result = num1 + num2;
                break;
            case "-":
                result = num1 - num2;
                break;
            case "*":
                result = num1 * num2;
                break;
            case "/":
                if (num2 != 0)
                    result = num1 / num2;
                else
                    Console.WriteLine("Cannot divide by zero");
                break;
            default:
                Console.WriteLine("Invalid operation");
                return "Invalid operation";
        }
        return result.ToString();
    }
    static void Main(string[] args)
    {
        //ques3
        //3) Take 2 numbers from user, check the operation user wants to perform (+,-,*,/). Do the operation and print the result
        Console.WriteLine("Enter num1-");
        int num1 = Convert.ToInt32(Console.ReadLine());
        Console.WriteLine("Enter num2-");
        int num2 = Convert.ToInt32(Console.ReadLine());
        Console.WriteLine("Enter operation (+,-,*,/)");
        string operation = Console.ReadLine();
        var final_result = calculate(num1, num2, operation);
        Console.WriteLine($"{final_result}");

    }
}
