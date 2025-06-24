class Program
{
    public static void Startgame(string guess, string secret)
    {
        if (string.IsNullOrEmpty(guess.Trim()))
        {
            Console.WriteLine("Please enter a valid guess.");
            return;
        }
        List<int> bulls = new List<int>();
        List<int> cows = new List<int>();
        bool won = false;
        int attempts = 1;
        while (!won)
        {
            bulls.Clear();
            cows.Clear();
            for (int i = 0; i < guess.Length; i++)
            {
                if (char.ToLower(guess[i]) == char.ToLower(secret[i]))
                {
                    bulls.Add(i);
                }
                else if (secret.ToLower().Contains(char.ToLower(guess[i])))
                {
                    cows.Add(i);
                }
            }

            if (bulls.Count == secret.Length)
            {
                Console.WriteLine($"You won in {attempts} attempts!");
                won = true;
            }
            else
            {
                attempts++;
                Console.WriteLine($"Bulls in positions: {string.Join(", ", bulls)}");
                Console.WriteLine($"Cows in positions: {string.Join(", ", cows)}");
                Console.WriteLine("Try again:");
                Console.Write("Enter your guess: ");
                guess = Console.ReadLine();
            }
        }
        
        
    }
    static void Main(string[] args)
        {
            //ques9
            //bull-correct word in correct pos
            //cow-correct word in wrong pos

            string secret = "Roger";
            Console.Write("Enter your guess: ");
            string guess = Console.ReadLine();
            Startgame(guess,secret);
        }
}
