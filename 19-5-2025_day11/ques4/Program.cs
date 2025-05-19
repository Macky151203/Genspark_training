class Program
{
    static void Main(string[] args)
    {
        //ques4
        //4) Take username and password from user. Check if user name is "Admin" and password is "pass" if yes then print success message. Give 3 attempts to user. In the end of eh 3rd attempt if user still is unable to provide valid creds then exit the application after print the message "Invalid attempts for 3 times. Exiting...."
        int cnt = 3;
        Console.WriteLine("Enter username -");
        string? username = Console.ReadLine();
        Console.WriteLine("Enter password -");
        string? password = Console.ReadLine();
        while (cnt > 0)
        {
            if (username == "Admin" && password == "pass")
            {
                Console.WriteLine("Success");
                break;
            }
            else
            {
                cnt--;
                Console.WriteLine($"Invalid credentials, {cnt} attempts left");
                if (cnt == 0)
                {
                    Console.WriteLine("Invalid attempts for 3 times. Exiting....");
                    break;
                }
                Console.WriteLine("Enter username -");
                username = Console.ReadLine();
                Console.WriteLine("Enter password -");
                password = Console.ReadLine();
            }
        }

    }
}
