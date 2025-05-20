class Program
{
    public static void q1()
    {
        int num_of_users;
        Console.WriteLine("enter no.of users-");
        num_of_users = Convert.ToInt32(Console.ReadLine());
        KeyValuePair<string, int>[][] insta_data = new KeyValuePair<string, int>[num_of_users][];
        for (int i = 0; i < num_of_users; i++)
        {
            int num_posts;
            Console.WriteLine($"enter no.of posts for user {i + 1}- ");
            num_posts = Convert.ToInt32(Console.ReadLine());
            insta_data[i] = new KeyValuePair<string, int>[num_posts];
            for (int j = 0; j < num_posts; j++)
            {
                Console.WriteLine($"enter post {j + 1} details for user {i + 1}- ");
                Console.WriteLine("enter caption-");
                string? caption = Console.ReadLine();
                Console.WriteLine("enter likes-");
                int likes = Convert.ToInt32(Console.ReadLine());
                insta_data[i][j] = new KeyValuePair<string, int>(caption, likes);
            }
        }

        for (int i = 0; i < num_of_users; i++)
        {
            Console.WriteLine($"user {i + 1} posts-");
            for (int j = 0; j < insta_data[i].Length; j++)
            {
                Console.WriteLine($"post {j + 1} caption: {insta_data[i][j].Key} | likes: {insta_data[i][j].Value}");
            }
        }
    }
    static void Main(string[] args)
    {
        q1();
    }
}