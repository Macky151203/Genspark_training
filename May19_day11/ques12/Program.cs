class program
{
    public static string encode(string input)
    {
        string temp = "";
        for (int i = 0; i < input.Length; i++)
        {
            char encodedchar = (char)(((input[i] - 'a' + 3) % 26) + 'a');
            temp += encodedchar.ToString();
        }
        return temp;
    }
    public static string decode(string input)
    {
        string temp = "";
        for (int i = 0; i < input.Length; i++)
        {
            int value = (input[i] - 'a' - 3) % 26;
            if (value < 0) {
                value += 26;
            }
            char encodedchar = (char)((value) + 'a');
            temp += encodedchar.ToString();
        }
        return temp;
    }

    public static bool checkstring(string input)
    {
        for (int i = 0; i < input.Length; i++)
        {
            if (input[i] < 'a' || input[i] > 'z')
            {
                return false;
            }
        }
        return true;
    }
    static void Main(string[] args)
    {
        Console.WriteLine("Enter a string:");
        string input = Console.ReadLine();
        if (string.IsNullOrEmpty(input) || !checkstring(input))
        {
            Console.WriteLine("Invalid input");
            return;
        }
        Console.WriteLine("Enter 1 to encode and 2 to decode.");
        int num;
        bool choice = int.TryParse(Console.ReadLine(), out num);
        if (choice)
        {
            if (num == 1)
            {
                string encodedinput = encode(input);
                Console.WriteLine($"Encoded string is {encodedinput}");
            }
            else if (num == 2)
            {
                string decodedinput = decode(input);
                Console.WriteLine($"Decoded string is {decodedinput}");
            }
            else
            {
                Console.WriteLine("Invalid Choice, enter between 1 or 2.");
            }
        }
        else
        {
            Console.WriteLine("Invalid input.");
        }
    }
}
