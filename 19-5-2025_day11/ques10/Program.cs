class Program
{
    public static void checkvalid(int[] row)
    {
        if(row.Length != 9)
        {
            Console.WriteLine("Invalid length, it should be 9.");
            return;
        }
        bool[] visited = new bool[9];
        for (int i = 0; i < row.Length; i++)
        {
            if (visited[row[i] - 1] == false)
            {
                visited[row[i] - 1] = true;
            }
            else
            {
                Console.WriteLine("Invalid");
                return;
            }
        }x
        Console.WriteLine("Valid");
    }
    static void Main(string[] args)
    {
        int[] row = new int[9];
        int num;
        for (int i = 0; i < 9; i++)
        {
            Console.WriteLine($"Enter a number {i+1}:");
            if (int.TryParse(Console.ReadLine(), out num))
            {
                if (num >= 1 && num <= 9)
                {
                    row[i] = num;
                }
                else
                {
                    Console.WriteLine("Please enter a number between 1 and 9.");
                    i--;
                }
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a valid number.");
                i--;
            }
        }
        checkvalid(row);
    }
}
