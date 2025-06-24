class Program
{
    static void Main(string[] args)
    {
        //ques5
        //5) Take 10 numbers from user and print the number of numbers that are divisible by 7
        int count = 0;
        for (int i = 0; i < 10; i++)
        {
            Console.WriteLine("Enter a number:");
            int num = Convert.ToInt32(Console.ReadLine());
            if (num % 7 == 0)
            {
                count++;
            }
        }
        Console.WriteLine($"The number of numbers that are divisible by 7 is: {count}");
    }
}
