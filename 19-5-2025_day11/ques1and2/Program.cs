class Program
{
    public static void getlargest(int num1, int num2)
    {
        if (num1 > num2)
        {
            Console.WriteLine($"{num1} is greater than {num2}");
        }
        else
        {
            Console.WriteLine($"{num2} is greater than {num1}");
        }
    }

    static void Main(string[] args)
    {
        //ques1
        // Console.Write("Enter your name: ");
        // string name = Console.ReadLine();
        // Console.WriteLine($"Hello!! {name}");


        //ques2
        //2) Take 2 numbers from user and print the largest
        Console.WriteLine("Enter num1-");
        string num1 = Console.ReadLine();
        Console.WriteLine("Enter num2-");
        string num2 = Console.ReadLine();
        if(int.TryParse(num1,out int pnum1) && int.TryParse(num2, out int pnum2))
        {
        getlargest(pnum1, pnum2);
            
        }
        else
        {
            Console.WriteLine("Invalid input");
     
        }
        

    }
}